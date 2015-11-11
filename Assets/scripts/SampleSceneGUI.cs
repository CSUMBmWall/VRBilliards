using UnityEngine;
using System.Collections;

// Draw simple instructions for sample scene.
// Check to see if a Myo armband is paired.
public class SampleSceneGUI : MonoBehaviour
{


    bool bposCueStick = false;
    public void setbposCueStick( bool setCue)
    {
        bposCueStick = setCue;
    }
    // Myo game object to connect with.
    // This object must have a ThalmicMyo script attached.
    public GameObject myo = null;

	void Start () {
		SoundManager.getInstance ().initializeSoundManager ();
        
	}

	void Update () {
		SoundManager.getInstance ().updateSoundManager ();
		if (Input.GetKey("escape"))
			Application.Quit();
		ThalmicHub hub = ThalmicHub.instance;
		if (Input.GetKeyDown ("q")) {
			hub.ResetHub();
		}
	}

    // Draw some basic instructions.
    void OnGUI ()
    {
        GUI.skin.label.fontSize = 20;

        ThalmicHub hub = ThalmicHub.instance;

        // Access the ThalmicMyo script attached to the Myo object.
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();

		/*

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
            GUI.Label (new Rect (12, 8, Screen.width, Screen.height),
                "Fist: Vibrate Myo armband\n" +
                "Wave in: Set box material to blue\n" +
                "Wave out: Set box material to green\n" +
                "Double tap: Reset box material\n" +
                "Fingers spread: Set forward direction"
            );
        }
        */
    }

}
