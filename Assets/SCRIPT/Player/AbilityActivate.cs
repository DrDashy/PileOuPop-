using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class AbilityActivate : MonoBehaviour {

    public float DistanceActivation;
    public LayerMask InteractifLayer;
    


    protected Camera cam;
    protected bool buttonPressed;
    protected IActivable currentActivable;


    void Start () {        
        buttonPressed = false;
        cam = GetComponentInChildren<Camera>();
        if (cam == null)
            Debug.Log("Pas de Camera détecté sur les fils l'objet");                                  
	}
	

	void Update () {
        currentActivable = null;
        CheckRaycast();
          
        if (Input.GetAxisRaw("Interaction") != 0)
        {
            if (!buttonPressed)
            {
                TryActivate();
                buttonPressed = true;
            }                
        } 
        else
            buttonPressed = false;
	}

    private void LateUpdate()
    {
        HighlightActivable();
    }

    protected void CheckRaycast()
    {
        RaycastHit result;
        Ray cameraRay = cam.ScreenPointToRay(new Vector2(cam.pixelWidth/2, cam.pixelHeight/2));
       

        if (Physics.Raycast(cameraRay, out result, DistanceActivation, InteractifLayer, QueryTriggerInteraction.UseGlobal)) {
            if (result.collider != null)
                currentActivable = result.collider.GetComponent<IActivable>();
        }            
    }

    protected void TryActivate()
    {
        if (currentActivable != null)
            currentActivable.Activate();
    }

    protected void HighlightActivable()
    {
        if (currentActivable != null)
        {
            currentActivable.Highlight();
        }
    }
}
