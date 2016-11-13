using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {


    public float health;

	// Use this for initialization
	void Start () 
    {
        health = 100;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(health <= 0)
        {
            Destroy(gameObject);
        }
	}

    void Damage(float d)
    {
        health -= d;
    }
}
