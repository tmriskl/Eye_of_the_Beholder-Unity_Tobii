using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePuzzleScript : PuzzleScript
{

    public MovePuzzlePieceScript[] sprites;
    public bool[] empty;
    public int size;
    public bool move = false;
    //public bool R = true;
    public int x = 0, y = 0;
    int times = 20;

	// Use this for initialization
	void Start () {
        int i, j; 
		for(i = 0;i< size; i++)
        {
            for (j = 0; j < size; j++)
            {
                sprites[i * size + j].PStart();
                sprites[i * size + j].setXY(i, j);
                sprites[i * size + j].setOriginalXY(i, j);
                empty[i * size + j] = false;
            }
        }
        //i = 0;//(int)(Random.Range(0.0f, size + 0.0f));
        //j = 0;//(int)(Random.Range(0.0f, size + 0.0f));
        sprites[0].setVisibility(false);
        empty[0] = true;
        for (int k = 0; k < times; k++)
        {
            i = (int)(Random.Range(0.0f, size + 0.0f));
            j = (int)(Random.Range(0.0f, size + 0.0f));
            sprites[i * size + j].Move();
        }
        done = false;

    }

    public int[] getEmptySpot(int x,int y)
    { 
        int i = 0, j = 0;
        int[] a = new int[2];
        for (i = 0; i < size; i++)
        {
            for (j = 0; j < size; j++)
            {
                if (empty[i * size + j])
                {
                    a[0] = i;
                    a[1] = j;
                }
            }
        }
        empty[a[0] * size + a[1]] = false;
        empty[y * size + x] = true;
        return a;
    }

    public bool canMove()
    {
        int i, j;
        for (i = 0; i < size; i++)
        {
            for (j = 0; j < size; j++)
            {
                if (sprites[i * size + j].isMoving())
                {
                    return false;
                }
            }
        }
        return true;
    }
    public bool Done()
    {
        int i, j;
        for (i = 0; i < size; i++)
        {
            for (j = 0; j < size; j++)
            {
                if (!sprites[i * size + j].inPlace())
                    return false;
            }
        }
        return true;
    }
	// Update is called once per frame
	void Update () {
        if (move)
        {
            move = false;
            if (canMove()) { 
                sprites[y% size * size + x% size].Use();
            }
        }

        if (Done() && !done)
        {
            int i, j;
            for (i = 0; i < size; i++)
            {
                for (j = 0; j < size; j++)
                {
                    sprites[i * size + j].setVisibility(true);
                }
            }
            done = true;
            door.Open();
        }
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    if (R)
        //    {
        //        R = false;
        //        int i = (int)(Random.Range(0.0f, size + 0.0f));
        //        int j = (int)(Random.Range(0.0f, size + 0.0f));
        //        sprites[i * size + j].Move();
        //    }
        //}
        //else
        //{
        //    R = true;
        //}
	}
}
