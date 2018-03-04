using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveFin : MonoBehaviour, IActivable {

	public void Activate()
    {
        SceneManager.LoadScene("Fin");
    }

    public void Highlight()
    {
        
    }
}
