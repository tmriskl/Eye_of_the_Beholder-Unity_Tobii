using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLocationScript : MonoBehaviour {

    public GameObject target;
    private Transform start;

    void Start()
    {
        start.rotation = gameObject.transform.rotation;
        start.position = gameObject.transform.position;
    }

	void Update () {
        gameObject.transform.position = target.transform.position + start.position;
        gameObject.transform.rotation =target.transform.rotation*start.rotation;

    }
}
