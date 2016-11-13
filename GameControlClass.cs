using UnityEngine;
using System.Collections;

public class GameControlClass : MonoBehaviour {
    AudioSource backgroundMusic;
	public GameObject[] bulletHoles, spawnedGuns, spawnedSpheres;
	GameObject player;
	ArrayList playerList;
	public GameObject radarSphere;
    public GameObject[] weaponSpawns, weapons;
    // Use this for initialization
    void Start()
    {
		player = GameObject.FindGameObjectWithTag ("Player");
        backgroundMusic = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        weaponSpawns = GameObject.FindGameObjectsWithTag("Weapon Spawn");
        SpawnWeapons();
		InvokeRepeating ("SpawnWeapons", 2f, 60f);

    }

 

  
    void SpawnWeapons()
    {
        Debug.Log("Server");
        for (int i = 0; i < weapons.Length; i++)
        {
            int r = (int)Random.Range(0, weaponSpawns.Length);
            int w = (int)Random.Range(0, weapons.Length);
            GameObject g = (GameObject)GameObject.Instantiate(weapons[w], weaponSpawns[r].transform.position, weaponSpawns[r].transform.rotation);
			//GameObject s = (GameObject)GameObject.Instantiate (radarSphere, g.transform.position, g.transform.rotation);
			//s.transform.parent = g.transform;


            //NetworkSpawn.Instantiate(g);
            Debug.Log("spawn");
        }
    }

    // Update is called once per frame
    void Update()
    {

		/*spawnedSpheres = GameObject.FindGameObjectsWithTag ("RadarSphere");
		spawnedGuns = GameObject.FindGameObjectsWithTag ("Weapon");
		for(int i = 0; i < spawnedGuns.Length -1; i++)
		{
			if ((spawnedGuns [i].transform.position.y - player.transform.position.y) > .3f && spawnedGuns[i] != null) 
			{
				spawnedSpheres [i].GetComponent<ChangeRadarColor> ().ChangeRed ();
			}

			if ((spawnedGuns [i].transform.position.y - player.transform.position.y) >-.3f && (spawnedGuns [i].transform.position.y - player.transform.position.y) < .3f  && (spawnedGuns [i].transform.position.y - player.transform.position.y) < 1f  && spawnedGuns[i] != null)
			{
				spawnedSpheres [i].GetComponent<ChangeRadarColor> ().ChangeGreen ();
			}

			if ((spawnedGuns [i].transform.position.y - player.transform.position.y) < -.3f && spawnedGuns[i] != null) 
			{
				spawnedSpheres [i].GetComponent<ChangeRadarColor> ().ChangeYellow();
			}

		}*/


        if (Input.GetKeyDown(KeyCode.M))
        {
            if (backgroundMusic.isPlaying)
            {
                backgroundMusic.Stop();
            }
            else
            {
                backgroundMusic.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

    }

	public void AddPlayerToList(string player)
	{
		playerList.Add (player);
	}
}
