using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnmisNiveau1Watch : MonoBehaviour {

    public GameObject Door;

    private bool DiSpawn;

	// Use this for initialization
	void Start () {
        DiSpawn = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(!Door.GetComponent<OuverturePorte>().IsStuck && !DiSpawn)
        {
            DiSpawn = true;
            this.gameObject.SetActive(false);
        }
    }
}
