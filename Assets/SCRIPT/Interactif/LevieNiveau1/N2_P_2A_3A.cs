using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N2_P_2A_3A : MonoBehaviour {

    private OuverturePorte Porte;

    private GameObject ManagerInteract;

    void Start()
    {
        ManagerInteract = GameObject.FindGameObjectWithTag("InteractionManager");
        Porte = GetComponent<OuverturePorte>();
        if (!ManagerInteract.GetComponent<InteractionManager>().CheckBoolIsActive(3, 2))
        {
            Porte.IsStuck = true;
        }
        else
        {
            Porte.IsStuck = false;
        }
    }

    void Update()
    {
        if (ManagerInteract.GetComponent<InteractionManager>().CheckBoolIsActive(3, 2))
        {
            Porte.IsStuck = false;
        }
    }
}
