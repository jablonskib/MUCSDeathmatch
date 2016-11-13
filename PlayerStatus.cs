using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class PlayerStatus : NetworkBehaviour 
{
    float deathTimer;
    bool invincible, fast;
    [SyncVar]
    public string userName;

    [SyncVar]
    public int health;

	[SyncVar]
	public bool amIServer;

	[SyncVar]
	public int kills;

	[SyncVar]
	public int deaths;

	[SyncVar]
	public float kdr; 

	[SyncVar]
	public string  whoDied;

	[SyncVar]
	public string whoJoined;

	public GameObject lastHit;

    public Animator anim;
    public GameObject[] spawns, weapons;
    public GameObject player, gameController;
	public Text deathText;
	public Text hpText;
	public Text joinText;
	float timer;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        gameController = GameObject.Find("Game Controller");
		spawns = GameObject.FindGameObjectsWithTag ("Spawn");
        hpText = GameObject.Find("HpShow").GetComponent<Text>(); ;
        deathText = GameObject.Find("DeathShow").GetComponent<Text>();
        joinText = GameObject.Find("JoinShow").GetComponent<Text>();

       
        GameObject g = GameObject.Find("PersistentData");
        userName = g.GetComponent<PersistentData>().username;
        Debug.Log(userName);

		if (isServer) 
		{
			amIServer = true;
		}
	}
	
	// Update is called once per frame
	void Update () 
    {

		if(kills == 0 || deaths == 0)
		{
			kdr = 0;
		}
		else
		{
			kdr = (float)(kills/deaths);
		}
        joinText.text = whoJoined;
		if(whoJoined != "")
		{
			timer += Time.deltaTime;
			if (timer > 3f) 
			{
				timer = 0;
                
				whoJoined = "";
			}
		}
			
		if (whoDied != "" ) 
		{
			timer += Time.deltaTime;
			if (timer > 3f) 
            {
				timer = 0;
				deathText.text = "";
				whoDied = "";
			}
		}
	
			
		if (health <= 0) 
		{
		
			whoDied = userName + " has died. Goodbye!";
			deathText.text = whoDied;
            anim.SetBool("Dead", true);
            deathTimer += Time.deltaTime;
            if (deathTimer > 6f)
            {
                
                deaths += 1;
                CmdIncreaseKillCount();
                CmdSpawnRandomWeapon();
                transform.position = spawns[Random.Range(0, spawns.Length - 1)].transform.position;

                CmdResetHealth(gameObject);
                deathTimer = 0;
            }

		}
        else
        {
            anim.SetBool("Dead", false);
        }

        if(health > 100)
        {
            health = 100;
        }
        hpText.text = "HP: " + health.ToString();


	}


	public void OnDamageTaken(int d, GameObject player)
    {
		lastHit = player;
        health -= d;
    }

    public void Pickup(string p)
    {
        switch (p)
        {
            case "Health":
                health += 20;
                break;
        }
    }
    
	[Command]
    void CmdIncreaseKillCount()
    {
        lastHit.GetComponent<PlayerStatus>().kills += 1;
    }
    [Command]
    void CmdSpawnRandomWeapon()
    {
        GameObject g = (GameObject)Instantiate(weapons[Random.Range(0, weapons.Length - 1)], transform.position + Vector3.up * 3f, transform.rotation);
        NetworkServer.Spawn(g);
    }
 
    [ClientRpc]
    void RpcSpawnRandomWeapon()
    {
        GameObject g = (GameObject)Instantiate(weapons[Random.Range(0, weapons.Length - 1)], transform.position, transform.rotation);
        NetworkServer.SpawnWithClientAuthority(g, connectionToServer);
        ClientScene.RegisterPrefab(g);
    }

    [Command]
    void CmdResetHealth(GameObject me)
    {
        me.GetComponent<PlayerStatus>().health = 100;
    }
  
   IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
    }

    
}
