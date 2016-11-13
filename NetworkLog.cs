using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class NetworkLog : NetworkBehaviour {
    public Text textU;
	GameObject gameController;
    [SyncVar]
    public string playerName;
	// Use this for initialization
	void Start ()
    {
		gameController = GameObject.Find ("Game Controller");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnPlayerConnected(NetworkPlayer player)
    {
		gameController.GetComponent<GameControlClass> ().AddPlayerToList (GetComponent<PlayerStatus> ().userName);
        //textU.text = GetComponent<PlayerStatus>().userName + "has joined the server.";
        Debug.Log(GetComponent<PlayerStatus>().userName + "has joined the server.");
    }

    void OnServerConnected(NetworkPlayer player)
    {
        //textU.text = GetComponent<PlayerStatus>().userName + "has joined the server.";
        Debug.Log(GetComponent<PlayerStatus>().userName + "has joined the server.");
    }
}
