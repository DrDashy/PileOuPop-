using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N2_L4 : MonoBehaviour {

    private GameObject ManagerInteract;

    private bool alreadyCheck;

    void Start()
    {
        alreadyCheck = false;
        ManagerInteract = GameObject.FindGameObjectWithTag("InteractionManager");
    }

    void Update()
    {
        bool test = ManagerInteract.GetComponent<InteractionManager>().LevierNiveau2IsActive[3];
        if (test && !alreadyCheck)
        {
            Debug.Log("test");
            alreadyCheck = true;
            TryOrderLever();
        }
    }


    public void TryOrderLever()
    {
        Debug.Log("test");
        if (ManagerInteract.GetComponent<InteractionManager>().CheckSuiteLevier())
        {
            ManagerInteract.GetComponent<InteractionManager>().Niveau2Reussi = true;
        } else
        {
            ManagerInteract.GetComponent<InteractionManager>().SetBoolIsActive(3, 2, false);
            ManagerInteract.GetComponent<InteractionManager>().SetBoolIsActive(2, 2, false);
            ManagerInteract.GetComponent<InteractionManager>().SetBoolIsActive(1, 2, false);
            ManagerInteract.GetComponent<InteractionManager>().SetBoolIsActive(0, 2, false);
            alreadyCheck = false;
        }
    }
}
