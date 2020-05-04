using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class AIPlayerStats : AIAlgorithm {

    public string[] stats = null;
    public Text[] data;
    private bool first = true;
    public bool ready = false;

    string fileName = "stats.txt";

    public void updateStat(double add, int stat)
    {
        if (options == null)
        {
            options = new double[stat + 1];
            for (int i = 0; i <= stat; i++)
            {
                options[i] = 0;
            }
        }

        if (stat < options.Length)
            options[stat] += add;
        else
        {
            double[] newOptions = new double[stat + 1];
            for (int i = 0; i <= stat; i++)
            {

                if (i < options.Length)
                    newOptions[i] = options[i];
                else
                {
                    newOptions[i] = 0;
                }
            }
            newOptions[stat] += add;
            options = newOptions;
        }
        saveToFile();
    }

    public int getStatLocationByName(string name)
    {
        for (int i = 0; i < stats.Length; i++)
        {
            if (stats[i].ToLower().Trim().Equals(name.ToLower().Trim()))
            {
                return i;
            }
        }
        return -1;

    }
    public double readStat(int stat)
    {
        if(stat < 0)
        {
            return 0;
        }
        if (options == null)
        {
            options = new double[stat + 1];
            for (int i = 0; i <= stat; i++)
            {
                options[i] = 0;
            }
            return options[stat];
        }

        if (stat >= options.Length)
        {
            double[] newOptions = new double[stat + 1];
            for (int i = 0; i <= stat; i++)
            {

                if (i < options.Length)
                    newOptions[i] = options[i];
                else
                {
                    newOptions[i] = 0;
                }
            }
            options = newOptions;
        }

        return options[stat];
    }
    public double readStatPercent(int stat)
    {
        if (options == null)
        {
            options = new double[stat + 1];
            for (int i = 0; i <= stat; i++)
            {
                options[i] = 0;
            }
            return options[stat];
        }

        if (stat >= options.Length)
        {
            double[] newOptions = new double[stat + 1];
            for (int i = 0; i <= stat; i++)
            {

                if (i < options.Length)
                    newOptions[i] = options[i];
                else
                {
                    newOptions[i] = 0;
                }
            }
            options = newOptions;
        }
        double sum = 0;
        for (int i = 0; i < options.Length; i++)
        {
            sum += options[i];
        }
        return options[stat]/sum;
    }

    protected void toTexts()
    {
        double sum = 0, precision = 100;
        for (int i = 0; i < options.Length; i++)
        {
            sum += options[i];
        }
        data[data.Length - 1].text = "  Total  \t: " + sum;
        if (sum == 0)
            sum = 1;
        for (int i = 0; (i < data.Length - 1)&&(i< stats.Length); i++)
        {
            //if(fileName != "stats.txt")
            //{
            //    Debug.Log(i);
            //    Debug.Log(", data: " + data[i]);
            //    Debug.Log(", stats: " + stats[i]);
            //    Debug.Log(",  options: " + options[i]);
            //}
               
            data[i].text = "  " + stats[i] + "\t: " + ((int)(options[i] / sum * 100 * precision)) / precision + "% " + options[i];
        }
    }

    void Update () {
        if (first)
        {
            ReadFromFile();
            first = false;
            ready = true;
        }
        //if(!GetComponent<PlayerGazeScript>().firstRead)
           toTexts();
    }

    public void saveToFile()
    {
        
        string filePath = MemoryScript.GetCurrentPath();
        //Debug.Log(filePath + gameObject.name + fileName);
        using (StreamWriter sw = File.CreateText(filePath + gameObject.name + fileName))
        {
            sw.WriteLine(stats.Length);
            for (int i = 0; i < stats.Length; i++)
                sw.WriteLine(stats[i]);
            sw.WriteLine(options.Length);
            for (int i = 0; i < options.Length; i++)
                sw.WriteLine(options[i]);
        }
    }

    public void ReadFromFile()
    {
        //Debug.Log(GetComponent<MemoryScript>().isReady());
        string filePath = MemoryScript.GetCurrentPath();
        //Debug.Log(filePath + gameObject.name + fileName);
        if (File.Exists(filePath + gameObject.name + fileName))
        {
            using (StreamReader sr = File.OpenText(filePath + gameObject.name + fileName))
            {
                string s;
                s = sr.ReadLine();
                int length = (int)double.Parse(s);
                stats = new string[length];
                for (int i = 0; i < length; i++)
                {
                    s = sr.ReadLine();
                    stats[i] = s;
                }
                s = sr.ReadLine();
                length = (int)double.Parse(s);
                options = new double[length];
                for (int i = 0; i < length; i++)
                {
                    s = sr.ReadLine();
                    options[i] = double.Parse(s);
                }
            }
        }
        else
        {
            options = new double[stats.Length];
            saveToFile();
        }
    }
}
