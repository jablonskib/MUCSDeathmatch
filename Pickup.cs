using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour 
{
	float rate = 60f;
    public string weaponName;
	// Use this for initialization
	void Start () 
    {
		Destroy (gameObject, 60f);

	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            col.transform.BroadcastMessage("Pickup", weaponName);
			gameObject.SetActive (false);
        }
    }

	public void Reset()
	{
		gameObject.SetActive (true);
	}
}
