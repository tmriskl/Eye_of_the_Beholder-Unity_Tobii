using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Tobii.Gaming;

public class GazeableNPCScript : GazeableScript
{
    private NavMeshAgent agent;
    public Transform playerPos = null;
    public Transform NPCPos = null;
    public float distanceToTarget;
    public readonly float maxDistanceToTarget = 5;
    //public double[] add;
    //public AIPlayerStats aiChooseNpc;
    void Start()
    {
        _gazeAwareComponent = GetComponent<GazeAware>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        switch(gameObject.name)
        {
            case "witch doctor":
                gazeName = Wizard_Name;
                break;
            case "A03":
                gazeName = Amazona_Name;
                break;
            case "Dwarf BerserkerM":
                gazeName = Dwarf_Name;
                break;
            case "Dwarf Master":
                gazeName = mDwarf_Name;
                break;
        }

    }



    void FixedUpdate()
    {
        if (null != playerPos)
        {
            Vector3 vectorToTarget = playerPos.position - transform.position;
            vectorToTarget.y = 0;
            distanceToTarget = Mathf.Abs(vectorToTarget.magnitude);
        }
        else
            distanceToTarget = maxDistanceToTarget + 1;

        if (distanceToTarget > maxDistanceToTarget)
            playerPos = null;

        if (null != playerPos)
        {
            //aiChooseNpc.updateOptions(add);
            /*if (agent.isStopped == true)
                agent.isStopped = false;
            GetComponent<NPCAnimationScript>().StandAnimation();*/
            transform.LookAt(playerPos);
        }
        else
        {
           /* if (agent.isStopped == false)
            {
                agent.isStopped = true;
                GetComponent<NPCAnimationScript>().WalkAnimation();
            }*/

        }
        
    }

    public override void Use()
    {
    }
}
