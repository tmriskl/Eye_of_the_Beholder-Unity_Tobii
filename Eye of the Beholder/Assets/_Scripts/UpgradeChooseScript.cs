using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeChooseScript : MonoBehaviour {

    public GameObject[] upgrades;
    public string[] stats;
    double[] stat_value;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Choose(other.gameObject.GetComponent<AIPlayerStats>());
        }
    }

    private void Choose(AIPlayerStats aIPlayerStats)
    {
        stat_value = new double[stats.Length];
        for (int i = 0; i < stats.Length; i++)
        {
            stat_value[i] = aIPlayerStats.readStat(aIPlayerStats.getStatLocationByName(stats[i]));
        }

        int max = 0;
        for (int i = 1; i < stats.Length; i++)
        {
            if (stat_value[i] > stat_value[max])
                max = i;
        }
        

        upgrades[max].gameObject.SetActive(true);


    }
    
}
