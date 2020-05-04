using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewSaveTextScript : MonoBehaviour {

    public Text text;
    public InputField fileName;

    public void add()
    {
        if(fileName.text.Equals(null)|| fileName.text.Equals(""))
        {
            text.text = "please write file name";
            return;
        }

        if (MemoryScript.addNewSaveFile(fileName.text))
        {
            text.text = fileName.text + " added";
        }
        else
        {
            text.text = fileName.text + " alredy exist";
        }
    }


}
