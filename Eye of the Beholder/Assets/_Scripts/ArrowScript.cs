using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {
    public float speed = 0;
    private Vector3 direction = new Vector3(0,0,-1);
    private int counter = 100; 

	void Update () {
        transform.Translate(direction * speed);
        counter--;
        if (counter <= 0)
        {
            Destroy(gameObject);
        }
	}
}
