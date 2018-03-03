using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuverturePorte : MonoBehaviour, IActivable {
	public GameObject porteVoisine;

	void Start()
	{

	}

	void Update()
	{
		
	}

	public void Activate()
	{
		if (this.GetComponent<Animator> ().GetBool("isOpen") == false) {
			OpenDoor (this.gameObject);
			OpenDoor (porteVoisine);
		} else {
			CloseDoor (this.gameObject);
			CloseDoor (porteVoisine);
		}

	}

	public void Highlight()
	{
		
	}

	public void OpenDoor(GameObject porte)
	{
		porte.GetComponent<Animator> ().SetBool ("isOpen", true);
		porte.GetComponent<BoxCollider> ().isTrigger = true;
	}



	public void CloseDoor(GameObject porte)
	{
		porte.GetComponent<Animator> ().SetBool ("isOpen", false);
		porte.GetComponent<BoxCollider> ().isTrigger = false;
	}

	public void OnTriggerExit()
	{
		this.GetComponent<Animator> ().SetBool ("isOpen", false);
		this.GetComponent<BoxCollider> ().isTrigger = false;
	}

}
