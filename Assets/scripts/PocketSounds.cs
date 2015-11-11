using UnityEngine;
using System.Collections;

public class PocketSounds : MonoBehaviour {
	public AudioClip impact;
	AudioSource audio;
	
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
	}
	
	void OnTriggerEnter(Collider collision) {
		if (collision.gameObject.tag == "ball" || collision.gameObject.tag == "cueball") {
			audio.PlayOneShot(impact);
		}
	}
}
