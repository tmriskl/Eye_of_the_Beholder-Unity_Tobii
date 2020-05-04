using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIClass : AIAlgorithm {

    public int numOfOptions = 3;
    public SpriteRenderer[] sprites;
    public TextMesh[] texts;
    public GazeableDoor door;
    public Text[] data;

    void Start()
    {
        numOfOptions = texts.Length;
       // numOfOptions = sprites.Length / 2;
        for (int i = 0; i < options.Length; i++)
            options[i] = 0;
    }

    private void Update()
    {
       // for (int i = 0; i < options.Length; i++)
           // print("Potions "+options[0]);
        if ((door.rotate <= 0)&& active)
        {
            active = false;
            conclusion();
        }
        toTexts();
    }

    protected void toTexts()
    {
        double sum = 0, precision = 100;
        string[] str = { "Potion Master:",
                         "Warrior:      ",
                         "Wizard:       " } ;
        for (int i = 0; i < options.Length; i++)
        {
            sum += options[i];
        }
        if (sum == 0)
            sum = 1;
        for (int i = 0; i < data.Length-1; i++)
        {
            data[i].text = str[i] + ((int)(options[i] / sum * 100 * precision)) / precision + "% " + options[i];
        }
        data[data.Length - 1].text = "Total: " + sum;
    }
    public override int conclusion()
    {
        //int max = 0;
        //double sum = 0, precision = 100;
        //for (int i = 0; i < options.Length; i++)
        //{
        //    sum += options[i];
        //    if (options[i] > options[max])
        //        max = i;
        //}
        base.conclusion();
        if(options[max]>0)
            sprites[max+3].enabled = true;
        if (sum == 0)
            sum = 1;
        double precision = 100;
        for (int i = 0; i < numOfOptions; i++)
        {
            sprites[i].enabled = true;
            texts[i].text = "" + ((int)(options[i]/sum*100*precision))/ precision + "%";
        }
        return max;
    }
    
}
