using System.Collections;
using System.Collections.Generic;

public class Player
{
    public enum PlayerType
    {
        NPC = 1,
        Human = 2
    }

    private List<Card> _cards;

	private PlayerType _type;

	private int _points;

	public Player(PlayerType type){
		_cards = new List<Card>();
		_type = type;
		_points = 0;
	}

	public void SetPoints(int q){
		_points += q;
	}

	public string GetPoints(){
		return _points.ToString();
	}

	public List<Card> GetCardsList(){
		return _cards;
	}
	
	public void SetCardsList(List<Card> cardsList){
		_cards = cardsList;
	}

	public PlayerType GetPlayerType(){
		return _type;
	}

    public void DealCard(int cardsCount){
        DealCards dealCards = new DealCards()
        {
            cardsAmount = cardsCount,
            player = this
        };
        dealCards.Execute();
        GameController.instance.SaveCommand(dealCards);
	}

    public void SaveDealedCards(){
        SaveDealCards saveDealCards = new SaveDealCards()
        {
            player = this
        };
        saveDealCards.Execute();
        GameController.instance.SaveCommand(saveDealCards);
    }
}
