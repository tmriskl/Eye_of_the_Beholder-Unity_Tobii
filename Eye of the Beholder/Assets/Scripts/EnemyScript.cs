using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour {
    public Transform target;
    UnityEngine.AI.NavMeshAgent agent;
    float startSpeed = 0;
    public Animator anim;
    public double hp = 25;
    public Rigidbody rb;
    public bool attacking;
    public Transform[] moveSpots;
    private int randomSpot;
    public bool chaseStarted;
    public float startWaitTime;
    private float waitTime;
    public Image alert;
    private Color redAlpha;
    private int alertUp = 1;
    public GameObject key;
    public PlayerScript playerScript;
    private bool isEntering=false;
    public int entering = 0;
    private readonly float MaxResist = 10;
    private double effectInterval = 0.5;
    public double[] resists = { 0, 0, 0 };
    public double[] effectsStart = { 3, 3, 3 };
    private double[] effects = {0,0,0};
    public GameObject[] visualEffects;

    // Use this for initialization
    void Start ()
    {
        //key = GetComponent<GameObject>();
        anim = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        randomSpot = UnityEngine.Random.Range(0,moveSpots.Length);
        waitTime = startWaitTime;
        if (playerScript==null)
            playerScript = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerScript>();
        startSpeed = agent.speed;
        effects = new double[PowerScript.NumEffects];
    }
	
	// Update is called once per frame
	void Update ()
    {

        entering--;
        if (chaseStarted)
        {
            if ((gameObject.tag.Contains("Orc") || gameObject.tag.Contains("Skeleton")) && (transform.position - target.position).sqrMagnitude < 75)
            {
                Alert();
            }
            else
            {
                if (alert.color.a != 0)
                {
                    redAlpha = alert.color;
                    redAlpha.a = 0;
                    alert.color = redAlpha;
                }
            }

            if ((transform.position - target.position).sqrMagnitude < 3)
            {

                if (anim.GetInteger("condition") != 2 && !attacking)
                    Attack();
                Vector3 direction = (target.position - transform.position).normalized;
                direction.y = 0;
                Quaternion qDir = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, qDir, Time.deltaTime * 4f);
                if ((transform.position - target.position).sqrMagnitude < 0.6 && GameObject.Find("Player").GetComponent<PlayerScript>().attacking == true)
                {
                    hp--;
                }
                //agent.Stop();
                /*if ((transform.position - target.position).sqrMagnitude < 0.6)
                {
                    if (gameObject.name == "Skeleton")
                        rb.AddForce((transform.position - target.position).normalized * 50);
                    //  Debug.Log("Pushing "+gameObject.name);
                }*/
            }
            else
            {
                if (anim.GetInteger("condition") != 1) anim.SetInteger("condition", 1);
                //agent.Resume();
                agent.SetDestination(target.position);
            }

        }
        else
        {
            if (Vector3.Distance(transform.position, target.position) < 20f)
            {
                chaseStarted = true;
            }
            else
            {
                agent.SetDestination(moveSpots[randomSpot].position);
                if (Vector3.Distance(transform.position, moveSpots[randomSpot].position) < 0.5f)
                {
                    if (waitTime <= 0)
                    {
                        randomSpot = UnityEngine.Random.Range(0, moveSpots.Length);
                        waitTime = startWaitTime;
                        if (anim.GetInteger("condition") != 1) anim.SetInteger("condition", 1);
                    }
                    else
                    {
                        if (anim.GetInteger("condition") != 0) anim.SetInteger("condition", 0);
                        waitTime -= Time.deltaTime;
                    }
                }
            }
        }

        manageEffects();

    }
    private void manageEffects()
    {
        if (effects != null)
        {
            if (effects.Length > PowerScript.effectBurn)
            {
                effects[PowerScript.effectBurn] -= effectInterval;
                if (effects[PowerScript.effectBurn] > 0)
                {
                    hp -= (float)PowerScript.effectBurnDamage;
                    Debug.Log("Burn" + effects[PowerScript.effectBurn]);
                    visualEffects[PowerScript.effectBurn].SetActive(true);
                }
                else
                {
                    visualEffects[PowerScript.effectBurn].SetActive(false);
                }
            }
            agent.speed = startSpeed;
            if (effects.Length > PowerScript.effectFreeze)
            {
                effects[PowerScript.effectFreeze] -= effectInterval;
                //Debug.Log("Freeze1 " + effects[PowerScript.effectFreeze]);
                if (effects[PowerScript.effectFreeze] > 0)
                {
                    agent.speed = 0;
                    Debug.Log("Freeze" + effects[PowerScript.effectFreeze]);
                    visualEffects[PowerScript.effectFreeze].SetActive(true);
                }
                else
                {
                    visualEffects[PowerScript.effectFreeze].SetActive(false);
                }
            }
            if (effects.Length > PowerScript.effectPoision)
            {
                effects[PowerScript.effectPoision] -= effectInterval;
                if (effects[PowerScript.effectPoision] > 0)
                {
                    hp -= (float)PowerScript.effectPoisionDamage;

                    agent.speed = agent.speed / 2;
                    Debug.Log("Poision" + effects[PowerScript.effectPoision]);
                    visualEffects[PowerScript.effectPoision].SetActive(true);
                }
                else
                {
                    visualEffects[PowerScript.effectPoision].SetActive(false);
                }
            }
        }
        //Debug.Log("agent.speed" + agent.speed);
    }

    void Alert()
    {
        redAlpha = alert.color;
        if (alertUp == 1)
            redAlpha.a += 0.03f;
        else if (alertUp == 0)
            redAlpha.a -= 0.03f;
        if (redAlpha.a >= 0.3f)
            alertUp = 0;
        if (redAlpha.a <= 0f)
            alertUp = 1;
        alert.color = redAlpha;

    }
    void Attack()
    {
        StartCoroutine(AttackRoutine());
    }
    IEnumerator AttackRoutine()
    {
        attacking = true;
        anim.SetInteger("condition", 2);
        RaycastHit hit;
        Ray landingRay = new Ray(transform.position + Vector3.up, transform.forward * 2);
        Debug.DrawRay(transform.position + Vector3.up, transform.forward*2);
        if (Physics.Raycast(landingRay, out hit, 5))
        {
            //Debug.Log("hit "+ hit.collider.tag);
            if (hit.collider.tag == "Player") {
                playerScript.hp.currentVal--;
                playerScript.hp.Initialize();
            }
        }
        else
        {
            Debug.Log("missed");
        }

        yield return new WaitForSeconds(1);
        anim.SetInteger("condition", 1);
        attacking = false;
    }
    void OnCollisionEnter(Collision col)
    {
        
        if (isEntering == false && entering <0)
        {
            isEntering = true;
            Debug.Log("col: " + col.gameObject.name);

            if (col.gameObject.CompareTag("Weapon"))
            {

                Debug.Log("powerrrrrrrrrrrrrr: " + col.gameObject.GetComponent<DamageScript>().getPower());
                hp -= (float)(col.gameObject.GetComponent<DamageScript>().getPower());
                float[] tempEffects = col.gameObject.GetComponent<DamageScript>().getEffects();
                float rand = UnityEngine.Random.Range(0, tempEffects[PowerScript.effectBurn]);
                if (rand >= resists[PowerScript.effectBurn])
                    if ((effects.Length > PowerScript.effectBurn) && (effectsStart.Length > PowerScript.effectBurn))
                    {
                        effects[PowerScript.effectBurn] = effectsStart[PowerScript.effectBurn];
                    }
                rand = UnityEngine.Random.Range(0, tempEffects[PowerScript.effectFreeze]);
                if (rand >= resists[PowerScript.effectFreeze])
                {
                    if ((effects.Length > PowerScript.effectFreeze) && (effectsStart.Length > PowerScript.effectFreeze))
                    {
                        effects[PowerScript.effectFreeze] = effectsStart[PowerScript.effectFreeze];
                    }
                }
                rand = UnityEngine.Random.Range(0, tempEffects[PowerScript.effectPoision]);
                if (rand >= resists[PowerScript.effectPoision])
                    if ((effects.Length > PowerScript.effectPoision) && (effectsStart.Length > PowerScript.effectPoision))
                    {
                        effects[PowerScript.effectPoision] = effectsStart[PowerScript.effectPoision];
                    }

            }
            isEntering = false;
            entering = 100;
        }
        if(hp<=0)
        {
            Instantiate(key, transform.position, transform.rotation);
            Destroy(gameObject);
        }
      
    }
}
