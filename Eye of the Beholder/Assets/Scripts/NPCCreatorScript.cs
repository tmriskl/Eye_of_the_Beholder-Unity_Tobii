using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NPCCreatorScript : MonoBehaviour {

    public AIPlayerStats stats;
    public NPCNavMeshScript[] NPCs;
    public bool first = true;

    void Update()
    {
        if (first&&stats.ready)
        {
            for (int i = 0; i < NPCs.Length; i++)
            {
                NPCs[i].stop = true;
                NPCs[i].gameObject.SetActive(false);
            }
            int max = stats.conclusion();

            max %= NPCs.Length;
            NPCs[max].gameObject.SetActive(true);
            first = false;
        }
    }


}
