using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTimeMachine : MonoBehaviour, IActivable {

	public Material highlightMaterial;
	public RoomLoader loader;
	public GameObject nextroom;

	protected MeshRenderer meshRend;
	protected Material[] initialMaterial;
	protected bool IsPuched;

	void Start()
	{
		meshRend = GetComponent<MeshRenderer>();
		if (meshRend == null)
			Debug.Log("Pas de MeshRenderer trouvé");
		initialMaterial = meshRend.materials;
		IsPuched = false;
	}

	void Update()
	{
		meshRend.materials = initialMaterial;
	}

	public void Activate()
	{
		if (IsPuched == false) {
			IsPuched = true;
			loader.RoomBack = nextroom;
			loader.PlacementBack = new Vector3 (0.25611f, -6.8188f, -25f);
			loader.LoadRooms ();
		}
	}

	public void Highlight()
	{
		Material[] mats = meshRend.materials;
		mats[1] = highlightMaterial;
		meshRend.materials = mats;
	}
}
