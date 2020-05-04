using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePowerScript : PowerScript {

    public bool rotate;
    public bool active = false;

    void Start()
    {
        gameObject.SetActive(active);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PowerScript power = other.gameObject.GetComponent<PowerScript>();
            power.addPower(this);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (rotate)
        {
            gameObject.transform.Rotate(0, 1, 0);
        }
    }
}
