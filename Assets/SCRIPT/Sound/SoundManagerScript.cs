using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManagerScript : MonoBehaviour {
    static public void PlayMusic (GameObject gameOjb, AudioClip Son)
    {
        gameOjb.GetComponent<AudioSource>().PlayOneShot(Son);
    }
}
