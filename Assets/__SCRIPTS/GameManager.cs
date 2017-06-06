using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject worldManager;
	public GameObject levelManager;

	// Use this for initialization
	void Start () {
		worldManager = GameObject.Find("WorldManager");
		levelManager = GameObject.Find("LevelManager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
