using UnityEngine;
using System.Collections;

public class CarMinigame : MonoBehaviour {
	Transform[] ourLocalTrack;
	CarArrowController arrowControllerScript;
	public int ourTargetCheckpoint = 0;

	void Awake(){
		SetTrack ();
	}

	void Start(){
		arrowControllerScript = this.gameObject.GetComponent<CarArrowController> ();
		SetNewTarget (ourTargetCheckpoint);
	}

	void SetNewTarget(int pos){
		arrowControllerScript.SetTarget (ourLocalTrack[pos]);
	}

	void SetTrack(){
		//ALL TRACKS MUST BE CALLED "Track" AND THERE MUST ONLY BE ONE IN EACH LEVEL
		if (GameObject.Find ("Track") != null) {
			ourLocalTrack = GameObject.Find ("Track").GetComponent<Track>().GetCheckpoints();
		}
	}

	void WinGame(){
		Debug.Log ("Player won won");
	}

	void OnTriggerEnter(Collider checkpoint){
		//if the collider is next in our track, cycle to next target
		//if there is no next we've finished
		Debug.Log ("triggered");

		//is it a checkpoint
		if (checkpoint.tag == "Checkpoint") {
			//is it the correct checkpoint
			if (ourTargetCheckpoint < ourLocalTrack.Length){
				if (checkpoint.transform == ourLocalTrack[ourTargetCheckpoint]){
					ourTargetCheckpoint++;
					if (!(ourTargetCheckpoint >= ourLocalTrack.Length))
					{
						SetNewTarget (ourTargetCheckpoint);
					}
					else
					{
						WinGame();
					}
				}
			}
		}
	}
}
