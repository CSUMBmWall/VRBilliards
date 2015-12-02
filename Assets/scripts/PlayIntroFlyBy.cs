using UnityEngine;
using System.Collections;

public class PlayIntroFlyBy : MonoBehaviour {
	public Animation animation;

	// Use this for initialization
	void Start () {
		animation = (Animation) Resources.Load("introFlyBy");
		animation.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
