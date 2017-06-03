using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;

public class SaveDealCards : Command
{

    public Player player;
   
    public override void Execute()
    {
        if (player != null)
        {
            string cardsList = String.Empty;
			foreach (var item in player.GetCardsList())
			{
				cardsList += ((int)item.GetCardType()).ToString();
				if (player.GetCardsList().FindIndex(x => x == item) < (player.GetCardsList().Count - 1))
				{
					cardsList += ",";
				}
			} 
            PlayerPrefs.SetString("PLAYER" + player.GetType(), cardsList);
        }
    } 
}