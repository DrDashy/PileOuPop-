using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageDisplayer : MonoBehaviour, IActivable {

    public Material highlightMaterial;

    protected MeshRenderer meshRend;
    protected Material[] initialMaterial;

    void Start()
    {
        meshRend = GetComponent<MeshRenderer>();
        if (meshRend == null)
            Debug.Log("Pas de MeshRenderer trouvé");
        initialMaterial = meshRend.materials;
    }

    void Update()
    {
        meshRend.materials = initialMaterial;
    }

    public void Activate()
    {
        Debug.Log("Blblblbl");
    }

    public void Highlight()
    {
        Material[] mats = meshRend.materials;
        mats[1] = highlightMaterial;
        meshRend.materials = mats;
    }
}
