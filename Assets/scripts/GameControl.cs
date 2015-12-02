using UnityEngine;

using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;


public class GameControl : MonoBehaviour {

	public GameObject myo = null;
	private Pose _lastPose = Pose.Unknown;
	ThalmicMyo thalmicMyo = null;
	ThalmicHub hub;
    
	private static GameControl instance;

    private GameControl() { }


    public static GameControl getInstance()
    {
        if (instance == null)
        {
			instance = new GameControl();
        }
        return instance;
    }
    
    

	private CueStickControl csc;

	public void setCueStickPos(Ray CueStickRay) {
		//csc.lineUpShot(CueStickRay);
	}

    void Start () {

		SoundManager.getInstance ().initializeSoundManager ();	
		//csc = CueStickControl.getInstance();

	}	
	
    bool bposCueStick = false;
    public void setbposCueStick(bool setCue)
    {
        //bposCueStick = setCue;
    }
    // Myo game object to connect with.
    // This object must have a ThalmicMyo script attached.

    void Update()
    {
        SoundManager.getInstance().updateSoundManager();
        if (Input.GetKey("escape"))
            Application.Quit();
		thalmicMyo = myo.GetComponent<ThalmicMyo> ();
        hub = ThalmicHub.instance;
        if (Input.GetKeyDown("q"))
        {
            hub.ResetHub();
        }
		//if (
    }

    // Draw some basic instructions.
    void OnGUI()
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
            GUI.Label (new Rect (12, 8, Screen.width, Screen.height),
                "Fist: Vibrate Myo armband\n" +
                "Wave in: Set box material to blue\n" +
                "Wave out: Set box material to green\n" +
                "Double tap: Reset box material\n" +
                "Fingers spread: Set forward direction"
            );
        }
        
        
    }
}


