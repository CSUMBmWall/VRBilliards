using UnityEngine;
using System.Collections;

public class CueBallControl : MonoBehaviour {
	
	private float leftright;
	private float thereback;
	private Rigidbody rb;
	
	public float force_multiplier;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	void FixedUpdate () {

		// read input


		leftright = Input.GetAxisRaw ("Horizontal");
		thereback = Input.GetAxisRaw ("Vertical");
		rb.AddForce (new Vector3 (leftright, 0f, thereback ).normalized * force_multiplier);
        
		// limit the maximum velocity, and make it dependent on the mass
		//clampVelocity ();	

	}
}
