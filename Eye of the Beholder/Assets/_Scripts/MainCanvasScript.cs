using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvasScript : MonoBehaviour {

    public GameObject[] panel;
    public Text text1;
    public Text text2;
    private bool first = true;
    private bool first2 = false;
    private bool showControls = true;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (first)
            {
                first = false;
                for(int i = 0; i< panel.Length; i++)
                    panel[i].SetActive(!panel[i].activeSelf);
            }
        }
        else
        {
            first = true;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (first2)
            {
                first2 = false;
                if (showControls)
                {
                    text1.text = "Walk\n" +
                              "Run\n" +
                              "Sword Hit\n" +
                              "Arrow\n" +
                              "Jump\n" +
                              "Pick Up\n" +
                              "Show Debugger\n" +
                              "Show Mouse";
                    text2.text = "W, A, S, D\n" +
                              "Left Shift\n" +
                              "Left Mouse Button\n" +
                              "Right Mouse Button\n" +
                              "Space\n" +
                              "E\n" +
                              "Z\n" +
                              "Esc";
                }
                else
                {
                    text1.text = "";
                    text2.text = "";
                }
                    showControls = !showControls;
            }
        }
        else
        {
            first2 = true;
        }
    }
}
