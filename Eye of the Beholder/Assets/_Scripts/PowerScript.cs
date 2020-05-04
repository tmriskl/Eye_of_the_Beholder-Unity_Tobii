using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PowerScript : MonoBehaviour {

    public static readonly float defaultMelee = 1;
    public static readonly float defaultRange = 1;
    public static readonly float[] defaultEffectMelee = { 0, 0, 0 };
    public static readonly float[] defaultEffectRange = { 0, 0, 0 };
    public static readonly int effectBurn = 0, effectPoision = 1, effectFreeze = 2, NumEffects = 3;
    public static readonly float effectBurnDamage = 1, effectPoisionDamage = 0.1f;
    string fileName = "power.txt";

    public float melee;
    public float range;
    public float[] meleeEffects;
    public float[] rangeEffects;


    public void addPower(PowerScript power)
    {
        addToMelee(power.melee);
        addToRange(power.range);
        for (int i = 0; (i < power.rangeEffects.Length) && (i < rangeEffects.Length); i++)
                addToMeleeEffect(power.rangeEffects[i], i);
        for (int i = 0; (i < power.meleeEffects.Length) && (i < meleeEffects.Length); i++)
                addToRangeEffect(power.meleeEffects[i], i);
        saveToFile();
    }

    public float getMeleePower()
    {
        return melee;
    }

    public float getRangePower()
    {
        return range;
    }

    public float getMeleeEffect(int effectNum)
    {
        if (effectNum < meleeEffects.Length)
            return meleeEffects[effectNum];
        return 0;
    }

    public float getRangeEffect(int effectNum)
    {
        if (effectNum < rangeEffects.Length)
            return meleeEffects[effectNum];
        return 0;
    }

    public float[] getMeleeEffects()
    {
        return meleeEffects;
    }

    public float[] getRangeEffects()
    {
        return meleeEffects;
    }

    public void addToMelee(float add)
    {
        melee += add;
        saveToFile();
    }
    public void addToRange(float add)
    {
        range += add;
        saveToFile();
    }

    public void addToMeleeEffect(float add, int effectNum)
    {
        if (effectNum >= meleeEffects.Length)
        {
            float[] newEffects = new float[effectNum + 1];
            for (int i = 0; i < newEffects.Length; i++)
            {
                if (meleeEffects.Length > i)
                    newEffects[i] = meleeEffects[i];
                else
                    newEffects[i] = 0;
            }
            meleeEffects = newEffects;
        }
        meleeEffects[effectNum] += add;
        saveToFile();
    }
    public void addToRangeEffect(float add, int effectNum)
    {
        if (effectNum >= rangeEffects.Length)
        {
            float[] newEffects = new float[effectNum + 1];
            for (int i = 0; i < newEffects.Length; i++)
            {
                if (rangeEffects.Length > i)
                    newEffects[i] = rangeEffects[i];
                else
                    newEffects[i] = 0;
            }
            rangeEffects = newEffects;
        }
        rangeEffects[effectNum] += add;
        saveToFile();
    }

    public void saveToFile()
    {
        string filePath = MemoryScript.GetCurrentPath();
        using (StreamWriter sw = File.CreateText(filePath + fileName))
        {
            sw.WriteLine(melee);
            sw.WriteLine(range);
            sw.WriteLine(meleeEffects.Length);
            for (int i = 0; i < meleeEffects.Length; i++)
                sw.WriteLine(meleeEffects[i]);
            sw.WriteLine(rangeEffects.Length);
            for (int i = 0; i < rangeEffects.Length; i++)
                sw.WriteLine(rangeEffects[i]);
        }
    }

    public void ReadFromFile()
    {
        string filePath = MemoryScript.GetCurrentPath();
        if (File.Exists(filePath + fileName))
        {
            using (StreamReader sr = File.OpenText(filePath + fileName))
            {
                string s;
                s = sr.ReadLine();
                melee = float.Parse(s);
                s = sr.ReadLine();
                range = float.Parse(s);
                s = sr.ReadLine();
                int length = (int)float.Parse(s);
                meleeEffects = new float[length];
                for (int i = 0; i < length; i++)
                {
                    s = sr.ReadLine();
                    meleeEffects[i] = float.Parse(s);
                }
                s = sr.ReadLine();
                length = (int)float.Parse(s);
                rangeEffects = new float[length];
                for (int i = 0; i < length; i++)
                {
                    s = sr.ReadLine();
                    rangeEffects[i] = float.Parse(s);
                }
            }
        }
        else
        {
            melee = defaultMelee;
            range = defaultRange;
            meleeEffects = new float[defaultEffectMelee.Length];
            for (int i = 0; i < defaultEffectMelee.Length; i++)
            {
                meleeEffects[i] = defaultEffectMelee[i];
            }
            rangeEffects = new float[defaultEffectRange.Length];
            for (int i = 0; i < defaultEffectRange.Length; i++)
            {
                rangeEffects[i] = defaultEffectRange[i];
            }
        }
    }

}
