using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour 
{
    public GameObject[] spawnPoints;
    int spawnNum;
	// Use this for initialization
	void Start () 
    {

        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
       
        Debug.Log(spawnPoints.Length);
        spawnNum = Random.Range(0, spawnPoints.Length);
        transform.position = spawnPoints[spawnNum].transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
        if(Input.GetKeyDown(KeyCode.X))
        {
            if( spawnNum +1 > spawnPoints.Length )
            {
                spawnNum = 0;
                transform.position = spawnPoints[spawnNum].transform.position;
				Camera.main.transform.rotation = spawnPoints[spawnNum].transform.rotation;
            }
            else
            {
                transform.position = spawnPoints[spawnNum++].transform.position;
				Camera.main.transform.rotation = spawnPoints[spawnNum++].transform.rotation;
            }
        }
	}
}
