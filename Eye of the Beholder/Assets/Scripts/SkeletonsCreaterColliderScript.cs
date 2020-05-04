using UnityEngine;
using System.Collections;

public class SkeletonsCreaterColliderScript : MonoBehaviour {
    public int numOfSkeletons=5;
    public Transform createSpot;
    public GameObject skeleton;
    public int timesGenerate = 1;
    // Use this for initialization
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player" && timesGenerate > 0)
        {
            for (int i = 0; i < numOfSkeletons; i++)
            {
                Instantiate(skeleton, createSpot.position, Quaternion.identity);
            }
            timesGenerate--;
        }
	}
	
}
