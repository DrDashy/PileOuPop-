using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckEnnemis : MonoBehaviour {

    public GameObject Player;
    public GameObject Ennemis;

    public float calmDistance = 80;

    public List<AudioClip> Chase;

    private bool inCalmZone = true;
    private bool inAlertZone = false;

	// Use this for initialization
	void Start () {
        // SoundManagerScript.PlayMusic(Player, Chase[0]);
	}
	
	// Update is called once per frame
	void Update () {
		// Calme Zone
        if(Vector3.Distance(Ennemis.transform.position, Player.transform.position) > calmDistance && !inCalmZone)
        {
            inCalmZone = true;
            inAlertZone = false;
            // SoundManagerScript.PlayMusic(Player, Chase[0]);
        }

        // Alert Zone
        if (Vector3.Distance(Ennemis.transform.position, Player.transform.position) <= calmDistance && !inAlertZone)
        {
            inCalmZone = false;
            inAlertZone = true;
            SoundManagerScript.PlayMusic(Player, Chase[0]);
        }
    }
}
