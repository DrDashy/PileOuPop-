using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckEnnemis : MonoBehaviour {

    public GameObject Player;
    public GameObject Ennemis;

    [Header("Distance zones Calme : ")]
    public float calmDistance;

    [Header("Distance zones Enemy Proche : ")]
    public float alertDistance;

    [Header("Distance zones Danger : ")]
    public float dangerDistance;

    [Header("Volume max du son : ")]
    public float maxVolume;

    [Header("Volume min du son : ")]
    public float minVolume;

    [Header("Vitesse de FadeIn et FadeOut du son : ")]
    public float speedVolume;

    private bool inCalmZone = true;
    private bool inAlertZone = false;
    private bool inDangerZone = false;

    [Header("AudioSource Zone Calm: ")]
    public AudioSource audioSourceCalm;

    [Header("AudioSource Zone Alert : ")]
    public AudioSource audioSourceAlert;

    [Header("AudioSource Zone Danger : ")]
    public AudioSource audioSourceDanger;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        CheckDistanceEnnemis();
    }

    private void FixedUpdate()
    {
        if (inCalmZone)
        {
            FadeOut(audioSourceAlert);
            FadeOut(audioSourceDanger);

            FadeIn(audioSourceCalm);
        }

        if (inAlertZone)
        {
            FadeOut(audioSourceCalm);
            FadeOut(audioSourceDanger);

            FadeIn(audioSourceAlert);
        }

        if (inDangerZone)
        {
            FadeOut(audioSourceCalm);
            FadeOut(audioSourceAlert);

            FadeIn(audioSourceDanger);
        }
    }

    private void CheckDistanceEnnemis()
    {
        // Calme Zone
        if (Vector3.Distance(Ennemis.transform.position, Player.transform.position) > calmDistance && !inCalmZone)
        {
            inCalmZone = true;
            inAlertZone = false;
            inDangerZone = false;

            audioSourceCalm.Play();
            audioSourceAlert.Stop();
            audioSourceDanger.Stop();
        }

        // Alert Zone
        if (Vector3.Distance(Ennemis.transform.position, Player.transform.position) <= calmDistance && Vector3.Distance(Ennemis.transform.position, Player.transform.position) > dangerDistance && !inAlertZone)
        {
            inCalmZone = false;
            inAlertZone = true;
            inDangerZone = false;

            audioSourceCalm.Stop();
            audioSourceAlert.Play();
            audioSourceDanger.Stop();
        }

        // Alert Zone
        if (Vector3.Distance(Ennemis.transform.position, Player.transform.position) < dangerDistance && !inDangerZone)
        {
            inCalmZone = false;
            inAlertZone = false;
            inDangerZone = true;

            audioSourceCalm.Stop();
            audioSourceAlert.Stop();
            audioSourceDanger.Play();
        }
    }

    private void FadeIn(AudioSource audioSource)
    {
        if(audioSource.volume < maxVolume)
        {
            audioSource.volume += speedVolume;
            Invoke("FadeIn", 1f);
        }
    }

    private void FadeOut(AudioSource audioSource)
    {
        if (audioSource.volume > 0)
        {
            audioSource.volume -= speedVolume;
			Invoke ("FadeOut", 0.5f);
        }
    }
}
