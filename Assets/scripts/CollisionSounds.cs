using UnityEngine;
using System.Collections;

public class CollisionSounds : MonoBehaviour {
	public AudioClip impact;
	AudioSource audio;
	
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
	}
	
	void OnCollisionEnter(Collision collision) {
		if (Time.time > 0.3f) { // Makes the balls not make sound at "collision" at the start of the game
			if (collision.gameObject.tag == "ball") {
				audio.PlayOneShot(impact);
			}
		}
	}
}
