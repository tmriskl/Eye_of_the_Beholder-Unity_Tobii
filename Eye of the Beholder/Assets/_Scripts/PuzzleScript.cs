using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleScript : MonoBehaviour {

    public DoorScript door;
    protected bool done;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void setDoor(DoorScript doorScript)
    {
        door = doorScript;
    }
}
