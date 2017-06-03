using UnityEngine;
using UnityEngine.UI;

public class AutoPlayCards : Command {
 
    public Player player;

    public Card autoCard;

	public override void Execute(){
        var cards = player.GetCardsList();
        var range = cards.Count;
        var rand = Random.Range(0, range);
        var card = cards[rand];
        autoCard.SetCardType(player.GetCardsList()[card.cardPosition].GetCardType());
		autoCard.SwitchCard(true);
        cards.Remove(card);
        GameController.instance.playerTwo.SetCardsList(cards);
	} 
}
