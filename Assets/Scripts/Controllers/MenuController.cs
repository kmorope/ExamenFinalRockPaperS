using UnityEngine; 
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
     
    public Button _playButton;

    public Button _settingsButton;

    public Button _historyButton;

    public int _focusedButton = 0;

	float _umbral = 0.5f;

	bool _isListening = true;

	float _cooldownTime = 0.25f;

    public PanelController panelConfig;

	public PanelController panelHistory;


	// Use this for initialization
	void Start()
	{
		Flip(true,_playButton);
        _focusedButton = 1;
	}

	// Update is called once per frame
	void Update()
	{
        if ((Input.GetAxis("JAxisY") > _umbral) || Input.GetKey(KeyCode.UpArrow) && _isListening) 
		{
            if (_focusedButton > 1)
                _focusedButton--;
			DisableListening();
			Flip(_focusedButton == 1, _playButton);
            Flip(_focusedButton == 3, _settingsButton);
            Flip(_focusedButton == 2, _historyButton);
		}

        if ((Input.GetAxis("JAxisY") < (_umbral * -1)) || Input.GetKey(KeyCode.DownArrow) && _isListening)
		{
            if (_focusedButton < 3)
                _focusedButton++;
			DisableListening();
			Flip(_focusedButton == 1, _playButton);
			Flip(_focusedButton == 3, _settingsButton);
			Flip(_focusedButton == 2, _historyButton);
		}
        if(Input.GetKey("joystick button 16") || Input.GetKey(KeyCode.Return)){
            DisableListening();
            if (_focusedButton == 1){
                ChangeScene();
            }
			if (_focusedButton == 2)
			{
				OpenHistory();
			}
			if (_focusedButton == 3)
			{
				OpenSettings();
			}
        }
        if (Input.GetKey("joystick button 17")|| Input.GetKey(KeyCode.Escape))
		{
            DisableListening();
			if (_focusedButton == 2)
			{
				CloseHistory();
			}
			if (_focusedButton == 3)
			{
				CloseSettings();
			}
		}
	}

	public void Flip(bool open,Button btn)
	{
		var rt = btn.gameObject.GetComponent(typeof(RectTransform)) as RectTransform;
		rt.sizeDelta = open ? new Vector2(160, 160) : new Vector2(100, 100); 
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

    public void ChangeScene(){
        SceneLoader.instance.LoadScene("Game");
    }

    public void OpenSettings(){
        panelConfig.ShowPanel();
    }

	public void OpenHistory()
	{
        panelHistory.GetComponent<HistoryController>().LoadVictorias();
		panelHistory.ShowPanel();
	}

	public void CloseSettings()
	{
		panelConfig.HidePanel();
	}

	public void CloseHistory()
	{
		panelHistory.HidePanel();
	}
}