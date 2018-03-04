using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N1_P1 : MonoBehaviour {

    private OuverturePorte Porte;

    private GameObject ManagerInteract;

    void Start()
    {
        ManagerInteract = GameObject.FindGameObjectWithTag("InteractionManager");
        Porte = GetComponent<OuverturePorte>();
        if (!ManagerInteract.GetComponent<InteractionManager>().CheckBoolIsActive(0, 1))
        {
            Porte.IsStuck = true;
        } else
        {
            Porte.IsStuck = false;
        }
    }

    void Update()
    {
        if (ManagerInteract.GetComponent<InteractionManager>().CheckBoolIsActive(2, 1))
        {
            Porte.IsStuck = false;
        }
    }
}
