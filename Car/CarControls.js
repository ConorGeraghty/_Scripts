//CarController1.js
var currentPower:float;
var wheels : Transform[];
private var engineRPM : float;
var speedIcon:Texture2D;
var power=0.0;
var brake=0.0;
var steer=0.0;
var pitchAdj= 5;
var body:Rigidbody;
var maxSteer=35.0;
var mphDisplay:String;
var timer: float = 300; 
var timerText:String;
var fuel:float=100;
var fuelText:String;
var gear:int=1;
//engine powerband
var minRPM = 400;
var maxRPM = 8000;
//maximum Engine Torque
var maxTorque = 400.0;
//automatic transmission shift points
var shiftDownRPM = 1500;
var shiftUpRPM = 7000;
//gear ratios
var gearRatios = [-1.66, 2.16, 1.48, 1.15, 1.00];
var finalDriveRatio = 3.4;
private var handbrake = 0.0;
private var wheelRPM : float;
var skidMarkPrefab : GameObject;
//var audio: AudioSource;

function Start()
{
    //this.GetComponent.<Rigidbody>().centerOfMass = Vector3(0,-0.5,0.3);
    body.centerOfMass = Vector3(0,-0.5,0.4);
}
 
function Update () 
{
	
    currentPower = CalcEngine();
  	wheelRPM= body.velocity.magnitude*60.0*0.5;
	steer=Input.GetAxis("Horizontal") * maxSteer;
	
	//XBOX360 CONTROLLER
    if(Input.GetAxis("triggers") > 0.2)
    {
    	power=Input.GetAxis("triggers") * CalcEngine() * Time.deltaTime * 200.0; //controller right trigger (360-win10)
    	motor=Input.GetAxis("triggers") * CalcEngine() * Time.deltaTime * 200.0; //controller right trigger (360-win10)

    	fuel -= 0.01  * Input.GetAxis("triggers");
    }
    else if(Input.GetAxis("triggers") < 0)
    { 
    	brake= (Input.GetAxis("triggers") * -1) ? body.mass * 0.9: 0.1;
    }
    else
    {
    	power=0.0;
		brake=0.0;
    }
	
	
	
    /*KEYBOARD CONTROLS
    
    //motor=Input.GetAxis("triggers") * CalcEngine() * Time.deltaTime * 200.0; 
    power=Input.GetAxis("triggers") * CalcEngine() * Time.deltaTime * 200.0; 
      
    steer=Input.GetAxis("Horizontal") * maxSteer;
    //brake=Input.Get.KeyCode("space") ? body.mass * 0.6: 0.1;
    */
    
    var mph = body.velocity.magnitude * 2.237;
	mphDisplay = mph.ToString("F2") + " MPH";
	fuelText = fuel.ToString("F2");
	
	
    GetCollider(0).steerAngle=steer;
    GetCollider(1).steerAngle=steer;
    ApplyLocalPositionToVisuals(GetCollider(0));
    ApplyLocalPositionToVisuals(GetCollider(2));
    ApplyLocalPositionToVisuals(GetCollider(1));
    ApplyLocalPositionToVisuals(GetCollider(3));

	timer -= Time.deltaTime; // I need timer which from a particular time goes to zero
     
     if (timer > 0)
     {
         timerText = timer.ToString("F0");
     } 
     else // timer is <= 0
     {
         timerText = "TIME OVER\nPress X to restart"; // when it goes to the end-0,game ends (shows text: time over...) 
         
         if (Input.GetKeyDown("x")) // And then i can restart game: pressing restart.
         { 
             Application.LoadLevel(Application.loadedLevel); // reload the same level
         }
   	}
    if(brake > 0.0)
    {
        GetCollider(0).brakeTorque=brake;
        GetCollider(1).brakeTorque=brake;
        GetCollider(2).brakeTorque=brake;
        GetCollider(3).brakeTorque=brake;
        GetCollider(0).motorTorque=0.0;
        GetCollider(1).motorTorque=0.0;
        GetCollider(2).motorTorque=0.0;
        GetCollider(3).motorTorque=0.0;
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
    }/*
    skidmarks(GetCollider(0));
    skidmarks(GetCollider(1));*/
    skidmarks(GetCollider(2));
    skidmarks(GetCollider(3));
	
    
}
 
function GetCollider(n : int) : WheelCollider
	{
	    return wheels[n].gameObject.GetComponent(WheelCollider);    
	}

//ROTATE WHEEL MESHES WITH COLLIDERS
function ApplyLocalPositionToVisuals(coll:WheelCollider)
	{
		 if (coll.transform.childCount == 0) 
		 {
		 	return;
	 	 }
		 var visualWheel:Transform = coll.transform.GetChild(0);
		 var position : Vector3;
		 var quat: Quaternion;
		 //(out position:Vector3, out quat:Quaternion);
		 coll.GetWorldPose(position, quat);
         visualWheel.transform.position = position;
         visualWheel.transform.rotation = quat;
         
		 
 }
 
 //TEMP HUD
 function OnGUI()
{
	
    	GUI.Box(new Rect((Screen.width - 200), 50, 100, 80), GUIContent(mphDisplay + "\n" + timerText + "\nFuel: " + fuelText +"\nGear: " + gear, speedIcon));
    
    	
}

//AUTO TRANSMISSION
function AutomaticTransmission()
{
   if(gear>0)
   {
     if(engineRPM>shiftUpRPM&&gear<gearRatios.length-1)
         gear++;
     if(engineRPM<shiftDownRPM&&gear>1)
         gear--;
   }
   //Debug.Log(gear);
}

//RUDIMENTARY POWER CALCULATOR
function CalcEngine() : float
{
   //no enginewhen braking
   if(Input.GetAxis("triggers") > 0.2)
    {
    	//power=Input.GetAxis("triggers") * CalcEngine() * Time.deltaTime * 200.0; //controller right trigger (360-win10)
    	motor=Input.GetAxis("triggers") * Time.deltaTime * 10; //controller right trigger (360-win10)

    	fuel -= 0.01  * Input.GetAxis("triggers");
    }
    else if(Input.GetAxis("triggers") < 0)
    { 
    	brake= (Input.GetAxis("triggers") * -1) ? body.mass * 0.9: 0.1;
    }
    else
    {
    	motor=0.0;
		brake=0.0;
    }
	
	
//CALCULATE GEAR AND RPM

     AutomaticTransmission();
     engineRPM=wheelRPM*gearRatios[gear]*finalDriveRatio;
     /*
     //ADJUST PITCH OF ENGINE
     //if car isairborne, just rev engine
   	if(!(GetCollider(0).IsGrounded)&&!(GetCollider(1).IsGrounded)&& !(GetCollider(2).IsGrounded) && !(GetCollider(3).IsGrounded))
   {
      engineRPM+= (motor-0.3)*25000.0*Time.deltaTime;
      engineRPM= Mathf.Clamp(engineRPM,minRPM,maxRPM);
      return 0;
   }
   else
   {
   */
   
    GetComponent.<AudioSource>().pitch = Mathf.Abs(engineRPM/maxRPM) + 1.25f;
    if (GetComponent.<AudioSource>().pitch > 3.0f) 
    {
        GetComponent.<AudioSource>().pitch = 3.0f;
    }
     //Debug.Log(engineRPM);     
	     
	 
	 
	 
	 if(engineRPM<minRPM)
     	engineRPM=minRPM;//KEEPIDLE
     if(engineRPM<maxRPM)
      {
         //fakea basic torque curve
        var x =(2*(engineRPM/maxRPM)-1);
        torqueCurve = 0.5*(-x*x+2);
        torqueToForceRatio = gearRatios[gear]*finalDriveRatio/0.5;
        return motor*maxTorque*torqueCurve*torqueToForceRatio;
      }
      
      else
         //rpmdelimiter
         return 0;
   //}
}
//COLLISION HANDLING
function OnCollisionEnter(other:Collision)
{

  	if(other.gameObject.name=="Fuel")
  	{
  		fuel += 25;
  		Destroy(other.gameObject);
	}
}

//UPDATE SKIDMARKSPER WHEEL PASSED

function skidmarks(coll:WheelCollider)
{
// define a hit point for the raycast collision
	var hit : RaycastHit;
	// Find the collider's center point, you need to do this because the center of the collider might not actually be
	// the real position if the transform's off.
	var ColliderCenterPoint : Vector3 = coll.transform.TransformPoint( coll.center );
	
	// now cast a ray out from the wheel collider's center the distance of the suspension, if it hit something, then use the "hit"
	/* variable's data to find where the wheel hit, if it didn't, then se tthe wheel to be fully extended along the suspension.
	if 
	( Physics.Raycast( ColliderCenterPoint, -coll.transform.up, hit, coll.suspensionDistance + coll.radius ) ) 
	{
		skidMarkPrefab.transform.localPosition = hit.point + (coll.transform.up * coll.radius);
	}
	else
	{*/
		skidMarkPrefab.transform.localPosition = hit.point;//ColliderCenterPoint - (coll.transform.up * coll.suspensionDistance);
	//}
	
	// define a wheelhit object, this stores all of the data from the wheel collider and will allow us to determine
	// the slip of the tire.
	var CorrespondingGroundHit : WheelHit;
	coll.GetGroundHit( CorrespondingGroundHit );
	
	// if the slip of the tire is greater than 2.0, and the slip prefab exists, create an instance of it on the ground at
	// a zero rotation.
	if ( Mathf.Abs( CorrespondingGroundHit.sidewaysSlip ) > 0.1)
		{
			//skidMarkPrefab.transform.position = hit.transform.localPosition;			
			skidMarkPrefab.gameObject.SetActive(true);
			Debug.Log(skidMarkPrefab.gameObject.activeSelf);
		}
	else if ( Mathf.Abs( CorrespondingGroundHit.sidewaysSlip ) <= 0.75)
		{
			skidMarkPrefab.gameObject.SetActive(false);
		}
	
}
