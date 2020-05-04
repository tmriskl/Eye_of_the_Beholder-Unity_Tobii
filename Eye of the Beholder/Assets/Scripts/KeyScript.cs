using UnityEngine;
using System.Collections;

public class KeyScript : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerScript>().keysCollected++;
            Destroy(gameObject);
        }
    }
}
