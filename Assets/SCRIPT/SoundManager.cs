using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	
	public void playOneShot(AudioClip music) {
		playOneShot (music, 1.0f);
	}

	public void playOneShot(AudioClip music, float volume) {
		playOneShot (music, volume, 1.0f);
	}

	public void playOneShot(AudioClip music, float volume, float pitch) {
		PlayOneShot (music, volume, pitch);
	}
}
