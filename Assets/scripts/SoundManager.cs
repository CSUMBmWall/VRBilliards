using UnityEngine;
using System.Collections;

public class SoundManager {
	
	private float[] playing = new float[MAX_PLAYERS];
	private const int MAX_PLAYERS = 6; // set maximum number of players
	private const float PLAY_TIME = 6.0f; // set time for how long this player is "busy"
	private static float time;

	private static SoundManager instance;
	
	private SoundManager() {}
	
	public static SoundManager getInstance()
	{
			if (instance == null)
			{
				instance = new SoundManager();
			}
			return instance;
	}

	public void playSound(AudioClip clip, Vector3 position){
		for (int i=0; i < MAX_PLAYERS; i++) {
			if (playing[i] <= 0) {
				playing[i] = PLAY_TIME;
				AudioSource.PlayClipAtPoint (clip, position);
				break;
			}
		}
	}


	public void updateSoundManager() {
		float time = Time.deltaTime;
		for (int i=0; i < MAX_PLAYERS; i++) {
			if (instance.playing[i] > 0)
				instance.playing[i] -= time;
		}
	}

	public void initializeSoundManager() {
		for (int i=0; i < MAX_PLAYERS; i++) {
			playing[i] = i;
		}
	}

}
