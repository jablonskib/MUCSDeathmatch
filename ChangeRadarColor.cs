using UnityEngine;
using System.Collections;

public class ChangeRadarColor : MonoBehaviour {
	public Material green, red, yellow;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void ChangeRed()
	{
		GetComponent<Renderer> ().material = red;
	}

	public void ChangeYellow()
	{
		GetComponent<Renderer> ().material = yellow;
	}

	public void ChangeGreen()
	{
		GetComponent<Renderer> ().material = green;
	}
}
