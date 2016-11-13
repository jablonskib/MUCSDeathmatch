using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadTestMap : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void LoadTestMapOffline()
    {
        SceneManager.LoadScene("Test SceneOffline");
    }
}
