public class EvaluateRules: Command {

    public Card cardOne;

    public Card cardTwo;

    /// <summary>
    /// Funcion que evalua las reglas basicas del juego
    /// </summary>
	public override void Execute(){

        var cardOneType = cardOne.GetCardType();
        var cardTwoType = cardTwo.GetCardType();

        //Caso de Empate
        if (cardOneType == cardTwoType){
            return;
        }
		//Casos Piedra
		else if (cardOneType == Card.CardTypes.Piedra)
		{
			if (cardTwoType == Card.CardTypes.Tijera)
			{
                //Gana 1
                GameController.instance.playerOne.SetPoints(1);
			}
			else if (cardTwoType == Card.CardTypes.Papel)
			{
				//Gana 2
                GameController.instance.playerTwo.SetPoints(1);
			}
		}
        //Caso Papel
		else if (cardOneType == Card.CardTypes.Papel)
		{
			if (cardTwoType == Card.CardTypes.Piedra)
			{
				//Gana 1
                GameController.instance.playerOne.SetPoints(1);
			}
			else if (cardTwoType == Card.CardTypes.Tijera)
			{
				//Gana 2
                GameController.instance.playerTwo.SetPoints(1);
			}
		}
        //Caso Tijera
		else if (cardOneType == Card.CardTypes.Tijera)
		{
			if (cardTwoType == Card.CardTypes.Papel)
			{
				//Gana 1
                GameController.instance.playerOne.SetPoints(1);
			}
			else if (cardTwoType == Card.CardTypes.Piedra)
			{
				//Gana 2
                GameController.instance.playerTwo.SetPoints(1);
			}
		}

	} 
}
