using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerScript>().updateHP();
            Destroy(gameObject);
        }
    }
}
