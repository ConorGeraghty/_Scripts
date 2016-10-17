using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CarArrowController : NetworkBehaviour {
	public GameObject arrow;
	public Transform target;
	private GameObject ourArrow;
	// Use this for initialization
	void Start () {
		if (!isLocalPlayer) {
			return;
		}
		ourArrow = Instantiate (arrow);
		ourArrow.GetComponent<indicator> ().player = this.gameObject.transform;
		CheckForTarget ();
	}

	public void SetTarget(Transform targ){
		if (targ == null)
			return;
		target = targ;
		ourArrow.GetComponent<indicator> ().target = this.target;
	}

	void CheckForTarget(){
		MeshRenderer[] mrs = ourArrow.GetComponentsInChildren<MeshRenderer>();
		Debug.Log (mrs[0] + " " +  mrs[1]);
		if (target == null) {
			foreach (MeshRenderer mr in mrs)
			{
				mr.enabled = true;
			}
		} else {
			foreach (MeshRenderer mr in mrs)
			{
				mr.enabled = false;
			}
		}
	}
}
