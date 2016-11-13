using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Missile : NetworkBehaviour {

    public AudioSource explodeSource;
    public GameObject explosion;
    public float timeAlive;
    public float elapsedTime;
    RaycastHit hit;
    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 5000f);
        //explodeSource = gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

       
           

        



    }

    void OnCollisionEnter(Collision col)
    {
        if (isLocalPlayer)
        {
            if (isServer)
            {
                RpcSpawnExplosion();
            }
            else
            {
                CmdSpawnExplosion();
            }
        }
        Collider[] objects = UnityEngine.Physics.OverlapSphere(col.transform.position, 3f);
        foreach (Collider h in objects)
        {
            Debug.Log(h.gameObject.name);
            Rigidbody r = h.GetComponent<Rigidbody>();

            if (r != null)
            {
                
              
                    Debug.Log("Enemy Hit");
                    if (h.transform.GetComponent<PlayerStatus>() != null)
                       CmdPlayerHit(h.gameObject, 20);
                
            }



        }

        //explodeSource.Play();
        Destroy(gameObject);
    }

    [Command]
    void CmdSpawnExplosion()
    {
        GameObject ex = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
        NetworkServer.Spawn(ex);
    }

    [ClientRpc]
    void RpcSpawnExplosion()
    {
        GameObject ex = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
        NetworkServer.Spawn(ex);
    }

    [Command]
    void CmdPlayerHit(GameObject hit, int dmgAmt)
    {
        hit.GetComponent<PlayerStatus>().OnDamageTaken(dmgAmt, gameObject);
    }
}
