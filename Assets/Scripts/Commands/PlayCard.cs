using UnityEngine;
using UnityEngine.UI;

public class PlayCard: Command {

    public Card card;

    public Player player;

	public override void Execute(){
        if(card != null){
			card.SetCardType(player.GetCardsList()[card.cardPosition].GetCardType());
			card.SwitchCard(true);
        } 
	} 
}
