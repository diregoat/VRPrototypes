using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

	public GameObject gameManager;
	public GameObject levelManager;

	public int currentWorldType = 0;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager");
		levelManager = GameObject.Find("LevelManager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
