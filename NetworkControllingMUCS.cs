using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;

public class NetworkControllingMUCS : NetworkBehaviour {

    public bool networkV;
    public GameObject[] players;
    public GameObject gunRenderCam;
    
	// Use this for initialization
	void Start () 
    {
       
      
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        
        
            if (!isLocalPlayer)
            {
                GetComponent<FirstPersonController>().enabled = false;
                GetComponentInChildren<Camera>().enabled = false;
                GetComponent<AudioSource>().enabled = false;
                GetComponent<Weapons>().enabled = false;
                GetComponent<PlayerStatus>().enabled = false;
                gunRenderCam.GetComponent<Camera>().enabled = false;
                GetComponentInChildren<PlayerAnimConNetwork>().enabled = false;
                
            
              
                
            }
            else
            {
                GetComponent<FirstPersonController>().enabled = true;
                GetComponentInChildren<Camera>().enabled = true;
                GetComponent<AudioSource>().enabled = true;
                GetComponent<Weapons>().enabled = true;
                GetComponent<PlayerStatus>().enabled = true;
                gunRenderCam.GetComponent<Camera>().enabled = true;
                GetComponentInChildren<PlayerAnimConNetwork>().enabled = true;
              
              
            }
        

        
        
	}

    public void Awake()
    {
        
    }
    void OnNetworkInstantiate(NetworkMessageInfo info)
    {
       
    }
}
