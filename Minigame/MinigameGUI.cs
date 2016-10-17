using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MinigameGUI : NetworkBehaviour {
	public int playersConnected;
	private GameObject[] playerList;
	bool minigameHasStarted = false;
	
	public int playersRequiredToStart = 2;
	//OnGUI should not be used for GUI stuff. It is purely for development/statistical purposes.
	void OnGUI(){
		if (GameObject.FindGameObjectsWithTag ("Player") != null) {
			playersConnected = GameObject.FindGameObjectsWithTag ("Player").Length;
			GUI.Label (new Rect (10, 10, 150, 20),"Players connected: " + playersConnected.ToString());
		}
	}

	/// <summary>
	/// Starts the minigame. Should be called once there are a sufficient amount of players connected or after a certain time.
	/// </summary>
	void StartMinigame(){
		//we could potentially have this method take in a variable that starts different types of minigame
		if (minigameHasStarted) {
			return;
		}
		minigameHasStarted = true;
		foreach (GameObject go in playerList){
			go.GetComponent<CarNetworking>().SetGameStatus("RACING!");
			//StartLapTime();
		}
		//Now we've got to get our racing track. This should be a gameobject with an array of different targets, and as we meet each
		//each target we cycle through to the next target. However, this should be on the player as the checkpoints are different for each player.
	}

	//Here, we should check for the right amount of players needed
	void Update(){
		playerList = GameObject.FindGameObjectsWithTag ("Player");
		if (playersConnected >= playersRequiredToStart) {
			StartMinigame();
		}
	}
}
