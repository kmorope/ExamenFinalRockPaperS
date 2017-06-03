using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{

    public enum PanelType{
        Main,
        Settings
    }

    public PanelType panelType = PanelType.Main;

	// Use this for initialization
	void Start()
	{
        if(panelType == PanelType.Settings)
		    HidePanel();
	}

	public void ShowPanel()
	{
		this.gameObject.SetActive(true);
	}

	public void HidePanel()
	{
		this.gameObject.SetActive(false);
	}
}