using UnityEngine;
using System.Collections;

public class CueStickControl : MonoBehaviour {

    private static CueStickControl instance;

    private CueStickControl() { }

    public static CueStickControl getInstance()
    {
        if (instance == null)
        {
            instance = new CueStickControl();
        }
        return instance;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void lineUpShot(Ray cueStickRay)
    {
        transform.position = cueStickRay.GetPoint (.5f);
    }
}
