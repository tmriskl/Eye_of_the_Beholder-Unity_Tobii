using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;

public class NPCNavMeshScript : GazeableScript {
    UnityEngine.AI.NavMeshAgent agent;
    public Transform[] moveSpots;
    private int randomSpot;
    public float startWaitTime;
    public float waitTime;
    public bool walk;
    public NPCAnimationScript npcAnimationScript;

    //public Transform playerPos = null;
    public float distanceToTarget;
    public float maxDistanceToTarget = 5;
    public bool stop = false;
    public Transform playerPos;
    public Transform NPCPos;
    public float y = 0;
    public static int maxCount = 20;
    public int count = maxCount;
    // Use this for initialization
    void Start ()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        randomSpot = Random.Range(0,moveSpots.Length);
        waitTime = startWaitTime;
        walk = true;
        npcAnimationScript.WalkAnimation();
        _gazeAwareComponent = GetComponent<GazeAware>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        switch (gameObject.name)
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
        action = NPC_Info[Random.Range(0, NPC_Info.Length-1)];
    }

    // Update is called once per frame
    void Update()
    {
        if ((null != playerPos)&&(null != player))
        {
            stop = true;
            Vector3 vectorToTarget = playerPos.position - NPCPos.position;
            vectorToTarget.y = 0;
            distanceToTarget = Mathf.Abs(vectorToTarget.magnitude);
            Debug.Log(distanceToTarget);

            if(distanceToTarget < maxDistanceToTarget)
            {
                count = maxCount;
            }

            if (distanceToTarget > maxDistanceToTarget)
            {
                count--;
                if (count == 0)
                {
                    count = maxCount;
                    player.setTexts("", "", "");
                    player.setNPC(false);
                    player = null;
                    playerPos = null;
                    action = NPC_Info[Random.Range(0, NPC_Info.Length - 1)];
                    stop = false;
                }
            }

            if (null != player)
            {
                transform.LookAt(playerPos);
                transform.rotation = new Quaternion(0, transform.rotation.y+this.y, 0, transform.rotation.w);
                player.setTexts("", gazeName, action);
                player.setNPC(true);
            }
        }
        if (stop)
        {
            npcAnimationScript.StandAnimation();
            agent.enabled = false;
        }
        else
        {
            npcAnimationScript.WalkAnimation();
            agent.enabled = true;
        }
        //Debug.Log(player + " " + distanceToTarget);
        if (!stop)
        {
            if(agent.isActiveAndEnabled)
                agent.SetDestination(moveSpots[randomSpot].position);
            Vector3 vectorToTarget = moveSpots[randomSpot].position - NPCPos.position;
            vectorToTarget.y = 0;
            float distanceToTarget = Mathf.Abs(vectorToTarget.magnitude);

            //Debug.Log(randomSpot + ", " + transform.position + ", " + moveSpots[randomSpot].position + ", " + distanceToTarget + ", " + waitTime);

            //Vector3.Distance(transform.position, moveSpots[randomSpot].position)
            if (distanceToTarget < 1f)
            {
                /*if (waitTime <= 0)
                {*/
                    randomSpot = Random.Range(0, moveSpots.Length);
               /*     waitTime = startWaitTime;
                    stop = true;
                }
                else
                {
                    stop = false;
                    waitTime -= Time.deltaTime;
                }*/
            }
        }
    }
    public override void Use()
    {
    }
}
