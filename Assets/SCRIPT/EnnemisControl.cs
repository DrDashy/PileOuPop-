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

    [HideInInspector]
    public bool PlayerDeadForSure;

    private GameObject MenuPause;

    private Animator anim;

    [Header("AudioSource chase le joueur :")]
    public AudioSource AudioChase;

    [Header("Vitesse de FadeIn et FadeOut du son : ")]
    public float speedVolume;

    [Header("AudioSource qui tue le joueur :")]
    public AudioSource AudioKill;
    public AudioClip KillSound;

    [Header("AudioSource qui casse les os du joueur :")]
    public AudioSource AudioBreakBones;
    public AudioClip BreakBonesSound;

    // Use this for initialization
    void Start () {
        MenuPause = GameObject.FindGameObjectWithTag("MenuPause");
        anim = GetComponent<Animator>();
        anim.SetBool("Kill", false);
        Player = GameObject.FindGameObjectWithTag("Player");
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
        } else
        {
            RotationToPlayer();
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
            FadeOut();
            AudioKill.PlayOneShot(KillSound);
            EnnemisBackALittle();
            MenuPause.GetComponent<PauseMenu>().PlayerDeadForSure = true;
            RotationToEnnemis();
            PlayerDeadForSure = true;
            anim.SetBool("Kill", true);
            Player.GetComponent<Player_dead>().MortPlayer();
            StartCoroutine(WaitAfficheMenuMort());
        }
    }

    void RotationToEnnemis()
    {
        // Player.transform.LookAt(transform.position);
        Player.transform.GetChild(0).LookAt(transform.position);
    }

    private void EnnemisBackALittle()
    {
        // ---------- Déplacement vers Joueur -------------//
        transform.position += Vector3.back * 20f * Time.deltaTime;
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
        AudioBreakBones.PlayOneShot(BreakBonesSound);
        StartCoroutine(WaitBeforeStopAllSound());
    }

    IEnumerator WaitBeforeStopAllSound()
    {
        yield return new WaitForSeconds(1f);
        // Desactive tous les sons
        AudioListener.pause = true;
    }

    void FadeOut()
    {
        if (AudioChase.volume > 0)
        {
            AudioChase.volume -= speedVolume;
            Invoke("FadeOut", 0.5f);
        }
    }
}
