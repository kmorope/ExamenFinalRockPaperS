using UnityEngine;
using System.Collections;

public class XBOXInputController : MonoBehaviour
{

    public XBOXInputController instance;

    public enum ControlType{
        game,menu
    }

    public ControlType _controlType = ControlType.game;

	private float _umbral = 0.5f;

	private bool _isListening = true;

	private float _cooldownTime = 0.25f;

    // Use this for initialization
	void Start()
	{
        instance = this;
	}

	// Update is called once per frame
	public void ControllUpdate()
	{
        switch(_controlType){
            case ControlType.game:
				if (Input.GetAxis("JAxisX") > _umbral && _isListening)
				{
					DisableListening();
					Debug.Log(Input.GetButton("JAxisX"));
				}

				if (Input.GetAxis("JAxisX") < (_umbral * -1) && _isListening)
				{
					DisableListening();
					Debug.Log(Input.GetButton("JAxisX"));
				}
                break;
			case ControlType.menu:
				if (Input.GetAxis("JAxisX") > _umbral && _isListening)
				{
					DisableListening();
					Debug.Log(Input.GetButton("JAxisX"));
				}

				if (Input.GetAxis("JAxisX") < (_umbral * -1) && _isListening)
				{
					DisableListening();
					Debug.Log(Input.GetButton("JAxisX"));
				}
                break;
            default:
                break;
        }
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
