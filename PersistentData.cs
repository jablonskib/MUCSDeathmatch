using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PersistentData : MonoBehaviour 
{
    public Text userText;
    public string username;
	// Use this for initialization
	void Start () 
    {

        Object.DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void SetUsername()
    {
        username = userText.text;
    }
}
