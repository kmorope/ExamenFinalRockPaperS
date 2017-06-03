using UnityEngine.UI;

public class GUIChange: Command {
     
    public Text textOne;

    public Text textTwo;

    public Image barFill;

    public string pointsOne;

    public string pointsTwo;
 
	public override void Execute(){
		textOne.text = pointsOne;
        textTwo.text = pointsTwo;
        barFill.fillAmount += 0.2f;
	} 
}