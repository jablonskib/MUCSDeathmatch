using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Workaround : NetworkBehaviour {
    public GameObject gunPos;
    public GameObject missile;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [Command]
    public void CmdSpawnMissile()
    {
        GameObject bfgM = (GameObject)GameObject.Instantiate(missile, gunPos.transform.position + gunPos.transform.forward * 2f, gunPos.transform.rotation);
        NetworkServer.Spawn(bfgM);

        
    }
}
