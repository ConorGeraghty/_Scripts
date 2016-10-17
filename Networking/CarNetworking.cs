using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CarNetworking : NetworkBehaviour {
	[SerializeField] Camera cam;
	[SerializeField] CarController cc;

	public string playerName = "Player 1";
	private string gameStatus = "Waiting for players...";

	private float lapTime = 0.0f;

	void Start () {
		if (!isLocalPlayer) {
			cc.enabled = false;
			cam.enabled = false;
			return;
		}
	}

	void OnGUI()
	{
		if (!isLocalPlayer) {
			return;
		}
		//Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
		//GUI.Label(new Rect(pos.x-30, Screen.height - pos.y - 50, 100, 30), playerName);
		GUI.Label(new Rect(100,150, 100, 50), "LAP TIME : " + lapTime.ToString("F2") + "s");
		GUI.Label (new Rect (400, 100, 150, 20),gameStatus);
	}
	
	public float GetLapTime(){
		return lapTime;
	}
	
	public void SetLapTime(float t){
		lapTime = t;
	}
	
	public void SetGameStatus(string str){
		gameStatus = str;
	}

	void Update(){
		lapTime += Time.deltaTime;
	}
}
