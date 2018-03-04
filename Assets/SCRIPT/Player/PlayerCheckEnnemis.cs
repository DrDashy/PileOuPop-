using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckEnnemis : MonoBehaviour {

    private GameObject Player;

    [Header("Distance zones Danger : ")]
    public float dangerDistance;

    [Header("Volume max du son : ")]
    [Range(0,1)]
    public float maxVolume;

    [Header("Volume min du son : ")]
    [Range(0, 1)]
    public float minVolume;

    [Header("Vitesse de FadeIn et FadeOut du son : ")]
    public float speedVolume;

    private bool inDangerZone = false;

    [Header("AudioSource Zone Danger : ")]
    public AudioSource audioSourceDanger;

    // Use this for initialization
    void Awake () {
        Player = GameObject.FindGameObjectWithTag("Player");
        audioSourceDanger.Stop();
        audioSourceDanger.volume = 0;
    }
	
	// Update is called once per frame
	void Update () {
        CheckDistanceEnnemis();
    }

    private void CheckDistanceEnnemis()
    {
        // Danger Zone
        if (Vector3.Distance(this.transform.position, Player.transform.position) < dangerDistance && !inDangerZone)
        {
            inDangerZone = true;

            audioSourceDanger.Play();

            StartCoroutine(IncrementVolume());
        }
    }

    IEnumerator IncrementVolume()
    {
        yield return new WaitForSeconds(1f);
        if (audioSourceDanger.volume < maxVolume)
        {
            audioSourceDanger.volume += speedVolume;
            StartCoroutine(IncrementVolume());
        }
    }

    IEnumerator DecrementVolume()
    {
        yield return new WaitForSeconds(1f);
        if(audioSourceDanger.volume > 0)
        {
            audioSourceDanger.volume -= speedVolume;
            StartCoroutine(IncrementVolume());
        }
    }

}
