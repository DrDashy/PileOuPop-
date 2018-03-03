using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemisControl : MonoBehaviour {

    private GameObject Player;

    [Header("Vitesse de déplacement :")]
    public float Speed;

    // Use this for initialization
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
	
	void FixedUpdate () {
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        // ---------- Déplacement vers Joueur -------------//
        Quaternion rotationAngle = Quaternion.LookRotation(Player.transform.position - transform.position); // we get the angle has to be rotated
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationAngle, Time.deltaTime * Speed); // we rotate the rotationAngle 
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0); //bloquer sur l'axe y

        // ---------- Déplacement vers Joueur -------------//
        transform.position += transform.forward * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player.GetComponent<Player_dead>().MortPlayer();
        }
    }
}
