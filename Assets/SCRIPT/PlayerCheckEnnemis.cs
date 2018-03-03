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

    [Header("Musique d'ennemis proche : ")]
    public AudioClip CloseEnemy;

    [Header("Musique de course poursuite : ")]
    public AudioClip Chase;

    [Header("Volume max du son : ")]
    public float maxVolume;

    [Header("Volume min du son : ")]
    public float minVolume;

    [Header("Vitesse de FadeIn et FadeOut du son : ")]
    public float speedVolume;

    private bool inCalmZone = true;
    private bool inAlertZone = false;
    private bool inDangerZone = false;

    [Header("AudioSource : ")]
    public AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource.GetComponent<AudioSource>();
        audioSource.loop = true;
    }
	
	// Update is called once per frame
	void Update () {
		// Calme Zone
        if(Vector3.Distance(Ennemis.transform.position, Player.transform.position) > calmDistance && !inCalmZone)
        {
            inCalmZone = true;
            inAlertZone = false;
            inDangerZone = false;

            audioSource.Stop();
        }

        // Alert Zone
        if (Vector3.Distance(Ennemis.transform.position, Player.transform.position) <= calmDistance && Vector3.Distance(Ennemis.transform.position, Player.transform.position) > dangerDistance && !inAlertZone)
        {
            audioSource.Stop();
            StartCoroutine(FadeOut());

            inCalmZone = false;
            inAlertZone = true;
            inDangerZone = false;

            audioSource.PlayOneShot(CloseEnemy);
            StartCoroutine(FadeIn());
        }

        // Alert Zone
        if (Vector3.Distance(Ennemis.transform.position, Player.transform.position) < dangerDistance && !inDangerZone)
        {
            audioSource.Stop();
            StartCoroutine(FadeOut());

            inCalmZone = false;
            inAlertZone = false;
            inDangerZone = true;

            audioSource.loop = true;
            audioSource.PlayOneShot(Chase);
            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeIn()
    {
        audioSource.volume = 0;

        while(audioSource.volume < maxVolume)
        {
            audioSource.volume += speedVolume;
        }
        yield return new WaitForSeconds(0);
    }

    IEnumerator FadeOut()
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume += speedVolume;
        }
        yield return new WaitForSeconds(0);
    }
}
