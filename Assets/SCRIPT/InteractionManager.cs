using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour {

    public bool[] LevierNiveau1IsActive;
    
    public bool[] LevierNiveau2IsActive;
    public bool Niveau2Reussi;

    public int[] LevierNiveau2CheckEnd;

    public bool[] LevierNiveau3IsActive;

    // Use this for initialization
    void Start () {
		for (int i = 0; i < LevierNiveau1IsActive.Length; i++)
        {
            LevierNiveau1IsActive[i] = false;
        }
        for (int i = 0; i < LevierNiveau2IsActive.Length; i++)
        {
            LevierNiveau2IsActive[i] = false;
        }
        for (int i = 0; i < LevierNiveau3IsActive.Length; i++)
        {
            LevierNiveau3IsActive[i] = false;
        }
        for (int i = 0; i < LevierNiveau2CheckEnd.Length; i++)
        {
            LevierNiveau2CheckEnd[i] = -1;
        }
    }

    public bool CheckBoolIsActive(int indiceTableau, int numeroNiveau)
    {
        bool WasItActive = false;
        switch (numeroNiveau)
        {
            case 1:
                WasItActive = LevierNiveau1IsActive[indiceTableau];
                break;

            case 2:
                WasItActive = LevierNiveau2IsActive[indiceTableau];
                break;

            case 3:
                WasItActive = LevierNiveau3IsActive[indiceTableau];
                break;
        }

        return WasItActive;
    }

    public void InitialiseSuiteLevier(int indiceTableau)
    {
        for (int i=0; i< LevierNiveau2CheckEnd.Length; i++)
        {
            if (LevierNiveau2CheckEnd[i] == -1)
            {
                LevierNiveau2CheckEnd[i] = indiceTableau;
                i = LevierNiveau2CheckEnd.Length;
            }
        }
    }

    public bool CheckSuiteLevier()
    {
        bool testReussi = false;
        int compteur = 0;
        for (int i = 0; i < LevierNiveau2CheckEnd.Length; i++)
        {
            if (compteur == 0 && LevierNiveau2CheckEnd[i] == 0)
            {
                compteur++;
            } else if (compteur == 1 && LevierNiveau2CheckEnd[i] == 1)
            {
                compteur++;
            } else if (compteur == 2 && LevierNiveau2CheckEnd[i] == 2)
            {
                testReussi = true;
            } else
            {
                return testReussi;
            }
        }
        return testReussi;
    }

    public void SetBoolIsActive(int indiceTableau, int numeroNiveau, bool boolSet)
    {
        switch (numeroNiveau)
        {
            case 1:
                LevierNiveau1IsActive[indiceTableau] = boolSet;
                break;

            case 2:
                LevierNiveau2IsActive[indiceTableau] = boolSet;
                break;

            case 3:
                LevierNiveau3IsActive[indiceTableau] = boolSet;
                break;
        }
    }
}
