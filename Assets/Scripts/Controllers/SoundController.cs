using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour
{

    static public SoundController instance;

    public enum Sounds{
        win,
        lose,
        table
    }

	// Use this for initialization
	void Start()
	{
        instance = this;
	}

	// Update is called once per frame
	void Update()
	{
			
	}
}
