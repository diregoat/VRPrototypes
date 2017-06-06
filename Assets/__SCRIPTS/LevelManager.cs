using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public GameObject gameManager;
	public GameObject worldManager;

	public GameObject player;
	public Vector3 playerPos;
	public GameObject currentLane;
	public GameObject newTrack;

	public GameObject track; //empty GameObjecto hold each track in
	//GameObjects to spawn on track
	public GameObject DW_bridge1;
	public GameObject DW_cell1;

	public GameObject laneObject;
	public int laneNameNum = 0;

	public int numPlatforms = 30;
	public int numCells = 100;
	public GameObject[] platformArray;

	public int bridgeNameNum = 0;
	public int cellNameNum = 0;

	public float bridge_zSpawnDistance;
	public float cell_zSpawnDistance;
	public float cell2_zSpawnDistance = 20.0f;
	public float cell_randomYSpawnDistance = 0.0f;

	public float timeLeft = 5.0f;

	public float newZSpawnDistance = 0.0f;


	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager");
		worldManager = GameObject.Find("WorldManager");

		player = GameObject.Find("Player");
		newTrack = Instantiate(track,new Vector3(0,0,newZSpawnDistance),Quaternion.identity,transform) as GameObject;
		newTrack.name = "newTrack";

		SpawnLevelLanes(0);
		SpawnTrack(0,0);
		SpawnTrack(1,1);
		SpawnTrack(1,2);
	}
	
	// Update is called once per frame
	void Update () {
		if(player.transform.position.z > 200){
			
		}

		CheckPlayersPosition();
	}

	void CheckPlayersPosition(){
		playerPos = player.transform.position;
	}

	void SpawnTrack(int trackType, int trackPos){  //0 = Bridges , 1 = Cells, 2 = Runways, 3 = Columns, etc..
		//IF Bridges
		if(trackType == 0 && trackPos == 0){
			platformArray = new GameObject[numPlatforms];
			for(int i = 0; i <numPlatforms; i++){
				GameObject platform = Instantiate(DW_bridge1, new Vector3(0,0,bridge_zSpawnDistance), Quaternion.identity) as GameObject;
				platform.transform.parent = newTrack.transform;
				string nameString = "DW_Bridge_" + bridgeNameNum.ToString();
				platform.name = nameString;
				bridgeNameNum += 1;
				platformArray[i] = platform;
				bridge_zSpawnDistance += 100.0f;
			}
		}

		//IF Cells
		if(trackType == 1 && trackPos == 1){
			platformArray = new GameObject[numPlatforms];
			for(int i = 0; i <numPlatforms; i++){
				int randomX = Random.Range(0,2);
				int randomY = Random.Range(0,3);

				float cell_randomXSpawnDistance = 0.0f;
				if(randomX == 0){
					cell_randomXSpawnDistance = 100.0f;
				}
				else{
					cell_randomXSpawnDistance = -100.0f;
				}

				if(randomY == 0){
					cell_randomYSpawnDistance = 0.0f;
				}
				if(randomY == 1){
					cell_randomYSpawnDistance = 10.0f;
				}
				if(randomY == 2){
					cell_randomYSpawnDistance = 20.0f;
				}

				GameObject platform = Instantiate(DW_cell1, new Vector3(cell_randomXSpawnDistance,cell_randomYSpawnDistance,cell_zSpawnDistance), Quaternion.identity) as GameObject;
					platform.transform.parent = newTrack.transform;
					string nameString = "DW_Cell_" + cellNameNum.ToString();
					platform.name = nameString;
					cellNameNum += 1;
					platformArray[i] = platform;
					cell_zSpawnDistance += 100.0f;
			}
		}
		if(trackType == 1 && trackPos == 2){
			platformArray = new GameObject[numPlatforms];
			for(int i = 0; i <numPlatforms; i++){
				int randomX = Random.Range(0,2);
				float cell_randomXSpawnDistance = 0.0f;
				if(randomX == 0){
					cell_randomXSpawnDistance = 200.0f;
				}
				else{
					cell_randomXSpawnDistance = -200.0f;
				}
				GameObject platform = Instantiate(DW_cell1, new Vector3(cell_randomXSpawnDistance,0,cell2_zSpawnDistance), Quaternion.identity) as GameObject;
					platform.transform.parent = newTrack.transform;
					string nameString = "AW_Cell_" + cellNameNum.ToString();
					platform.name = nameString;
					cellNameNum += 1;
					platformArray[i] = platform;
					cell2_zSpawnDistance += 88.0f;
			}
		}
	}

	void SpawnLevelLanes(int laneType){ // 0 = Side Lanes, 1 = Height Lanes
		
		if(laneType == 0){
			GameObject laneCenter = GameObject.Instantiate(laneObject, new Vector3(0,0,1500), Quaternion.identity) as GameObject;
			laneCenter.name = "laneCenter_0" + laneNameNum.ToString();
			laneCenter.transform.parent = newTrack.transform;
			GameObject laneRight1 = GameObject.Instantiate(laneObject, new Vector3(100,0,1500), Quaternion.identity) as GameObject;
			laneRight1.name = "laneRight_0" + laneNameNum.ToString();
			laneRight1.transform.parent = newTrack.transform;
			GameObject laneRight2 = GameObject.Instantiate(laneObject, new Vector3(200,0,1500), Quaternion.identity) as GameObject;
			laneRight2.name = "laneFarRight_0" + laneNameNum.ToString();
			laneRight2.transform.parent = newTrack.transform;
			GameObject laneLeft1 = GameObject.Instantiate(laneObject, new Vector3(-100,0,1500), Quaternion.identity) as GameObject;
			laneLeft1.name = "laneLeft_0" + laneNameNum.ToString();
			laneLeft1.transform.parent = newTrack.transform;
			GameObject laneLeft2 = GameObject.Instantiate(laneObject, new Vector3(-200,0,1500), Quaternion.identity) as GameObject;
			laneLeft2.name = "laneFarLeft_0" + laneNameNum.ToString();
			laneLeft2.transform.parent = newTrack.transform;

			laneNameNum += 1;
		}
	}
}
