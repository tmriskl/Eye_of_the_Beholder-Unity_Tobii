using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSaveScript : MonoBehaviour {
    private bool first = true;
    private static int loseLevelNum = 5;
	// Update is called once per frame
	void Update () {
        if (first)
        {
            MemoryScript.DeleteCurrentFile();
            first = false;
        }
	}
    public static int getLoseLevelNum()
    {
        return loseLevelNum;
    }
}
