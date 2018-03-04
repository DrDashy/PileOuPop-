using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [Header("Bouton du menu principal :")]
    public Button[] BoutonMenuPrincipal;

    [Header("Souligner bouton du menu principal :")]
    public GameObject[] SoulignerBoutonMenuPrincipal;

    [Header("Nom de la fonction qu'activera chaque bouton :")]
    public string[] NomFonction;
    private int BoutonSelectionner;

    [Header("Nom de la scene du niveau :")]
    public string NomMenuPrincipal;

    [Header("Pannel Menu :")]
    public GameObject AffichePanelMenuPrincipal;

    [Header("Pannel Controle :")]
    public GameObject AffichePanelControle;
    private bool AfficheControleEnCour;

    [HideInInspector]
    public bool CursorActif;

    [Header("AudioSource qui sera le petit bruit au changement de bouton :")]
    public AudioSource AudioTic;
    public AudioClip TicSound;

    void Awake()
    {
        CursorActif = false;
        AffichePanelMenuPrincipal.SetActive(true);
        AffichePanelControle.SetActive(false);
        AfficheControleEnCour = false;
        BoutonSelectionner = 0;
    }

    void Update()
    {
        if (!AfficheControleEnCour)
        {
            DeplacementBouton();
            if (Input.GetButtonDown("Interaction") || Input.GetButtonDown("Submit"))
            {
                ActiveBouton(BoutonSelectionner);
            }
        } else
        {
            if (Input.GetButtonDown("Interaction") || Input.GetButtonDown("Submit"))
            {
                RetourMenuPrincipal();
            }
        }
    }

    private void ActiveBouton(int compteur)
    {
        // Jous le son "Tic"
        AudioTic.PlayOneShot(TicSound);

        int Taille = BoutonMenuPrincipal.Length;

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
        if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0 || Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0)
        {
            BoutonSelectionner++;
            BoutonSelectionner = CheckConteur(BoutonSelectionner);
            // Jous le son "Tic"
            AudioTic.PlayOneShot(TicSound);
        }
        else if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") < 0 || Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0)
        {
            BoutonSelectionner--;
            BoutonSelectionner = CheckConteur(BoutonSelectionner);
            // Jous le son "Tic"
            AudioTic.PlayOneShot(TicSound);
        }
        // Check quel bouton doit être activé et les autre se désactive
        CheckBoutonSelectionner(BoutonSelectionner);
    }

    private int CheckConteur(int compteur)
    {
        int Taille = BoutonMenuPrincipal.Length;
        int ReelMaxTaille = BoutonMenuPrincipal.Length - 1;
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
        int Taille = BoutonMenuPrincipal.Length;

        for (int i = 0; i < Taille; i++)
        {
            if (i == compteur)
            {
                BoutonMenuPrincipal[i].Select();
                SoulignerBoutonMenuPrincipal[i].GetComponent<Image>().enabled = true;
            } else
            {
                SoulignerBoutonMenuPrincipal[i].GetComponent<Image>().enabled = false;
            }
        }
    }

    private void ChargerLaPartie()
    {
        SceneManager.LoadScene(NomMenuPrincipal);
    }

    private void AfficheControle()
    {
        AfficheControleEnCour = true;
        AffichePanelMenuPrincipal.SetActive(false);
        AffichePanelControle.SetActive(true);
    }

    private void RetourMenuPrincipal()
    {
        // Jous le son "Tic"
        AudioTic.PlayOneShot(TicSound);
        AfficheControleEnCour = false;
        AffichePanelMenuPrincipal.SetActive(true);
        AffichePanelControle.SetActive(false);
        BoutonSelectionner = 0;
    }

    private void QuitterLeJeu()
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

}

