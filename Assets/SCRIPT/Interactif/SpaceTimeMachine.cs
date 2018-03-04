using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTimeMachine : MonoBehaviour, IActivable {

	public Material highlightMaterial;
	public RoomLoader loader;
	public GameObject nextroom;
	public GameObject Tuyaud;
	protected MeshRenderer meshRend;
	protected Material[] initialMaterial;
	protected bool IsPuched;

	public AudioClip ButtonSound;
	public AudioSource audiosource;

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
			loader.PlacementBack = new Vector3 (1.04f, -6.9141e-05f, -23.7f);
			loader.LoadRooms ();
			Tuyaud.GetComponent<madMachine> ().CanDegenerate = true;
			audiosource.PlayOneShot (ButtonSound, 1f);
		}
	}

	public void Highlight()
	{
		Material[] mats = meshRend.materials;
		mats[1] = highlightMaterial;
		meshRend.materials = mats;
	}
}
