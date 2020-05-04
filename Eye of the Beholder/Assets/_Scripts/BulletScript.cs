using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public GameObject player;
    public ProjectileScript projectile;
    public Vector3 projectile_dir;
    private float projectile_speed = 10f;
    private int counter = 500;
    //public AudioSource shot;
    //public BulletCaseScript bullet_case;
    //public Vector3 case_dir;
    //private float case_speed = 5;

    // Use this for initialization
    void Start ()
    {
        projectile.projectile_dir = player.transform.forward;
        //bullet_case.case_dir = player.transform.right + player.transform.up;
        gameObject.transform.position = player.transform.position + player.transform.forward*0.5f + new Vector3(0,2f,0);
        
        //shot.loop = false;
        //shot.Play();
        //projectile.look = Vector3.zero - player.transform.position * 10000;
        projectile.transform.rotation = player.transform.rotation;
        projectile.transform.Rotate(new Vector3(0, 180, 0));
        projectile.projectile_speed = projectile_speed;
        projectile.player = player;
       // bullet_case.transform.rotation = player.transform.rotation;
       // bullet_case.transform.Rotate(new Vector3(0, 180, 0));
       // bullet_case.speed = case_speed;
    }
	
	// Update is called once per frame
	void Update ()
    {
        counter--;
        //projectile.gameObject.GetComponent<Rigidbody>().velocity = projectile_dir * projectile_speed;
        //bullet_case.gameObject.GetComponent<Rigidbody>().velocity = case_dir * case_speed;
        //projectile.gameObject.transform.Translate(projectile_dir * projectile_speed);
        //bullet_case.gameObject.transform.Translate(case_dir * case_speed);
        if (counter <= 0) {
            //Destroy(bullet_case.gameObject);
            //Destroy(projectile.gameObject);
            Destroy(gameObject);
        }
    }
}
