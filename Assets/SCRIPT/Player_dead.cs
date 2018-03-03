using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Player_dead : MonoBehaviour {

    private GameObject MenuMortAffiche;

	// Use this for initialization
	void Start () {
        MenuMortAffiche = GameObject.FindGameObjectWithTag("MenuMort");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MortPlayer()
    {
        // Anule les mouvements du joueur
        this.GetComponent<FirstPersonController>().enabled = false;
        // Active le menu mort
        MenuMortAffiche.GetComponent<MortMenu>().ActiveCanvas();
        MenuMortAffiche.GetComponent<MortMenu>().isDead = true;
    }
}
