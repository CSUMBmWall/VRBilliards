using UnityEngine;
using System.Collections;

public class RandomAudio : MonoBehaviour
{
	public AudioClip[] soundtrack;
	
	// Use this for initialization
	void Start ()
	{
		soundtrack = Resources.LoadAll<AudioClip>("jukeBox");
		for (int i = 0; i < soundtrack.Length; i++) {
			Debug.Log("Soundtrack" + soundtrack[i].ToString());
		} 
		
		if (!GetComponent<AudioSource>().playOnAwake)
		{
			GetComponent<AudioSource>().clip = soundtrack[Random.Range(0, soundtrack.Length)];
			GetComponent<AudioSource>().Play();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!GetComponent<AudioSource>().isPlaying)
		{
			GetComponent<AudioSource>().clip = soundtrack[Random.Range(0, soundtrack.Length)];
			GetComponent<AudioSource>().Play();
		}
	}
}