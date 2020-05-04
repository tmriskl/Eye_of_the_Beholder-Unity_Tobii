using UnityEngine;
using System.Collections;

public class StartChaseTriggerScript : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            
            GameObject.Find("Wizard").GetComponent<WizardScript>().chaseStarted = true;
            
        }
    }
}
