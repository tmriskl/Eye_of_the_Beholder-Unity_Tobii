using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPointScript : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
         //   other.gameObject.GetComponent<GazeableNPCScript>().nextPoint();
        }
    }
}
