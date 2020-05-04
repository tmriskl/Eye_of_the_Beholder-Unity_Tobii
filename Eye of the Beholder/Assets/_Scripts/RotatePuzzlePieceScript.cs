using System;
using Tobii.Gaming;
using UnityEngine;

public class RotatePuzzlePieceScript : GazeableScript {

    public int rotate = 0;
    private int num = 0;
    bool rotateable = true;
    public float count = 0;
    float rotation = 0.1f;
    public Quaternion error;
    public RotatePuzzleScript rotatePuzzleScript;
    AudioSource sound;
    Collider collider;

    public override void Use()
    {
        if (rotate == 0)
            rotate = 1;
        else
            rotate = -rotate;
    }

    public void setNum(int number)
    {
        num = number;
    }
    // Use this for initialization
    void Start()
    {
        error = rotatePuzzleScript.transform.rotation;
        gazeName = RoundPuzzlePiece_Name + " " + num;
        action = Action + Next_Line + RoundPuzzlePiece_Action1 + " " + num + Next_Line + Action + " " +Again + Next_Line + RoundPuzzlePiece_Action2;
        _gazeAwareComponent = GetComponent<GazeAware>();
        sound = GetComponent<AudioSource>();
        collider = GetComponent<Collider>();
    }

    void Update()
    {
        checkGaze();
        if ((rotateable)&&(rotate!=0))
        {
            if((count <= rotatePuzzleScript.angle)&&(count >= -rotatePuzzleScript.angle))
            {
                if(!sound.isPlaying)
                    sound.Play();
                count += rotation * rotate;
                gameObject.transform.Rotate(0, 0, rotation * rotate);
                //gameObject.transform.rotation = new Quaternion(error.x, error.y, error.z + count * rotate * rotatePuzzleScript.angle, error.w);
            }
            else
            {
                sound.Stop();
                count = 0;
                rotate = 0;
                /*if ((transform.rotation.z <= 0.01) && (transform.rotation.z >= -0.01))
                {
                    collider.enabled = false;
                }*/
            }
        }
    }



    public void stop()
    {
        _gazeAwareComponent.enabled = false;
        rotateable = false;
        sound.Stop();

    }
}
