using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class MortMenu : MonoBehaviour
{

    [Header("Canvas Menu Mort :")]
    public GameObject MenuMort;

    [Header("Canvas In Game :")]
    public GameObject InGame;

    [Header("Bouton du menu mort :")]
    public Button[] BoutonMenuMort;

    [Header("Texte bouton :")]
    public Image[] TexteBoutton;

    [Header("Surlignage bouton :")]
    public Image[] SurlignageBoutton;

    [Header("Nom de la fonction qu'activera chaque bouton :")]
    public string[] NomFonction;
    private int BoutonSelectionner;

    [Header("Nom de la scene à recharger :")]
    public string NomSceneRecharger;

    [Header("Texte a la mort :")]
    public Text ObjectTexteMort;

    [Header("Nom du texte a la mort :")]
    public string[] TexteMort;

    [Header("Nom de la scene pour quitter :")]
    public string NomSceneQuitter;

    [HideInInspector]
    public bool isDead;

    void Awake()
    {
        isDead = false;
        MenuMort.SetActive(false);
        BoutonSelectionner = 0;
    }

    void Update()
    {
        if (isDead)
        {
            DeplacementBouton();
            if (Input.GetButtonDown("Interaction") || Input.GetButtonDown("Submit"))
            {
                ActiveBouton(BoutonSelectionner);
            }
        }
    }

    public void ActiveCanvas()
    {
        if (!isDead)
        {
            MenuMort.SetActive(true);
            ObjectTexteMort.text = TexteMort[Random.Range(0, TexteMort.Length)];
            InGame.SetActive(false);
        }    
    }

    private void ActiveBouton(int compteur)
    {
        int Taille = BoutonMenuMort.Length;

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
        int Taille = BoutonMenuMort.Length;
        int ReelMaxTaille = BoutonMenuMort.Length - 1;
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
        int Taille = BoutonMenuMort.Length;

        for (int i = 0; i < Taille; i++)
        {
            if (i == compteur)
            {
                BoutonMenuMort[i].Select();
                TexteBoutton[i].color = new Color32(0, 33, 255, 255);
                SurlignageBoutton[i].enabled = true;
            } else
            {
                TexteBoutton[i].color = new Color32(255, 255, 255, 255);
                SurlignageBoutton[i].enabled = false;
            }
        }
    }

    private void RecommencerPartie()
    {
        SceneManager.LoadScene(NomSceneRecharger);
    }

    private void QuitterDepuisMenuMort()
    {
        SceneManager.LoadScene(NomSceneQuitter);
    }

}