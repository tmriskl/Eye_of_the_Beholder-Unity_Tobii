using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCaseScript : MonoBehaviour {

    public AudioSource shell_sound;
    public Vector3 case_dir;
    public float speed;
    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity = case_dir * speed;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        shell_sound.Play();
        Destroy(gameObject);
    }

}
