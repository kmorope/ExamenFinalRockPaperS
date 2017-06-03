using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    static public GameController instance;

    int frameCount = 0; 

    public Player playerOne = new Player(Player.PlayerType.Human);

    public Player playerTwo = new Player(Player.PlayerType.NPC);

    public Text textOne;

    public Text textTwo;

    public Image barFill;

    public PanelController panel;

    [SerializeField]
    List<Command> commandsList = new List<Command>();

    [SerializeField]
    List<int> frameList = new List<int>();

    List<int> playedCards = new List<int>();

    public enum GameState
    {
        play, replay
    }

    public GameState gameState = GameState.play;

    public int cardsQty = 5;

	float _umbral = 0.5f;

	bool _isListening = true;

    bool _isPlayed = false;

	float _cooldownTime = 0.25f;

    public int selectedCard = 0;

    public int turn = 0;

    void Start()
    {
        instance = this; 
        panel.HidePanel();
        if (PlayerPrefs.GetInt("REPEAT") == 1)
        {
            PlayerPrefs.SetInt("REPEAT", 0);
            ReplayGame();
        }else{
			playerOne.DealCard(cardsQty);
			playerTwo.DealCard(cardsQty);
        }


    }

    void Update()
    {
        frameCount++;//Number to Save the frame

        /// Repetir juego
		if (gameState == GameState.replay)
		{
            for (var i = 0; i < frameList.Count;i++){
                if (frameList[i] == frameCount){
                    commandsList[i].Execute();
                }
            } 
        }else{
            if ((Input.GetAxis("JAxisX") > _umbral || Input.GetKey(KeyCode.LeftArrow)) && _isListening && !_isPlayed && gameState != GameState.replay)
			{
				DisableListening();
				if (selectedCard > 1)
				{
					selectedCard--;
				}
                PlayCard(); 
			}

            if ((Input.GetAxis("JAxisX") < (_umbral * -1) || Input.GetKey(KeyCode.RightArrow)) && _isListening && !_isPlayed && gameState != GameState.replay)
			{
                DisableListening();
				if (selectedCard < cardsQty)
				{
					selectedCard++;
				}
                PlayCard();
			}
            if ((Input.GetKey("joystick button 16") || Input.GetKey(KeyCode.Return)) && !_isPlayed && gameState != GameState.replay)
			{
				DisableListening(); 
                PlaySelectedCard();
			}
        }
    }

    public void SaveCommand(Command command)
    {
        frameList.Add(frameCount);
        commandsList.Add(command);
    }

    public Player GetPlayer(Player.PlayerType type){
        return type == Player.PlayerType.Human ? playerOne : playerTwo;
    }

    public void ClearCardsState(){
        var cards = GameObject.FindGameObjectsWithTag("PlayerCard");
        foreach (var card in cards)
        {
            card.GetComponent<Card>().SwitchCard(false);
        }
    }

    public Card SelectCard(int index){
		var cards = GameObject.FindGameObjectsWithTag("PlayerCard");
        foreach (var card in cards)
        {
            if(card.GetComponent<Card>().name.Contains("_" + index)) {
                return card.GetComponent<Card>();
            }
		}
        return null;
    }

    public void PlayCard(){
        ClearCardsState();
		PlayCard pl = new PlayCard();
        pl.player = playerOne;
		pl.card = SelectCard(selectedCard-1);
        if (pl.card == null)
            EnableListening();
		pl.Execute();
		SaveCommand(pl);
    }

    public void PlaySelectedCard(){
        if (selectedCard != 0 && SelectCard(selectedCard - 1) != null){
            turn++;
            playedCards.Add(selectedCard - 1);
			_isPlayed = true;
			AutoPlayCards au = new AutoPlayCards();
			au.player = playerTwo;
			au.autoCard = GameObject.FindGameObjectsWithTag("NpcCard")[0].GetComponent<Card>();
			au.Execute();
			SaveCommand(au);
            Invoke("Play", 2); 
        } 
    }

    public void Play(){ 
		EvaluateRules er = new EvaluateRules();
		er.cardOne = SelectCard(selectedCard - 1);
		er.cardTwo = GameObject.FindGameObjectsWithTag("NpcCard")[0].GetComponent<Card>();
		er.Execute();
		SaveCommand(er);
		GUIChange gc = new GUIChange();
		gc.textOne = textOne;
		gc.textTwo = textTwo;
		gc.pointsOne = playerOne.GetPoints();
		gc.pointsTwo = playerTwo.GetPoints();
		gc.barFill = barFill;
		gc.Execute();
		SaveCommand(gc);
		var cards = playerOne.GetCardsList();
		cards.Remove(er.cardOne);
		playerOne.SetCardsList(cards);
		GameObject.Find("player_" + (selectedCard - 1)).SetActive(false);
        GameObject.FindGameObjectsWithTag("NpcCard")[0].GetComponent<Card>().SwitchCard(false);
		_isPlayed = false;
        if(turn==5){
            panel.ShowPanel();
            if(Convert.ToInt32(playerOne.GetPoints()) > Convert.ToInt32(playerTwo.GetPoints())){
                PlayerPrefs.SetString("STATUS","Ganaste!!");
                HistoryController.SetVictoria(Player.PlayerType.Human);
            }
            if (Convert.ToInt32(playerTwo.GetPoints()) > Convert.ToInt32(playerOne.GetPoints())){
				PlayerPrefs.SetString("STATUS", "Perdiste :(");
                HistoryController.SetVictoria(Player.PlayerType.NPC);
			}
            if (Convert.ToInt32(playerOne.GetPoints()) == Convert.ToInt32(playerTwo.GetPoints())){
				PlayerPrefs.SetString("STATUS", "Empate");
			}
            GameObject.Find("txtNPC").GetComponent<Text>().text = playerTwo.GetPoints();
            GameObject.Find("txtVictoPl").GetComponent<Text>().text = playerOne.GetPoints();
            Invoke("FinishGame", 2);
		}
    }

    public void FinishGame(){
		SceneLoader.instance.LoadScene("Finish");
    }

    public void ReplayGame(){
        gameState = GameState.replay;
        frameCount = 0;
    }

	private void DisableListening()
	{
		_isListening = false;
		Invoke("EnableListening", _cooldownTime);
	}

	private void EnableListening()
	{
		_isListening = true;
	}

}
