using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DoorScript : MonoBehaviour
{
    protected bool open = false;
    protected bool first = true;
    protected AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    protected void PStart()
    {
        Start();
    }

    public void Open()
    {
        open = true;
    }
    public bool isOpen()
    {
        return open;
    }
    protected void PUpdate()
    {
        Update();
    }
    void Update()
    {
        if (first&&open)
        {
            first = false;
            audio.Play();
        }
        else if(!open)
        {
            audio.Stop();
        }
    }
}
