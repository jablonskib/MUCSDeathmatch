using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class HurtScript : NetworkBehaviour {

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void Damage(int d)
    {
        //if (!isServer)
          //  return;
        gameObject.GetComponent<PlayerStatus>().health -= d;
    }
}
