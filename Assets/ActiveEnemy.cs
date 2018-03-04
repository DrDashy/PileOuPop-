using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveEnemy : MonoBehaviour {

    private GameObject EnnemyManager;

    private void OnTriggerEnter(Collider other)
    {
        EnnemyManager = GameObject.FindGameObjectWithTag("EnnemisManager");
        EnnemyManager.SetActive(true);
    }
}
