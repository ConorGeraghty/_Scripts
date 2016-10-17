using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CarSyncRotation : NetworkBehaviour {
	[SyncVar] private Quaternion syncCarRotation;
	
	[SerializeField] private Transform carTransform;
	[SerializeField] private float lerpRate = 15f;

	void LerpRotations(){
		if (!isLocalPlayer) {
			carTransform.rotation = Quaternion.Lerp(carTransform.rotation, syncCarRotation, lerpRate * Time.deltaTime);
		}
	}

	[Command]
	void CmdProvideRotationsToServer(Quaternion carRot){
		syncCarRotation = carRot;
	}

	[Client]
	void TransmitRotation(){
		if (isLocalPlayer) {
			CmdProvideRotationsToServer(carTransform.rotation);
		}
	}

	void FixedUpdate(){
		TransmitRotation ();
		LerpRotations ();
	}
}
