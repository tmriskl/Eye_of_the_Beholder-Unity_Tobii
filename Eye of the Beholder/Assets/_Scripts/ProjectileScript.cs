using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageScript))]
public class ProjectileScript : MonoBehaviour {
    public GameObject player;
    public Vector3 projectile_dir;
    public float projectile_speed = 30;
    // Use this for initialization
    void Start () {
        gameObject.GetComponent<Rigidbody>().velocity = projectile_dir * projectile_speed;
        DamageScript arrow = GetComponent<DamageScript>();
        arrow.setPower(player.GetComponent<PowerScript>().getRangePower());
        arrow.setEffects(player.GetComponent<PowerScript>().getRangeEffects());
	}
	
	// Update is called once per frame
	/*void Update () {
        //gameObject.transform.LookAt(look);
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Player")
            Destroy(gameObject);
    }
}
