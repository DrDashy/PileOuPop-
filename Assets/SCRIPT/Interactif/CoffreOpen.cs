using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffreOpen : MonoBehaviour {

    private Animator anim;

    private GameObject ManagerInteract;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        ManagerInteract = GameObject.FindGameObjectWithTag("InteractionManager");
    }
	
	// Update is called once per frame
	void Update () {
		if (ManagerInteract.GetComponent<InteractionManager>().LevierNiveau1IsActive[1])
        {
            anim.SetBool("CanOpen", true);
        }
	}
}
