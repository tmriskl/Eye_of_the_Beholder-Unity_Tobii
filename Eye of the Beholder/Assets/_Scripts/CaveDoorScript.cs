using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveDoorScript : DoorScript {


    public float max = 1.0f;
    float step = 0.01f;

    private void Start()
    {
        base.PStart();
    }
    // Update is called once per frame
    void Update () {
        base.PUpdate();
        if (open)
        {
            max -= step;
            transform.Translate(0, step, 0);
            if (max < 0)
            {
                Destroy(gameObject);
            }
        }
		
	}
}
