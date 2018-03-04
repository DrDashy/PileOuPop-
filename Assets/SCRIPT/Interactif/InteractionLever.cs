using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionLever : MonoBehaviour, IActivable {
	//public IActivable objetActivable;

    // [HideInInspector]
    public bool WasActivated;

    public int IndiceTableau;

    public bool CanBeReactivated;

    [Range(1,3)]
    public int NiveauReferent;

    private GameObject ManagerInteract;

	// Use this for initialization
	void Start () {
        ManagerInteract = GameObject.FindGameObjectWithTag("InteractionManager");
        CheckIsActive();
        if (WasActivated)
            GetComponent<Animator>().SetBool("isActivated", true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Activate()
	{
        Debug.Log("Hoy");
        if (!WasActivated || CanBeReactivated)
        {
            if (this.GetComponent<Animator>().GetBool("isActivated") == false)
            {
                ManagerInteract.GetComponent<InteractionManager>().SetBoolIsActive(IndiceTableau, NiveauReferent, true);
                CheckIsActive();
                ActivateLever();
            }
            else
            {
                DesactivateLever();
            }
        }
	}

    public void CheckIsActive()
    {
        WasActivated = ManagerInteract.GetComponent<InteractionManager>().CheckBoolIsActive(IndiceTableau, NiveauReferent);
    }

    public void Highlight()
	{

	}

	public void ActivateLever()
	{
		GetComponent<Animator> ().SetBool ("isActivated", true);
	}

	public void DesactivateLever()
	{
		GetComponent<Animator> ().SetBool ("isActivated", false);
	}
		
}
