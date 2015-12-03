using UnityEngine;
using System.Collections;

public class CueStickControl : MonoBehaviour {    

	GeneralInfo generalInfo;

    // Use this for initialization
    void Start () {
		generalInfo = GeneralInfo.getInstance();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.transform.name == "cueBall") {
			gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			Debug.Log ("CueSTick on collision enter: transform.position " + transform.position + " ballcontrol " + generalInfo.getCueStickPosition());
			transform.position = Vector3.Lerp (transform.position, generalInfo.getCueStickPosition(), Time.deltaTime * 2);
		}
	}
}
