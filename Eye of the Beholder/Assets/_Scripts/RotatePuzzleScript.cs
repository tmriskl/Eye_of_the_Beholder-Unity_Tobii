using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePuzzleScript : PuzzleScript
{
    
    public RotatePuzzlePieceScript[] sprites;
    public int angle = 30;

    void Start () {
        for (int i = 0; i < sprites.Length; i++)
        {
            int a = ((int)(Random.Range(angle + 0.1f, 360.0f))/ angle)*angle;
            sprites[i].setNum(sprites.Length - i);
            sprites[i].transform.Rotate(0, 0, a);
            sprites[i].gameObject.GetComponent<Renderer>().sortingOrder = i + 1;
        }
    }

    private void Update()
    {
        done = true;
        for (int i = 1; i < sprites.Length; i++)
        {
            done = done && ((sprites[i].transform.rotation.z) <= 0.001) && ((sprites[i].transform.rotation.z) >= -0.001);
        }
        if (done&&!door.isOpen())
        {
            for (int i = 1; i < sprites.Length; i++)
            {
                sprites[i].stop();
            }
            door.Open();
        }
    }
}
