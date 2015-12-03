using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour {


	bool scratchTrue = true;
	bool positionCueTrue = false;
	bool selectBallTrue = false;
	
	GameObject cueBall;
	GameObject cueStick;
	
	Rigidbody cueBallRB;
	Rigidbody cueStickRB;
	
	Collider cueCollider;
	Collider cueStickCollider;
	
	Vector3 origCueBallPos;
	
	Vector3 currentDirection;
	Vector3 selectedBall;
	
	GameControl gc;

	int stripeCount;
	int solidCount;

	public GameObject myo = null;
	bool myoIsPaired = false;
	ThalmicHub hub;
	ThalmicMyo thalmicMyo;

	GeneralInfo generalInfo;
	
	// Use this for initialization
	void Start ()
	{
		selectedBall = GameObject.Find("1").GetComponent<Transform>().position;
		solidCount = stripeCount = 7;        
		gc = GameControl.getInstance ();
		generalInfo = GeneralInfo.getInstance();
		getCueInfo ();
		getCueStickInfo();
		setCueStick();		
		//MYO TESTS
		//cueStickRB.constraints = RigidbodyConstraints.FreezePositionY;
		//cueStickCollider.enabled = false;
		ThalmicHub hub = ThalmicHub.instance;
		thalmicMyo = myo.GetComponent<ThalmicMyo> ();
		
	}
	
	void Update ()
	{
		
		if (Input.GetKeyDown ("q")) {
			hub.ResetHub();
		}
		if (scratchTrue == true && Input.GetKeyDown ("space")) {
			positionCueTrue = true;
		}
		if (scratchTrue && positionCueTrue) {          			
			moveCueBallAfterScratch();			
			
			if (Input.GetKeyDown (KeyCode.C)) {
				stopMovingCueBallAfterScratch();
				selectBallTrue = true;
			}
		}
		if (selectBallTrue) {
			selectBall();
			
			
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			selectBallTrue = true;
		}
		if (Input.GetKeyDown (KeyCode.Y)) {
			shoot();
		}
		if (positionCueTrue) {
			selectBall();
			setCueStick();
		}		

		//Debug.Log ("Ball Control accel " + myoAcceleration());
	}

	void OnGUI ()
	{
		GUI.skin.label.fontSize = 20;
		
		hub = ThalmicHub.instance;
		
		// Access the ThalmicMyo script attached to the Myo object.
		thalmicMyo = myo.GetComponent<ThalmicMyo> ();
		
		if (!hub.hubInitialized) {
			GUI.Label(new Rect (12, 8, Screen.width, Screen.height),
			          "Cannot contact Myo Connect. Is Myo Connect running?\n" +
			          "Press Q to try again."
			          );
		} else if (!thalmicMyo.isPaired) {
			GUI.Label(new Rect (12, 8, Screen.width, Screen.height),
			          "No Myo currently paired."
			          );
		} else if (!thalmicMyo.armSynced) {
			GUI.Label(new Rect (12, 8, Screen.width, Screen.height),
			          "Perform the Sync Gesture."
			          );
		} else {
			myoIsPaired = true;
			/*
			GUI.Label (new Rect (12, 8, Screen.width, Screen.height),

			           "Fist: Vibrate Myo armband\n" +
			           "Wave in: Set box material to blue\n" +
			           "Wave out: Set box material to green\n" +
			           "Double tap: Reset box material\n" +
			           "Fingers spread: Set forward direction"
			           );
			*/
		}
	}
	
	void shoot (){
		generalInfo.setCuePosition(cueStick.transform.position);
		cueStickRB.constraints = RigidbodyConstraints.None;
		Vector3 velocity =   (cueBallRB.transform.position - cueStickRB.transform.position) * 2f;

		cueStickRB.velocity = velocity*30;
		selectBallTrue = false;
	}
	
	void OnTriggerEnter (Collider col)
	{   
		if (col.gameObject.transform.name == "cueStick") {
			return;
		}
		if (col.gameObject.transform.name == "cueBall") {
			resetCueBall (col);            
		} else {
			if (col.gameObject.transform.tag == "solid") {
				solidCount--;
			}
			if (col.gameObject.transform.tag == "stripe") {
				stripeCount--;
			}
			Destroy (col.gameObject);
		}
	}
	
	void selectBall() {
		Ray ballRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ballRay, out hit)) {
			if ((hit.collider.tag == "solid" || hit.collider.tag == "stripe") && Input.GetMouseButton (0)) {
				selectedBall = hit.collider.transform.position;
				setCueStick();
			}
		}			
	}
	
	void setCueStick() {
		
		Vector3 direction = cueBallRB.transform.position - selectedBall;
		direction.y += .2f; //offset to keep from hitting table
		
		// Changes stick rotation to be pointing at the target ball (red)
		cueStick.transform.rotation = Quaternion.LookRotation (direction); 
		
		// Moves stick to the cueball and then to the edge + 20% further so they're not touching
		cueStick.transform.position = cueBall.transform.position + direction.normalized * (cueStick.transform.localScale.z / cueBall.transform.localScale.z / 6 * 1.2f);
		
		cueStickRB.constraints = RigidbodyConstraints.FreezeAll;
		cueCollider.enabled = true;
		cueStickCollider.enabled = true;
		
	}
	
	void moveCueBallAfterScratch() {
		cueStickRB.constraints = RigidbodyConstraints.FreezeAll;
		
		cueCollider.enabled = false;
		findMouseHitPoint ();
		
		Vector3 currentCueBallPos = cueBallRB.transform.position;		
		Vector3 newCueBallPos = cueBallRB.transform.position + currentDirection;
		
		currentCueBallPos = keepInKitchen(currentCueBallPos);
		newCueBallPos = keepInKitchen(newCueBallPos);
		
		cueBallRB.transform.position = Vector3.Lerp (currentCueBallPos, newCueBallPos, Time.deltaTime * 2);
		
	}
	
	void stopMovingCueBallAfterScratch() {
		scratchTrue = positionCueTrue = false;
		cueCollider.enabled = true;
	}
	
	void resetCueBall (Collider col)
	{
		cueBallRB.velocity = Vector3.zero;
		cueBallRB.angularVelocity = Vector3.zero;
		col.gameObject.transform.position = origCueBallPos;  
		scratchTrue = true;
	}
	
	bool findMouseHitPoint ()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;
		int floorMask = LayerMask.GetMask ("TableFloor");
		if (Physics.Raycast (camRay, out floorHit)) {
			Vector3 mouseHit = floorHit.point - cueBallRB.transform.position;
			
			//keepInKitchen(mouseHit);
			currentDirection = mouseHit;
			return true;
		}
		return false;
	}
	
	Vector3 keepInKitchen (Vector3 position)
	{
		position.y = .8f;
		if (position.x > 2.25f)
			position.x = 2.25f;
		else if (position.x < 1.8f)
			position.x = 1.8f;
		if (position.z > 3.66f)
			position.z = 3.66f;
		else if (position.z < 2.6f)
			position.z = 2.6f;
		return position;
	}
	
	void getCueInfo ()
	{
		cueBall = GameObject.Find ("cueBall"); 
		cueBallRB = cueBall.GetComponent<Rigidbody> ();
		origCueBallPos = cueBall.transform.position;
		cueCollider = cueBall.GetComponent<Collider> ();
	}
	
	void getCueStickInfo () {		
		cueStick = GameObject.Find("cueStick");
		cueStickRB = cueStick.GetComponent<Rigidbody>();
		cueStickCollider = cueStick.GetComponent<Collider>();
	}
	
	void moveCueWithMyo() {
		cueStick.transform.RotateAround (cueStick.transform.position, Vector3.up, 0.3f); 
	}

	public float myoAcceleration() {
		if (myoIsPaired) {
			//myoRotationVal = myo.GetComponent<Quaternion>();
			Vector3 accel = thalmicMyo.accelerometer;
			float accelx = Mathf.Pow(accel.x, 2);
			float accely = Mathf.Pow(accel.y, 2);
			float accelz = Mathf.Pow(accel.z, 2);
			float acceleration = Mathf.Sqrt(accelx + accely + accelz);
			return acceleration;
		}
		else {
			return 0f;
		}
	}
}