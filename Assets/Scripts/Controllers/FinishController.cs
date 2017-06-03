using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FinishController : MonoBehaviour
{

	float _umbral = 0.5f;

	bool _isListening = true;

	float _cooldownTime = 0.25f;

    public int _focusedButton = 0;

    public Button repeatBtn;

    public Button menuBtn;

	// Use this for initialization
	void Start()
	{
        GameObject.Find("Status").GetComponent<Text>().text = PlayerPrefs.GetString("STATUS");
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetAxis("JAxisY") > _umbral && _isListening)
		{
			if (_focusedButton > 1)
				_focusedButton--;
			DisableListening();
			Flip(_focusedButton == 1, repeatBtn); 
			Flip(_focusedButton == 2, menuBtn);
		}

		if (Input.GetAxis("JAxisY") < (_umbral * -1) && _isListening)
		{
			if (_focusedButton < 2)
				_focusedButton++;
			DisableListening();
			Flip(_focusedButton == 1, repeatBtn); 
			Flip(_focusedButton == 2, menuBtn);
		}
		if (Input.GetKey("joystick button 16"))
		{
			if (_focusedButton == 1)
			{
                RepeatGame();
			}
			else if (_focusedButton == 2)
			{
				RestartGame();
			}
		}
	}

	public void Flip(bool open, Button btn)
	{
		var rt = btn.gameObject.GetComponent(typeof(RectTransform)) as RectTransform;
		rt.sizeDelta = open ? new Vector2(130, 130) : new Vector2(100, 100);
	}

	public void RestartGame()
	{
        PlayerPrefs.SetInt("REPEAT", 0);
		SceneLoader.instance.LoadScene("Welcome");
	}

	public void RepeatGame()
	{
        PlayerPrefs.SetInt("REPEAT", 1);
		SceneLoader.instance.LoadScene("Game");
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
