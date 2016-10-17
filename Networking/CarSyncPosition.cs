using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CarSyncPosition : NetworkBehaviour {

	[SyncVar]
	Vector3 syncPos;

	[SerializeField] float lerpRate = 15f;
	[SerializeField] Transform myTransform;

	void FixedUpdate(){
		TransmitPosition ();
		LerpPosition ();
	}

	void LerpPosition(){
		if (!isLocalPlayer) {
			myTransform.position = Vector3.Lerp(myTransform.position, syncPos, lerpRate * Time.deltaTime);
		}
	}

	//This code is only run on the server, but is called from the client.
	[Command]
	void CmdProvidePositionToServer(Vector3 pos){
		syncPos = pos;
	}

	[ClientCallback]
	void TransmitPosition(){
		if (isLocalPlayer) {
			CmdProvidePositionToServer(myTransform.position);
		}
	}
}
