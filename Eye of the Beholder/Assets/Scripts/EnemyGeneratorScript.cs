using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorScript : MonoBehaviour {
    public AIPlayerStats playerStats;
    // public Transform location1;
    //public GameObject Orc;
    //public GameObject Skeleton;
    //public bool skeleton = false;
    public GameObject[] Orcs;
    public GameObject[] Skeletons;
    public bool createdMonsters = false;
   // public int j=0;
	// Use this for initialization
	void Start ()
    {
        //Skeleton.SetActive(true);
        //Debug.Log("set activeeeeeeeeeeeeeeeee " + Skeleton);
        //Orc = GetComponent<GameObject>();
        //Skeleton = GetComponent<GameObject>();
        // Orcs = new GameObject[3];
        // Skeletons = new GameObject[3];
    }
	
	// Update is called once per frame
	void Update () {
    }

    /*void SetActiveSkeleton()
    {
        //Skeleton= GetComponent<GameObject>();
        Debug.Log("set activeeeeeeeeeeeeeeeee " + Skeleton + " " + gameObject.name);
        Skeleton.SetActive(true);
    }*/

    void OnTriggerExit(Collider col)
    {

        //   orcs[1].SetActive(true);
        if (col.gameObject.tag == "Player")
        {
            //Debug.Log("entered trigger" +j+" " + col.name);
            if ((playerStats.readStat(playerStats.getStatLocationByName("bone")) <= playerStats.readStat(playerStats.getStatLocationByName("monster"))) && !createdMonsters)
            {
                //Debug.Log("create orc" + Orcs.Length);
                //Debug.Log((playerStats.getStatLocationByName("bone")));
                //Orc.SetActive(true);
                for (int i = 0; i < Orcs.Length; i++)
                {
                    //Debug.Log("FUCK ME " +i);
                    Orcs[i].SetActive(true);
                }
                //Debug.Log("create orc after");
                // Instantiate(orc, location1.position, Quaternion.identity);
                createdMonsters = true;
            }
            else if ((playerStats.readStat(playerStats.getStatLocationByName("bone")) > playerStats.readStat(playerStats.getStatLocationByName("monster"))) && !createdMonsters)
            {
                Debug.Log("create skelelton" + Skeletons.Length);
                //Skeleton = GetComponent<GameObject>();
                // SetActiveSkeleton();
                //Debug.Log("set activeeeeeeeeeeeeeeeee " + this.Skeleton + " " + skeleton + " " + gameObject.name);
                //if (this.Skeleton == null) Debug.Log("no skelelton " + Skeleton);
                //Skeleton.SetActive(true);
                //// Skeleton.SetActive(true);
                for (int i = 0; i < Skeletons.Length; i++)
                {
                    Skeletons[i].SetActive(true);
                }
                //Debug.Log("create skeleton after");
                createdMonsters = true;
            }
            //Instantiate(key, transform.position, Quaternion.identity);
        }
    }
}
