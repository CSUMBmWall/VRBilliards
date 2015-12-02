using UnityEngine;
using System.Collections;

public class PlayIntroFlyBy : MonoBehaviour {
	public Animator flyBy;

	// Use this for initialization
	void Start () {
Assets/scripts/PlayIntroFlyBy.cs
		animation = (Animation) Resources.Load("introFlyBy");
		animation.Play();



	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
