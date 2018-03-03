﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemisControl : MonoBehaviour {

    private GameObject Player;

    [Header("L'ennemis nous regarde t-il ? :")]
    public bool stare;

    [Header("L'ennemis est-il de passge ? :")]
    public bool passage;

    [Header("Temps avant de faire disparaitre un ennemis :")]
    public float TimeBeforeDestroy;

    [Header("Vitesse de rotation :")]
    public float SpeedRotate;

    [Header("Vitesse de déplacement :")]
    public float SpeedMoving;

    // Use this for initialization
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (!stare && !passage)
        {
            RotationToPlayer();
            MoveToPlayer();
        }
        else if (stare && !passage)
        {
            RotationToPlayer();
        }
        else if (!stare && passage)
        {
            MoveSomeWhere();
            StartCoroutine(WaitBeforeDestroy());
        }
        else
        {
            DesactiveMechant();
        }
    }

    public void DesactiveMechant()
    {
        this.enabled = false;
    }

    void RotationToPlayer()
    {
        // ---------- Déplacement vers Joueur -------------//
        Quaternion rotationAngle = Quaternion.LookRotation(Player.transform.position - transform.position); // we get the angle has to be rotated
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationAngle, Time.deltaTime * SpeedRotate); // we rotate the rotationAngle 
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0); //bloquer sur l'axe y
    }

    void MoveToPlayer()
    {
        // ---------- Déplacement vers Joueur -------------//
        transform.position += transform.forward * SpeedMoving * Time.deltaTime;
    }

    void MoveSomeWhere()
    {
        transform.position += transform.forward * SpeedMoving * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player.GetComponent<Player_dead>().MortPlayer();
        }
    }

    IEnumerator WaitBeforeDestroy()
    {
        yield return new WaitForSeconds(TimeBeforeDestroy);
        DesactiveMechant();
    }
}
