using UnityEngine;
using System.Collections;

public class GeneralInfo {

	private static GeneralInfo instance;
	
	private GeneralInfo() { }
	
	public static GeneralInfo getInstance()
	{
		if (instance == null)
		{
			instance = new GeneralInfo();
		}
		return instance;
	}

	Vector3 cueStickPositionBeforeShot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setCuePosition(Vector3 cueStickPositionBeforeShot) {
		this.cueStickPositionBeforeShot = cueStickPositionBeforeShot;
	}

	public Vector3 getCueStickPosition() {
		return cueStickPositionBeforeShot;
	}
}
