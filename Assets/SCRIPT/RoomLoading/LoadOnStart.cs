﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOnStart : MonoBehaviour {
	void Start () {
        GetComponent<RoomLoader>().LoadRooms();
	}
}
