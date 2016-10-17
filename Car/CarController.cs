using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour 
{
	//CarController1.js
	public Transform[] wheels;
	private float engineRPM;
	Texture2D speedIcon;
	float brake = 0.0f;
	float steer = 0.0f;
	public Rigidbody body;
	float maxSteer = 35.0f;
	string mphDisplay;
	float timer = 300; 
	float motor = 0.0f;
	float fuel=100;
	string fuelText;
	public float AntiRollA = 7000.0f;
	
	private float wheelRPM;
	//engine powerband
	float minRPM = 3000;
	float maxRPM = 14000;
	//maximum Engine Torque
	public float maxTorque = 800.0f;
	//automatic transmission shift points
	float shiftDownRPM = 8000;
	float shiftUpRPM = 13250;
	public int gears = 5;
	int gear=1;
	public float[] gearRatios;
	public float finalDriveRatio = 4.4f;
	//public GameObject skidMarkPrefab;
	
	
	void Start ()
	{	    
		body.centerOfMass = new Vector3(0,-0.5f,0.4f);
	}
	
	void Update ()
	{

		
		
		wheelRPM= body.velocity.magnitude*60.0f*0.5f;
		steer=Input.GetAxis("Horizontal") * maxSteer;	    
		
		fuelText = fuel.ToString("F2");
		
		
		GetCollider(0).steerAngle=steer;
		GetCollider(1).steerAngle=steer;
		
		
		ApplyLocalPositionToVisuals(GetCollider(0));
		ApplyLocalPositionToVisuals(GetCollider(2));
		ApplyLocalPositionToVisuals(GetCollider(1));
		ApplyLocalPositionToVisuals(GetCollider(3));
		
		timer -= Time.deltaTime; // I need timer which from a particular time goes to zero
		
		
		/*
	     if (timer > 0)
	     {
	         timerText = timer.ToString("F0");
	     } 
	     else // timer is <= 0
	     {
	         timerText = "TIME OVER\nPress X to restart"; // when it goes to the end-0,game ends (shows  time text   over...) 
	         
	         if (Input.GetKeyDown("x")) // And then i can restart game: pressing restart.
	         { 
	             Application.LoadLevel(Application.loadedLevel); // reload the same level
	         }
	   	}

		*/
		
		
		if(brake > 0.0f)
		{
			GetCollider(0).brakeTorque=brake;
			GetCollider(1).brakeTorque=brake;
			GetCollider(2).brakeTorque=brake;
			GetCollider(3).brakeTorque=brake;
			GetCollider(0).motorTorque=0.0f;
			GetCollider(1).motorTorque=0.0f;
			GetCollider(2).motorTorque=0.0f;
			GetCollider(3).motorTorque=0.0f;
		} 
		else 
		{
			GetCollider(0).brakeTorque=0;
			GetCollider(1).brakeTorque=0;
			GetCollider(2).brakeTorque=0;
			GetCollider(3).brakeTorque=0;
			GetCollider(0).motorTorque=CalcEngine();
			GetCollider(1).motorTorque=CalcEngine();
			GetCollider(2).motorTorque=CalcEngine();
			GetCollider(3).motorTorque=CalcEngine();
			
		}
		//ANTIROLL = MAKE SURE 0,2 = LEFT, 1,3 = RIGHT
		AntiRoll(GetCollider(0),GetCollider(1));
		AntiRoll(GetCollider(2),GetCollider(3));
		
		skidmarks(GetCollider(0));
		skidmarks(GetCollider(1));
		skidmarks(GetCollider(2));
		skidmarks(GetCollider(3));
		
		
	}
	
	WheelCollider GetCollider ( int n  )
	{
		return wheels[n].gameObject.GetComponent<WheelCollider>();    
	}
	
	//ROTATE WHEEL MESHES WITH COLLIDERS
	void ApplyLocalPositionToVisuals ( WheelCollider coll  )
	{
		if (coll.transform.childCount == 0) 
		{
			return;
		}
		Transform visualWheel = coll.transform.GetChild(0);
		Vector3 position;
		Quaternion quat;
		//(out  Vector3 position ,  out  Quaternion quat  );
		coll.GetWorldPose(out position, out quat);
		visualWheel.transform.position = position;
		visualWheel.transform.rotation = quat;
		
		
	}
	
	
	void OnGUI()
	{
		float mph = body.velocity.magnitude * 2.237f;
		GUI.Box(new Rect((Screen.width - 200), 50, 150, 100),
		        new GUIContent(mph.ToString("F2") + "\nFuel: " + fuelText +"\nGear: " + gear + "\n engine power: " + CalcEngine() + "\n curr rpm: " + engineRPM , speedIcon));
		
		
	}
	
	
	//AUTO TRANSMISSION
	void AutomaticTransmission ()
	{
		if(gear>0)
		{
			if(engineRPM>shiftUpRPM&&gear<gearRatios.Length-1)
				gear++;
			if(engineRPM<shiftDownRPM&&gear>1)
				gear--;
		}
		
	}
	void ManualTransmission ()
	{
		if(gear < 0)
		{
			gear = 0;
		}
		else if(gear > gearRatios.Length - 1)
		{
			gear = gearRatios.Length - 1;
		}
		
	}
	
	//RUDIMENTARY POWER CALCULATOR
	float CalcEngine ()
	{
		//no engine when braking
		if(Input.GetAxis("Vertical") > 0.2f)
		{

			motor = Input.GetAxis("Vertical") * Time.deltaTime * 10; //controller right trigger (360-win10)
			fuel -= 0.01f  * Input.GetAxis("Vertical");
		}
		else if(Input.GetAxis("Vertical") < 0 )
		{ 
			if(Input.GetAxis("Vertical") < -0.02)
			{
				motor = 0.0f;
				brake= body.mass * 0.3f;
			}
			else
			{
				motor = 0.0f;
				brake = body.mass * 0.1f;
			}	
		}
		else
		{
			motor=0.0f;
			brake=0.0f;
		}
		
		
		//CALCULATE GEAR AND RPM
		
		AutomaticTransmission();
		
		engineRPM = wheelRPM * gearRatios[gear] * finalDriveRatio;
		
		if(engineRPM<minRPM)
			engineRPM=minRPM;//KEEPIDLE
		
		
		if(engineRPM<maxRPM)
		{
			//fake a basic torque curve
			float x=(2*(engineRPM/maxRPM)-1);
			float torqueCurve = 0.5f*(-x*x+2);
			float torqueToForceRatio = gearRatios[gear]*finalDriveRatio/0.5f;
			return motor*maxTorque*torqueCurve*torqueToForceRatio;
		}	      
		else
			//rpmdelimiter
			return 0;
	}
	
	//COLLISION HANDLING
	void OnCollisionEnter ( Collision other  )
	{
		if(other.gameObject.name=="Fuel")
		{
			fuel += 25;
			Destroy(other.gameObject);
		}
	}
	
	//"ROLLBARS"
	void AntiRoll(WheelCollider WheelL, WheelCollider WheelR)
	{
		
		WheelHit hit; 
		float travelL = 1.0f; 
		float travelR = 1.0f;
		
		bool groundedL= WheelL.GetGroundHit(out hit);   
		if (groundedL) 
			travelL = (-WheelL.transform.InverseTransformPoint(hit.point).y - WheelL.radius) 
				/ WheelL.suspensionDistance;
		
		bool groundedR= WheelR.GetGroundHit(out hit); 
		if (groundedR) 
			travelR = (-WheelR.transform.InverseTransformPoint(hit.point).y - WheelR.radius) 
				/ WheelR.suspensionDistance;
		
		float antiRollForce= (travelL - travelR) * AntiRollA;
		
		if (groundedL) 
			body.AddForceAtPosition(WheelL.transform.up * -antiRollForce, WheelL.transform.position); 
		if (groundedR) 
			body.AddForceAtPosition(WheelR.transform.up * antiRollForce, WheelR.transform.position); 
	}
	
	
	
	//UPDATE SKIDMARKS PER WHEEL PASSED
	
	void skidmarks ( WheelCollider coll  )
	{
		
		
		// define a wheelhit object, this stores all of the data from the wheel collider and will allow us to determine
		// the slip of the tire.
		WheelHit CorrespondingGroundHit;
		coll.GetGroundHit(out CorrespondingGroundHit );
		
		// if the slip of the tire is greater than 2.0f, and the slip prefab exists, create an instance of it on the ground at
		// a zero rotation.
		if ( Mathf.Abs( CorrespondingGroundHit.sidewaysSlip ) > .6 )
		{
			//Instantiate (skidMarkPrefab, CorrespondingGroundHit.point, coll.transform.rotation);
			Debug.Log ("skidding");
		}
		else if ( Mathf.Abs( CorrespondingGroundHit.sidewaysSlip ) <= .45)
		{
			//skidMarkPrefab.gameObject.SetActive(false);
		}
	}
	
	
}

