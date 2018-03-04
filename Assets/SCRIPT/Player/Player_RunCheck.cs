using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_RunCheck : MonoBehaviour {

    [Header("Time while running before essouflement fort :")]
    public float CurrentTimeWhileRun;

    private float originTimeWhileRun;

    [Header("Volume max du son : ")]
    public float maxVolume;

    [Header("Volume min du son : ")]
    public float minVolume;

    [Header("Vitesse de FadeIn et FadeOut du son : ")]
    public float speedVolume;

    [Header("AudioSource : ")]
    public AudioSource audioSource;

    [Header("Son Respiration : ")]
    // public AudioClip Respiration;

    private bool IsRunning;
    private bool Stop;

    // Use this for initialization
    void Start () {
        IsRunning = false;
        originTimeWhileRun = CurrentTimeWhileRun;
        audioSource.GetComponent<AudioSource>();
        audioSource.loop = true;
        Stop = false;
        audioSource.volume = 0;
    }
	
	// Update is called once per frame
	void Update () {
        // Lance le check qui cour
        if (Input.GetKeyDown(KeyCode.LeftShift) && !IsRunning)
        {
            IsRunning = true;
            Stop = false;
            FadeIn();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && IsRunning)
        {
            IsRunning = false;
            Stop = true;
            FadeOut();
        }
    }

    void FadeIn()
    {
        if (audioSource.volume < maxVolume && !Stop)
        {
            audioSource.volume += speedVolume;
            Invoke("FadeIn", 1f);
        }
    }

    void FadeOut()
    {
        if (audioSource.volume > 0 && Stop)
        {
            audioSource.volume -= speedVolume;
            Invoke("FadeOut", 1f);
        }
    }

}
