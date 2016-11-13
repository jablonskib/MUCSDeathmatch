using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.NetworkSystem;
using System.Collections;

public class GameControl : NetworkBehaviour {
    AudioSource backgroundMusic;
    GameObject[] bulletHoles, healthSpawns;
    ArrayList playerNames;

    public GameObject[] players;
    public GameObject[] playerNamesT, playerScores;
    public GameObject scores, health;

    
    public GameObject[] weaponSpawns, weapons;
	// Use this for initialization
	void Start () 
    {
        backgroundMusic = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        weaponSpawns = GameObject.FindGameObjectsWithTag("Spawn");
        healthSpawns = GameObject.FindGameObjectsWithTag("HealthSpawn");
		if (isServer) 
        {
			InvokeRepeating ("RpcSpawnRandomWeapon", 3f, 15f);
		}

        if(isServer)
        {
            InvokeRepeating("RpcSpawnHealthRandom", 60f, 30f);
        }
		


					
	}

  
 
	// Update is called once per frame
	void Update () 
    {
        players = GameObject.FindGameObjectsWithTag("Player");
       // bulletHoles = GameObject.FindGameObjectsWithTag("BulletHole");
        /*if(bulletHoles.Length > 30)
        {
            for(int i = 0; i < bulletHoles.Length; i++)
            {
                NetworkSpawn.Destroy(bulletHoles[i]);
            }
        }*/

		if (Input.GetKeyDown (KeyCode.L)) 
		{
			if (isServer) 
			{
				RpcSpawnRandomWeapon ();
			} 
			else 
			{
				CmdSpawnRandomWeapon ();
			}
		}
        if(Input.GetKey(KeyCode.Tab) || Input.GetKey(KeyCode.Joystick1Button6))
        {
            scores.GetComponent<Canvas>().enabled = true;
            for(int i = 0; i < players.Length ; i++)
            {
                playerNamesT[i].GetComponent<Text>().text = players[i].GetComponent<PlayerStatus>().userName;

                playerScores[i].GetComponent<Text>().text = players[i].GetComponent<PlayerStatus>().kills.ToString() + " / " + players[i].GetComponent<PlayerStatus>().deaths.ToString();
            }
        }
        else
        {
            scores.GetComponent<Canvas>().enabled = false;
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            if(backgroundMusic.isPlaying)
            {
                backgroundMusic.Stop();
            }
            else
            {
                backgroundMusic.Play();
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

       
	
	}
			[Command]
			public void CmdSpawnRandomWeapon()
			{
				GameObject g = (GameObject)Instantiate (weapons [Random.Range (0, weapons.Length - 1)], weaponSpawns [Random.Range (0, weaponSpawns.Length - 1)].transform.position, weaponSpawns [Random.Range (0, weaponSpawns.Length - 1)].transform.rotation);
				NetworkServer.Spawn(g);
				ClientScene.RegisterPrefab (g);
			}

			[ClientRpc]
			void RpcSpawnRandomWeapon()
			{
				GameObject g = (GameObject)Instantiate (weapons [Random.Range (0, weapons.Length - 1)], weaponSpawns [Random.Range (0, weaponSpawns.Length - 1)].transform.position, weaponSpawns [Random.Range (0, weaponSpawns.Length - 1)].transform.rotation);
				NetworkServer.Spawn(g);
				ClientScene.RegisterPrefab (g);
			}

            [ClientRpc]
            void RpcSpawnHealthRandom()
            {
                int ranNum = Random.Range(0, healthSpawns.Length -1);
                GameObject g = (GameObject)Instantiate(health, healthSpawns[ranNum].transform.position, healthSpawns[ranNum].transform.rotation);
                NetworkServer.Spawn(g);
                ClientScene.RegisterPrefab(g);
            }
}
