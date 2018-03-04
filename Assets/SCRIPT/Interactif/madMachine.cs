using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class madMachine : MonoBehaviour {
	Animator anim;
	private bool canAccelerate;
	public GameObject spirale;
	public bool CanDegenerate;
	public GameObject smoke;

	public GameObject redLight1;
	public GameObject redLight2;

	public Light blancLight1;
	public Light blancLight2;
	public Light blancLight3;
	public Light blancLight4;
	public Light blancLight5;
	public Light blancLight6;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		canAccelerate = true;
		anim.speed = 0f;
		CanDegenerate = false;
		smoke.SetActive(false);
		redLight2.SetActive(false);
		redLight1.SetActive(false);
		spirale.GetComponent<AudioSource> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (CanDegenerate) {
			spirale.GetComponent<AudioSource> ().enabled = true;
			blancLight1.intensity = 0f;
			blancLight2.intensity = 0f;
			blancLight3.intensity = 0f;
			blancLight4.intensity = 0f;
			blancLight5.intensity = 0f;
			blancLight6.intensity = 0f;

			if (anim.speed > 2) {
				smoke.SetActive(true);
			}
			if (anim.speed < 5) {
				if (canAccelerate) {
					StartCoroutine (WaitToAccelerate ());
					canAccelerate = false;
					redLight2.SetActive(true);
					redLight1.SetActive(true);
				}

			}
		}
	}
	IEnumerator WaitToAccelerate()
	{
		
		yield return new WaitForSeconds(0.2f);
		anim.speed += 0.05f;
		spirale.GetComponent<spirale> ().speed += 6;
		GetComponent<AudioSource> ().pitch += 0.002f;
		canAccelerate = true;
	}
}
