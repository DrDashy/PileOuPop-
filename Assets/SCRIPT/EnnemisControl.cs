using System.Collections;
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

    [Header("Temps avant d'afficher le menu mort :")]
    public float TimeBeforeAfficheMenu;

    [Header("Vitesse de rotation :")]
    public float SpeedRotate;

    [Header("Vitesse de rotation quand Mort :")]
    public float SpeedRotateDead;

    [Header("Vitesse de déplacement :")]
    public float SpeedMoving;

    private bool PlayerDeadForSure;

    private Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        anim.SetBool("Kill", false);
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Use this for initialization
    void Update()
    {
        if (PlayerDeadForSure)
        {
            //RotationToEnnemis();
        }
    }

    void FixedUpdate()
    {
        if (!PlayerDeadForSure)
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
            EnnemisBackALittle();
            RotationToEnnemis2();
            PlayerDeadForSure = true;
            anim.SetBool("Kill", true);
            Player.GetComponent<Player_dead>().MortPlayer();
            StartCoroutine(WaitAfficheMenuMort());
        }
    }

    void RotationToEnnemis()
    {
        // ---------- Déplacement vers Ennemis -------------//
        Quaternion rotationAngle = Quaternion.LookRotation(Player.transform.position - transform.position); // we get the angle has to be rotated
        Player.transform.rotation = Quaternion.Slerp(transform.rotation, rotationAngle, Time.deltaTime * SpeedRotateDead); // we rotate the rotationAngle 
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0); //bloquer sur l'axe y
    }

    void RotationToEnnemis2()
    {
        Player.transform.LookAt(transform.position);
    }

    private void EnnemisBackALittle()
    {
        // ---------- Déplacement vers Joueur -------------//
        transform.position = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);
    }

    IEnumerator WaitBeforeDestroy()
    {
        yield return new WaitForSeconds(TimeBeforeDestroy);
        DesactiveMechant();
    }

    IEnumerator WaitAfficheMenuMort()
    {
        yield return new WaitForSeconds(TimeBeforeAfficheMenu);
        Player.GetComponent<Player_dead>().AfficheMenuMortPlayer();
        PlayerDeadForSure = false;
    }
}
