using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WizardScript : MonoBehaviour {

    public Transform target;
    UnityEngine.AI.NavMeshAgent agent;
    public Animation anim;
    [SerializeField]
    public Stat hp;
    public bool attacking;
    public bool chaseStarted;
    private PlayerScript playerScript;
    public bool dead = false;
    private bool playedDead = false;
    public GameObject orb;
    private double effectInterval = 0.1;
    private double[] effects = { 0, 0 ,0 };
    float startSpeed = 0;
    private readonly float MaxResist = 100;
    private double[] resists = { 80, 80, 80 };
    private double[] effectsStart = { 10, 30, 15 };
    public GameObject[] visualEffects;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animation>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        hp.Initialize();
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        startSpeed = agent.speed;
        effects = new double[PowerScript.NumEffects];
    }

    // Update is called once per frame
    void Update()
    {
        if (hp.currentVal <= 0 && !dead)
            dead = true;
        if (!dead)
        {
            if (chaseStarted)
            {
                if ((transform.position - target.position).sqrMagnitude < 3)
                {

                    if (!attacking)
                        Attack();
                    Vector3 direction = (target.position - transform.position).normalized;
                    direction.y = 0;
                    Quaternion qDir = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, qDir, Time.deltaTime * 4f);
                    //if ((transform.position - target.position).sqrMagnitude < 0.6 && playerScript.attacking && playerScript.hitEnemyWithRay)
                    //{
                    //    hp.currentVal--;
                    //    hp.Initialize();
                    //}
                }
                else
                {
                    if (!anim.IsPlaying("move_forward")) anim.Play("move_forward");

                    agent.SetDestination(target.position);
                }

            }
        }
        else if(!playedDead)
        {
            PlayDead();
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
                    hp.currentVal -= (float)PowerScript.effectBurnDamage;
                    hp.Initialize();
                    //Debug.Log("Burn" + effects[PowerScript.effectBurn]);
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
                    //Debug.Log("Freeze" + effects[PowerScript.effectFreeze]);
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
                    hp.currentVal -= (float)PowerScript.effectPoisionDamage;
                    hp.Initialize();

                    agent.speed = agent.speed / 2;
                    //Debug.Log("Poision" + effects[PowerScript.effectPoision]);
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
    //void OnCollisionEnter(Collision col)
    //{
    //    Debug.Log("col: " + col.gameObject.name);

    //    if (col.gameObject.CompareTag("Weapon"))
    //    {

    //        Debug.Log("powerrrrrrrrrrrrrr: " + col.gameObject.GetComponent<DamageScript>().getPower());
    //        hp.currentVal -= (float)(col.gameObject.GetComponent<DamageScript>().getPower());
    //        hp.Initialize();
    //        effects = new double[PowerScript.NumEffects];
    //        float[] tempEffects = col.gameObject.GetComponent<DamageScript>().getEffects();
    //        float rand = UnityEngine.Random.Range(0, MaxResist);
    //        if (rand >= resists[PowerScript.effectBurn])
    //            if ((effects.Length > PowerScript.effectBurn) && (effectsStart.Length > PowerScript.effectBurn))
    //            {
    //                effects[PowerScript.effectBurn] = effectsStart[PowerScript.effectBurn];
    //            }
    //        rand = UnityEngine.Random.Range(0, MaxResist);
    //        if (rand >= resists[PowerScript.effectFreeze])
    //        {
    //            if ((effects.Length > PowerScript.effectFreeze) && (effectsStart.Length > PowerScript.effectFreeze))
    //            {
    //                effects[PowerScript.effectFreeze] = effectsStart[PowerScript.effectFreeze];
    //            }
    //        }
    //        rand = UnityEngine.Random.Range(0, MaxResist);
    //        if (rand >= resists[PowerScript.effectPoision])
    //            if ((effects.Length > PowerScript.effectPoision) && (effectsStart.Length > PowerScript.effectPoision))
    //            {
    //                effects[PowerScript.effectPoision] = effectsStart[PowerScript.effectPoision];
    //            }

    //    }
    //}
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("col: " + col.gameObject.name);

        if (col.gameObject.CompareTag("Weapon"))
        {

            Debug.Log("powerrrrrrrrrrrrrr: " + col.gameObject.GetComponent<DamageScript>().getPower());
            hp.currentVal -= (float)(col.gameObject.GetComponent<DamageScript>().getPower());
            hp.Initialize();
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
    }


    void PlayDead()
    {
        playedDead = true;
        anim.CrossFade("dead", 0.2f);
        StartCoroutine(CreateOrb());

    }
    IEnumerator CreateOrb()
    {
        yield return new WaitForSeconds(4);
        orb.SetActive(true);
        orb.transform.position = transform.position + Vector3.up;
        //Instantiate(orb, , Quaternion.identity);
    }
    void Attack()
    {
        StartCoroutine(AttackRoutine());
    }
    IEnumerator AttackRoutine()
    {
        attacking = true;
        anim.Play("attack_short_001");
        RaycastHit hit;
        Ray landingRay = new Ray(transform.position + Vector3.up, transform.forward * 2);
        Debug.DrawRay(transform.position + Vector3.up, transform.forward * 2);
        if (Physics.Raycast(landingRay, out hit, 5))
        {
            if (hit.collider.tag == "Player")
            {
                GameObject.Find("Player").GetComponent<PlayerScript>().hp.currentVal--;
                GameObject.Find("Player").GetComponent<PlayerScript>().hp.Initialize();
            }
        }
        else
        {
            Debug.Log("missed");
        }
        yield return new WaitForSeconds(1);
        if(!dead)anim.Play("move_forward");
        attacking = false;
    }
}
