using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuverturePorte : MonoBehaviour, IActivable {
	public OuverturePorte porteVoisine;
    public RoomLoader loader;

    protected bool IsOpen;

	void Start()
	{
        IsOpen = false;
        if (loader == null)
            Debug.Log("Pas de loader détecté");
	}

	public void Activate()
	{
		if (!IsOpen) {
			OpenDoor();
            if(porteVoisine!= null)
                 porteVoisine.OpenDoor();
		} 
	}

	public void Highlight()
	{
		
	}

	public void OpenDoor()
	{
        IsOpen = true;
		GetComponent<Animator> ().SetBool ("isOpen", true);
		GetComponent<BoxCollider> ().isTrigger = true;
	}



	public void CloseDoor()
	{
        Debug.Log("Closing");
        IsOpen = false;
        GetComponent<Animator> ().SetBool ("isOpen", false);
		GetComponent<BoxCollider> ().isTrigger = false;
        if (loader.isInTheRoom)
        {
            Debug.Log("Loading");
            loader.LoadRooms();            
        }
            
	}

	public void OnTriggerExit()
	{
        Debug.Log("Quit coolider door");
        CloseDoor();
        if (porteVoisine != null)
            porteVoisine.CloseDoor();
	}

}
