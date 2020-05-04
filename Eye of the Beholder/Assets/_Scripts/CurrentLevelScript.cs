using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Text))]
public class CurrentLevelScript : MonoBehaviour {

    public Text text;
    private bool firstRead = true;

    void Update()
    {
        if (firstRead)
        {
            if (MemoryScript.isReady())
            {
                text.text = MemoryScript.getCurrentFileName();
                firstRead = false;
            }
        }
        else
        {
                text.text = MemoryScript.getCurrentFileName();
        }
    }

    public void nextSave()
    {
        MemoryScript.nextFile();
    }

    public void previousSave()
    {
        MemoryScript.previousFile();
    }
    
    public void DeleteCurrentSave()
    {
        MemoryScript.DeleteCurrentFile();
    }
}
