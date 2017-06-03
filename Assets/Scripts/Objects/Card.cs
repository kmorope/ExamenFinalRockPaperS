using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card:MonoBehaviour
{ 	
	public enum CardTypes
    {
        Piedra = 1,
        Papel = 2,
        Tijera = 3
    }

	public CardTypes _type;

    public int cardPosition;

	public Card(CardTypes type)
    {
        _type = type;
    }

    public void SetCardType(CardTypes type){
        this._type = type;
    }

    public CardTypes GetCardType()
    {
        return _type;
    }

    public void PlayCard(){
        GameController.instance.ClearCardsState();
        GameController.instance.selectedCard = this.cardPosition+1;
        PlayCard pl = new PlayCard();
        pl.player = GameController.instance.GetPlayer(Player.PlayerType.Human);
        pl.card = this;
        pl.Execute();
        GameController.instance.SaveCommand(pl);
    }

    public void SelectCard(){
        
    }

    public void SwitchCard(bool open){
        var rt = this.gameObject.GetComponent(typeof(RectTransform)) as RectTransform;
        rt.sizeDelta = open ? new Vector2(90, 100) : new Vector2(80, 90);
        var image = this.gameObject.GetComponent(typeof(Image)) as Image;
        image.sprite = null;
        var cardImage = open ? _type.ToString().ToLower() : "giphy";
        image.sprite = Resources.Load("Images/" + cardImage, typeof(Sprite)) as Sprite;
    }

    public void SetStatus(bool status){
        this.gameObject.SetActive(status);
    }
}
