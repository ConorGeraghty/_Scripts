using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class CarC : MonoBehaviour 
{
	public WheelCollider leftWheel;
	public WheelCollider rightWheel;
	public WheelCollider backLeftWheel;
	public WheelCollider backRightWheel;
	public bool motor; // is this wheel attached to motor?
	public bool steering; // does this wheel apply steer angle?
}


public class SimpleCarController : MonoBehaviour 
{
	public List<CarC> axleInfos; // the information about each individual axle
		public float maxMotorTorque = 600.0f; // maximum torque the motor can apply to wheel
		public float maxSteeringAngle = 30.0f; // maximum steer angle the wheel can have
	public void FixedUpdate()
	{
		float motor = maxMotorTorque * Input.GetAxis("Vertical");
		float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
		foreach (CarC axleInfo in axleInfos) {
			if (axleInfo.steering) {
				axleInfo.leftWheel.steerAngle = steering;
				axleInfo.rightWheel.steerAngle = steering;
				axleInfo.backLeftWheel.steerAngle = steering;
				axleInfo.backRightWheel.steerAngle = steering;
			}
			if (axleInfo.motor) {
				axleInfo.leftWheel.motorTorque = motor;
				axleInfo.rightWheel.motorTorque = motor;
				axleInfo.backLeftWheel.motorTorque = motor;
				axleInfo.backRightWheel.motorTorque = motor;
			}
		}
	}
}
/*
public class GetCollider(n : int)
{
	return wheels[n].gameObject.GetComponent(WheelCollider);
}
*/