using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckEnnemis : MonoBehaviour {

    public GameObject Player;
    public GameObject Ennemis;

    [Header("Distance zones Calme : ")]
    public float calmDistance;
    public float EnnemySpeedCalm;

    [Header("Distance zones Enemy Proche && Vitesse: ")]
    public float alertDistance;
    public float EnnemySpeedAlert;

    [Header("Distance zones Danger : ")]
    public float dangerDistance;
    public float EnnemySpeedDanger;

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

            Ennemis.GetComponent<EnnemisControl>().SetSpeed(5f);

            audioSource.Stop();
        }

        // Alert Zone
        if (Vector3.Distance(Ennemis.transform.position, Player.transform.position) <= calmDistance && Vector3.Distance(Ennemis.transform.position, Player.transform.position) > dangerDistance && !inAlertZone)
        {
            audioSource.Stop();
            FadeOut();

            inCalmZone = false;
            inAlertZone = true;
            inDangerZone = false;

            Ennemis.GetComponent<EnnemisControl>().SetSpeed(EnnemySpeedAlert);

            audioSource.PlayOneShot(CloseEnemy);
            audioSource.volume = 0;
            FadeIn();
        }

        // Alert Zone
        if (Vector3.Distance(Ennemis.transform.position, Player.transform.position) < dangerDistance && !inDangerZone)
        {
            audioSource.Stop();
            FadeOut();

            inCalmZone = false;
            inAlertZone = false;
            inDangerZone = true;

            Ennemis.GetComponent<EnnemisControl>().SetSpeed(5f);

            audioSource.loop = true;
            audioSource.PlayOneShot(Chase);
            audioSource.volume = 0;
            FadeIn();
        }
    }

    void FadeIn()
    {
        if(audioSource.volume < maxVolume)
        {
            audioSource.volume += speedVolume;
            Invoke("FadeIn", 1f);
        }
    }

    void FadeOut()
    {
        if (audioSource.volume > 0)
        {
            audioSource.volume -= speedVolume;
            Invoke("FadeOut", 1f);
        }
    }
}
