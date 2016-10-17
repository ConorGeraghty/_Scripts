// Expose center of mass to allow it to be set from
// the inspector.
var com: Vector3;
var rb: Rigidbody;

function Start() 
{
	rb = GetComponent.<Rigidbody>();
	rb.centerOfMass = com;
}