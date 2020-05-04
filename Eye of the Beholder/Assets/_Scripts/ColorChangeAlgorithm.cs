using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeAlgorithm : MonoBehaviour {

    public double[] options;
    public MyChangeColor obj1;
    public MyChangeColor obj2;
    bool first = true;
    // Use this for initialization
    void Start () {
        for (int i = 0; i < options.Length; i++)
            options[i] = 0;
    }
	
    public void updateOptions (double[] add)
    {
        for (int i = 0; (i < add.Length)&&(i < options.Length); i++)
            options[i] += add[i];
        if (add.Length == 0)
        {
            print("0");
        }
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P)&&first){
            first = false;
            int max = 0;
            for (int i = 0; i < options.Length; i++)
                if(options[i] > options[max])
                    max = i;
            conclusion(max);
        }
	}

    private void conclusion(int option)
    {
        if(option == 0)
        {
            obj2._deselectionColor = obj1._deselectionColor;
        }
        else
        {
            obj1._deselectionColor = obj2._deselectionColor;
        }
        print("\n");
        for (int i = 0; i < options.Length; i++)
            print(i + ": " + options[i] + "\n");
        print("\n");
    }
}
