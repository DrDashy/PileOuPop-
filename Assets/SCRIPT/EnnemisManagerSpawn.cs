using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemisManagerSpawn : MonoBehaviour {

    [Header("GameObject Poursuivant Ennemis :")]
    public GameObject PoursuivantEnnemis;

    [Header("Temps entre chaque spawn de poursuivant :")]
    [Header("Min Temps :")]
    public float TempsMinSpawn;
    [Header("Max Temps :")]
    public float TempsMaxSpawn;

    [Header("Distance :")]
    public float Distance;

    private GameObject Player;

    [HideInInspector]
    public bool CloneCreer;

    [Header("Nombre de salle avant dispawn :")]
    public int conteurSalle;

    // Use this for initialization
    void Start () {
        conteurSalle = 0;
        CloneCreer = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(WaitToSpawnPoursuivant());
    }

    private void SpawnPoursuivant()
    {
        GameObject clone = Instantiate(PoursuivantEnnemis);
        float rand = Random.Range(-Mathf.PI, Mathf.PI);
        clone.transform.position = Player.transform.position + new Vector3(Mathf.Cos(rand)*Distance, 1.4f, Mathf.Sin(rand) * Distance);
        CloneCreer = true;
        CheckCloneRoomEnter(clone);
    }

    private void CheckCloneRoomEnter(GameObject clone)
    {
        if (conteurSalle < 0)
        {
            Destroy(clone);
            conteurSalle = 0;
            CloneCreer = false;
            StartCoroutine(WaitToSpawnPoursuivant());
        }
    }

    private IEnumerator WaitToSpawnPoursuivant()
    {
        yield return new WaitForSeconds(Random.Range(TempsMinSpawn,TempsMaxSpawn));
        SpawnPoursuivant();
    }
}
