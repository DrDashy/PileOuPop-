using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumiereSpot : MonoBehaviour {

    public GameObject spot;

    public int IndiceTableau;

    private GameObject ManagerInteract;

    // Use this for initialization
    void Start()
    {
        ManagerInteract = GameObject.FindGameObjectWithTag("InteractionManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (ManagerInteract.GetComponent<InteractionManager>().LevierNiveau2IsActive[IndiceTableau])
        {
            spot.GetComponent<Light>().enabled = true;
        } else
        {
            spot.GetComponent<Light>().enabled = false;
        }
    }
}
