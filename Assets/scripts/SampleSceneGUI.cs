using UnityEngine;
using System.Collections;

// Draw simple instructions for sample scene.
// Check to see if a Myo armband is paired.
public class SampleSceneGUI : MonoBehaviour
{
	// Myo game object to connect with.
	// This object must have a ThalmicMyo script attached.
	public GameObject myo = null;
	bool myoIsPaired = false;
	ThalmicHub hub;
	ThalmicMyo thalmicMyo;
	
	// Draw some basic instructions.
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

	public bool isPaired() {
		return myoIsPaired;
	}
	
	void Update ()
	{
		ThalmicHub hub = ThalmicHub.instance;
		
		if (Input.GetKeyDown ("q")) {
			hub.ResetHub();
		}
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
