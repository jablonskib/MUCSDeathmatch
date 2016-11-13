using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class BulletDestroy : NetworkBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {

        NetworkSpawn.Destroy(gameObject, 5f);
	}
}
