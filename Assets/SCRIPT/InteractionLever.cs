using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionLever : MonoBehaviour, IActivable {
	public IActivable objetActivable;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Activate()
	{
		if (this.GetComponent<Animator> ().GetBool("isActivated") == false) {
			ActivateLever ();
		} else {
			DesactivateLever ();
		}

	}

	public void Highlight()
	{

	}

	public void ActivateLever()
	{
		GetComponent<Animator> ().SetBool ("isActivated", true);
		objetActivable.Activate ();
	}



	public void DesactivateLever()
	{
		GetComponent<Animator> ().SetBool ("isActivated", false);
		objetActivable.Activate ();
	}
		
}
