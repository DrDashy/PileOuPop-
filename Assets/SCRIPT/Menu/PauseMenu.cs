using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    [Header("Canvas Menu Pause :")]
    public GameObject MenuPause;

    [Header("Canvas Menu Mort :")]
    public GameObject MenuMortAffiche;

    [Header("Bouton du menu pause :")]
    public Button[] BoutonMenuPause;

    [Header("Bouton du menu pause :")]
    public Image[] TexteBoutonMenuPause;

    [Header("Nom de la fonction qu'activera chaque bouton :")]
    public string[] NomFonction;
    private int BoutonSelectionner;

    [Header("Nom de la scene du menu principal :")]
    public string NomMenuPrincipal;

    private GameObject Player;
	private bool isPaused;

    void Awake()
	{
		Player = GameObject.FindGameObjectWithTag("Player");
        MenuPause.SetActive(false);
        BoutonSelectionner = 0;
        isPaused = false;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	void Update () 
	{
		//Check if the Cancel button in Input Manager is down this frame (default is Escape key) and that game is not paused, and that we're not in main menu
		if ( ( Input.GetButtonDown ("Pause") || Input.GetButtonDown("Cancel") ) && !isPaused && MenuMortAffiche.GetComponent<MortMenu>().isDead == false) 
		{
			//Call the DoPause function to pause the game
			ActivePause();
		} 
		//If the button is pressed and the game is paused and not in main menu
		else if ( (Input.GetButtonDown("Pause") || Input.GetButtonDown("Cancel") ) && isPaused && MenuMortAffiche.GetComponent<MortMenu>().isDead == false) 
		{
			//Call the UnPause function to unpause the game
			EnlevePause ();
		}

        if (isPaused)
        {
            DeplacementBouton();
            if (Input.GetButtonDown("Interaction") || Input.GetButtonDown("Submit"))
            {
                ActiveBouton(BoutonSelectionner);
            } 
        }
	}

	public void ActivePause()
	{
        // Le jeu est en pause
        isPaused = true;
		// Set time.timescale to 0, this will cause animations and physics to stop updating
		Time.timeScale = 0;
        // Desactive tous les sons
        AudioListener.pause = true;
        // Anule les mouvements du joueur
		Player.GetComponent<FirstPersonController>().enabled = false;
        // Active la fenêtre de menu
        MenuPause.SetActive(true);
    }


	public void EnlevePause()
	{
        // Le jeu n'est plus en pause
		isPaused = false;
		// Set time.timescale to 1, this will cause animations and physics to continue updating at regular speed
		Time.timeScale = 1;
        // Active tous les sons
        AudioListener.pause = false;
        // Active les mouvements du joueur
        Player.GetComponent<FirstPersonController>().enabled = true;
        // Desactive la fenêtre de menu
        MenuPause.SetActive(false);
    }

    private void ActiveBouton(int compteur)
    {
        int Taille = BoutonMenuPause.Length;

        for (int i = 0; i < Taille; i++)
        {
            if (i == compteur)
            {
                Invoke(NomFonction[i], 0f);
            }
        }
    }

    private void DeplacementBouton()
    {
        if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
        {
            BoutonSelectionner++;
            BoutonSelectionner = CheckConteur(BoutonSelectionner);
        }
        else if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
        {
            BoutonSelectionner--;
            BoutonSelectionner = CheckConteur(BoutonSelectionner);
        }
        // Check quel bouton doit être activé et les autre se désactive
        CheckBoutonSelectionner(BoutonSelectionner);
    }

    private int CheckConteur(int compteur)
    {
        int Taille = BoutonMenuPause.Length;
        int ReelMaxTaille = BoutonMenuPause.Length - 1;
        if (compteur < 0)
        {
            compteur = Taille - 1;
        }
        else if (compteur > ReelMaxTaille)
        {
            compteur = 0;
        }

        return compteur;
    }

    private void CheckBoutonSelectionner(int compteur)
    {
        int Taille = BoutonMenuPause.Length;
        
        for(int i = 0; i < Taille; i++)
        {
            if (i == compteur)
            {
                BoutonMenuPause[i].Select();
                TexteBoutonMenuPause[i].color = new Color32(0, 33, 255, 255);
            } else
            {
                TexteBoutonMenuPause[i].color = new Color32(255, 255, 255, 255);
            }
        }
    }

    private void QuitterDepuisMenuPause()
    {
        SceneManager.LoadScene(NomMenuPrincipal);
    }

}
