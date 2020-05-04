using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;

public class MovePuzzlePieceScript : GazeableScript
{
    public int x, y;
    public int new_x, new_y;
    public int o_x,o_y;
    bool moving = false;
    public float move = 2.25f;
    public float moveDivider = 100.0f;
    public float moveX = 0;
    public float moveY = 0;
    public float moveZ = 0;
    public Vector3 errorPosition;
    public Vector3 errorScale;
    SpriteRenderer renderer = null;
    public MovePuzzleScript movePuzzleScript;
    AudioSource sound;

    public override void Use()
    {
        //if (renderer == null)
        //    renderer = GetComponent<SpriteRenderer>();
        //Debug.Log(renderer.enabled && movePuzzleScript.canMove());
        if (/*renderer.enabled && */movePuzzleScript.canMove())
        {
           int[] newLocation = movePuzzleScript.getEmptySpot(x, y);
           new_x = newLocation[1];
           new_y = newLocation[0];
           moving = true;
           // gameObject.transform.Translate(0, 0, 0.01f);
        }
    }

    public void Move()
    {
        if(renderer==null)
            renderer = GetComponent<SpriteRenderer>();
        if (renderer.enabled)
        {
            //errorPosition = movePuzzleScript.transform.position;
            //errorScale = movePuzzleScript.transform.localScale;
            int[] newLocation = movePuzzleScript.getEmptySpot(x, y);
            x = newLocation[1];
            y = newLocation[0];
            gameObject.transform.position = new Vector3((x * move ) * errorScale.x+ errorPosition.x, (y * move)  * errorScale.y + errorPosition.y, (0)  * errorScale.z + errorPosition.z);
        }
    }

    public void PStart()
    {
        Start();
    }

    public bool inPlace()
    {
        return (x == o_x) && (y == o_y);
    }

    public void setXY(int i, int j)
    {
        x = j;
        y = i;
    }

    public void setOriginalXY(int i, int j)
    {
        o_x = j;
        o_y = i;
    }

    public void setVisibility(bool visible)
    {
        renderer = GetComponent<SpriteRenderer>();
        GetComponent<Collider>().enabled = visible;
        renderer.enabled = visible;
    }
    // Use this for initialization
    void Start () {
        errorPosition = movePuzzleScript.transform.position;
        errorScale = movePuzzleScript.transform.localScale;
        gazeName = MovePuzzlePiece_Name;
        action = Action + Next_Line + MovePuzzlePiece_Action;
        _gazeAwareComponent = GetComponent<GazeAware>();
        renderer = GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
    }
	public bool isMoving()
    {
        return moving;
    } 

	// Update is called once per frame
	void Update ()
    {
        //moveX = 0;
        //moveY = 0;
        checkGaze();
        moveZ = 0;
        if (moving)
        {
            if (!sound.isPlaying)
                sound.Play();
            if (!closeEnough(gameObject.transform.position.x, (new_x * move) * errorScale.x + errorPosition.x))
            {
                if (gameObject.transform.position.x > (new_x * move) * errorScale.x + errorPosition.x)
                {
                    moveX -= move / moveDivider;
                    moveZ = 0.2f;
                }
                else if (gameObject.transform.position.x < (new_x * move) * errorScale.x + errorPosition.x)
                {
                    moveX += move / moveDivider;
                    moveZ = 0.2f;
                }
            }
            else
            {
                x = new_x;
                moveX = 0;
            }
            if (!closeEnough(gameObject.transform.position.y, (new_y * move)  * errorScale.y + errorPosition.y))
            {
                if (gameObject.transform.position.y > (new_y * move) * errorScale.y + errorPosition.y)
                {
                    moveY -= move / moveDivider;
                    moveZ = 0.2f;
                }
                else if (gameObject.transform.position.y < (new_y * move) * errorScale.y + errorPosition.y)
                {
                    moveY += move / moveDivider;
                    moveZ = 0.2f;
                }
            }
            else
            {
                y = new_y;
                moveY = 0;
            }
            if (moveZ == 0)
            {
                moving = false;
                renderer.sortingOrder = 0;
                sound.Stop();
            }
            else
            {
                renderer.sortingOrder = 1;
            }
        }
        gameObject.transform.position = new Vector3((x * move + moveX)  * errorScale.x + errorPosition.x, (y * move + moveY)  * errorScale.y + errorPosition.y, errorPosition.z + (moveZ)  * errorScale.z);

    }

    bool closeEnough(float f1, float f2)
    {
        float f = f1 - f2;
        if (f < 0)
            f = -f;
        return f < 0.1;
    }
}
