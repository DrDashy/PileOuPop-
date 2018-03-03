using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [Header("Bouton du menu principal :")]
    public Button[] BoutonMenuPrincipal;

    [Header("Nom de la fonction qu'activera chaque bouton :")]
    public string[] NomFonction;
    private int BoutonSelectionner;

    [Header("Nom de la scene du niveau :")]
    public string NomMenuPrincipal;

    void Awake()
    {
        BoutonSelectionner = 0;
    }

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        DeplacementBouton();
        if (Input.GetButtonDown("Interaction") || Input.GetButtonDown("Submit"))
        {
            ActiveBouton(BoutonSelectionner);
        }
    }

    private void ActiveBouton(int compteur)
    {
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
            }
        }
    }

    private void ChargerLaPartie()
    {
        SceneManager.LoadScene(NomMenuPrincipal);
    }

    private void QuitterLeJeu()
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

}

