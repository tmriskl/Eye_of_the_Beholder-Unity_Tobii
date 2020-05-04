using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {
    public float speed = 4;
    public float rotSpeed = 80;
    public float gravity = 8;
    public float rot = 0f;
    public float runningFactor=1;
    bool charging=false;
    public bool attacking = false;
    public Vector3 moveDir=Vector3.zero;
    public int keysCollected = 0;
    public float startingAPWaitingTime;
    private float APWaitingTime;
    private bool apOut = false;
    private bool dontAddAp = false;
    public float startingMPWaitingTime;
    private float MPWaitingTime;
    private bool mpOut = false;
    private bool dontAddMp = false;
    public bool moveScene = false;
    public bool hitEnemyWithRay=false;
    public DamageScript[] swords;
    public BulletScript arrow;
    private float ammoSpeed = 0.1f;
    private bool firstShot;

    [SerializeField]
    public Stat hp;
    [SerializeField]
    public Stat ap;
    [SerializeField]
    public Stat mp;

    CharacterController controller;
    public Animator anim;
    public float jumpForce=4;

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        arrow.player = gameObject;
       
    }
	
    private void Awake()
    {
        hp.Initialize();
        ap.Initialize();
        mp.Initialize();
    }

	// Update is called once per frame
	void Update () {
        RaycastHit hit;

        GetInput();
        Movement();

        if (mp.currentVal <= 0 && !mpOut)
        {
            mpOut = true;
            Debug.Log("Enterd mp<0");
            charging = false;
            runningFactor = 1;
            anim.SetInteger("condition", 0);
            Wait(5);
        }

        Ray landingRay = new Ray(transform.position + Vector3.up, transform.forward * 4);
        Debug.DrawRay(transform.position+Vector3.up, transform.forward*4);
        if(Physics.Raycast(landingRay,out hit,5))
        {
            
            if (hit.collider.tag.Contains("Orc") || hit.collider.tag.Contains("Wizard"))
            {
                hitEnemyWithRay = true;
            }
            else
            {
                hitEnemyWithRay = false;
            }

        }
        else
        {
            hitEnemyWithRay = false; 
        }
    }

   public void updateHP()
    {
        hp.currentVal = hp.maxVal;
        hp.Initialize();
    }
    void OnCollisionEnter(Collision col)
    {
        //Debug.Log("contains skeleton? "+col.gameObject.name.Contains("Skeleton")+", attacking?"+ GameObject.Find(col.gameObject.name).GetComponent<NavMeshScript>().attacking);
        if (col.gameObject.name.Contains("Orc") && GameObject.Find(col.gameObject.name).GetComponent<EnemyScript>().attacking == true)
        {
            if (hp.CurrentVal <= 0)
                GameOver();
            else
            {
                hp.CurrentVal -= 1;
            }
        }/*else if (col.gameObject.name.Contains("Skeleton") && GameObject.Find(col.gameObject.name).GetComponent<NavMeshScript>().attacking == true)
        {
            
            if (hp.CurrentVal <= 0)
                Debug.Log("Game Over");
            //GameOver();
            else
            {
                hp.CurrentVal -= 2;
                hp.Initialize();
            }
            
        }*/

    }
    void GameOver()
    {
        SceneManager.LoadScene(DeleteSaveScript.getLoseLevelNum());
    }
    void Movement()
    {

        dontAddAp = false;
        dontAddMp = false;
        if (ap.currentVal<=0 && !apOut)
        {
            apOut = true;
            Debug.Log("Enterd ap<0");
            charging = false;
            runningFactor = 1;
            anim.SetInteger("condition", 0);
            Wait(5);
           // ap.currentVal++;
        }
        if (controller.isGrounded)
        {
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                charging = false;
                runningFactor = 1;
                anim.SetInteger("condition", 0);
            }
            if (Input.GetKey(KeyCode.W))
            {
                if (anim.GetBool("attacking") == true)
                {
                    return;
                }
                else if (anim.GetBool("attacking") == false)
                {
                    if (Input.GetKey(KeyCode.LeftShift) && ap.currentVal>0 && !apOut)
                    {
                        charging = true;
                        ap.currentVal -= 0.5f;
                        dontAddAp = true;
                        ap.Initialize();
                        runningFactor = 2.5f;
                        anim.SetInteger("condition", 3);
                    }
                    if (Input.GetKeyUp(KeyCode.LeftShift))
                    {
                        charging = false;
                        runningFactor = 1;
                        anim.SetInteger("condition", 0);
                    }
                    anim.SetBool("running", true);
                    if(!charging)
                        anim.SetInteger("condition", 1);
                    moveDir = new Vector3(0, 0, 1);
                    moveDir *= speed*runningFactor;
                    moveDir = transform.TransformDirection(moveDir);
                }
                    
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                anim.SetBool("running", false);
                anim.SetInteger("condition", 0);
                moveDir = new Vector3(0, 0, 0);
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (anim.GetBool("attacking") == true)
                {
                    return;
                }
                else if (anim.GetBool("attacking") == false)
                {
                    anim.SetBool("running", true);
                    anim.SetInteger("condition", 1);
                    moveDir = new Vector3(0, 0, -1);
                    moveDir *= speed *0.7f;
                    moveDir = transform.TransformDirection(moveDir);
                }

            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                anim.SetBool("running", false);
                anim.SetInteger("condition", 0);
                moveDir = new Vector3(0, 0, 0);
            }

            if (Input.GetKey(KeyCode.D))
            {
                if (anim.GetBool("attacking") == true)
                {
                    return;
                }
                else if (anim.GetBool("attacking") == false)
                {
                    anim.SetBool("running", true);
                    anim.SetInteger("condition", 1);
                    moveDir = new Vector3(1, 0, 0);
                    moveDir *= speed * 0.7f;
                    moveDir = transform.TransformDirection(moveDir);
                }

            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                anim.SetBool("running", false);
                anim.SetInteger("condition", 0);
                moveDir = new Vector3(0, 0, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (anim.GetBool("attacking") == true)
                {
                    return;
                }
                else if (anim.GetBool("attacking") == false)
                {
                    anim.SetBool("running", true);
                    anim.SetInteger("condition", 1);
                    moveDir = new Vector3(-1, 0, 0);
                    moveDir *= speed * 0.7f;
                    moveDir = transform.TransformDirection(moveDir);
                }

            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                anim.SetBool("running", false);
                anim.SetInteger("condition", 0);
                moveDir = new Vector3(0, 0, 0);
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                moveDir.y += jumpForce;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift) && ap.currentVal > 0 && !apOut)
            {
                ap.currentVal -= 0.5f;
                dontAddAp = true;
                ap.Initialize();
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                charging = false;
                runningFactor = 1;
                anim.SetInteger("condition", 0);
            }
            
            if (Input.GetKeyUp(KeyCode.W))
            {
                anim.SetBool("running", false);
                anim.SetInteger("condition", 0);
                moveDir = new Vector3(0, 0, 0);
            }
            
        }
        if (!dontAddAp && ap.currentVal < ap.maxVal && !apOut)
        {
            ap.currentVal += 0.25f;
            ap.Initialize();
        }
        if (!dontAddMp && mp.currentVal < mp.maxVal && !mpOut)
        {
            mp.currentVal += 0.25f;
            mp.Initialize();
        }
        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        //transform.eulerAngles = new Vector3(0, rot, 0);
        if(moveDir.y>0)
            moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }

    void GetInput()
    {
        if (controller.isGrounded)
        {
            if (Input.GetMouseButtonDown(0))
            {
                /*
                if(anim.GetBool("running")==true)
                {
                    anim.SetBool("running", false);
                    anim.SetInteger("condition", 0);
                }
                if(anim.GetBool("running") ==false)
                {*/
                Attacking();
                // }
            }

            if (Input.GetMouseButtonDown(1))
            {
                Shoot();
            }
            else
            {
                firstShot = true;
            }
        }
    }


    private void Shoot()
    {
        if (firstShot)
        {
            firstShot = false;
            if (mp.currentVal > mp.maxVal/4 && !mpOut)
            {
                mp.currentVal -= mp.maxVal / 4;
                //dontAddMp = true;
                mp.Initialize();
                GameObject shot = Instantiate(arrow.gameObject);
                
            }
        }

    }

    void Wait(int waitTime)
    {
        StartCoroutine(WaitRoutine(waitTime));

    }
    IEnumerator WaitRoutine(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        apOut = false;
        mpOut = false;
        Debug.Log("ap = false 5 secs passed");
        ap.currentVal++;
        mp.currentVal++;
    }
    void Attacking()
    {
        StartCoroutine(AttackRoutine());
    }
    IEnumerator AttackRoutine() 
    {
        anim.SetBool("attacking",true);
        attacking = true;
        swords[0].setPower(GetComponent<PowerScript>().getMeleePower());
        swords[1].setPower(GetComponent<PowerScript>().getMeleePower());
        swords[0].setEffects(GetComponent<PowerScript>().getMeleeEffects());
        swords[1].setEffects(GetComponent<PowerScript>().getMeleeEffects());
        anim.SetInteger("condition", 2);
        yield return new WaitForSeconds(1);
        anim.SetInteger("condition",0);
        attacking = false;
        anim.SetBool("attacking", false);
    }
    
}
