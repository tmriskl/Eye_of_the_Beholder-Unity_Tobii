
using UnityEngine;
using Tobii.Gaming;

public class GazeableDoor : GazeableScript
{
    public GazeableKey key;
    public bool open = false;
    public int rotate = 100;
    public float count = 0;
    public float angle = 110;
    public GameObject toActivate;
    float rotation = 0.01f;
    AudioSource sound;


    public override void Use()
    {
        if (key.hasPicked)
        {
            open = true;
            action = Action + Next_Line + Door_Action2;
            if (null != toActivate)
                toActivate.SetActive(true);
            _gazeAwareComponent.enabled = false;
           // print("true");
        }
    }

    // Use this for initialization
    void Start () {

        gazeName = Door_Name;
        action = Next_Line + Door_Action1 + Door_Action2;
        _gazeAwareComponent = GetComponent<GazeAware>();
        sound = GetComponent<AudioSource>();
        if (null != toActivate)
            toActivate.SetActive(false);

    }
	
	// Update is called once per frame
	void Update ()
    {
        checkGaze();
        if ((open) && (rotate != 0))
        {
            if ((count <= angle) && (count >= -angle))
            {
                if ((null != sound)&&(!sound.isPlaying))
                    sound.Play();
                count += rotation * rotate;
                gameObject.transform.Rotate(0, rotation * rotate, 0);
            }
            else
            {
                if (null != sound)
                   sound.Stop();
                count = 0;
                rotate = 0;
                /*if ((transform.rotation.z <= 0.01) && (transform.rotation.z >= -0.01))
                {
                    collider.enabled = false;
                }*/
            }
        }
        if (key.hasPicked)
        {
            action = Action + Next_Line + Door_Action2;
        }
        /*if (open&&(rotate>0))
        {
            rotate--;
            //Door.transform.Rotate(new Vector3(0,1,0));
            gameObject.transform.Rotate(0, 1, 0);
        }
        if(rotate == 0)
        {
          //  Destroy(GetComponent<MeshRenderer>());
        }*/
    }
}
