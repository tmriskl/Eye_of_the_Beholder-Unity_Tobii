using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class BehaviourPlayerStats : BehaviourObject
{
    bool first = true;

    void Start()
    {
        _gazeAwareComponent = GetComponent<GazeAware>();
    }

    public/* override*/ void updateOptions(AIPlayerStats Optional_AI)
    {
        
        if (AI == null)
            AI = Optional_AI;
     
            if (first)
        {
            first = false;
            int stat = ((AIPlayerStats)AI).stats.Length;
            if (options == null)
            {
                options = new double[stat + 1];
                for (int i = 0; i <= stat; i++)
                {
                    options[i] = 0;
                }
            }

            if (stat >= options.Length)
            {
                double[] newOptions = new double[stat + 1];
                for (int i = 0; i <= stat; i++)
                {
                    if (i < options.Length)
                        newOptions[i] = options[i];
                    else
                    {
                        newOptions[i] = 0;
                    }
                }
                options = newOptions;
            }
        }
        for (int i = 0; i < options.Length; i++)
            ((AIPlayerStats)AI).updateStat(options[i], i);

        if (((AIPlayerStats)AI).Equals(Optional_AI))
            AI = null;
    }

    // Update is called once per frame
    void FixedUpdate() {
       /* if (_gazeAwareComponent.HasGazeFocus)
        {
            updateOptions();
        }*/
    }
}
