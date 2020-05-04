using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAlgorithm : MonoBehaviour
{
    public double[] options = null;
    protected bool active = true;
    protected int max = 0;
    protected double sum = 0;

    public virtual void updateOptions(double[] add)
    {
        for (int i = 0; (i < add.Length) && (i < options.Length); i++)
            options[i] += add[i];
    }

    //public void setOptions(double[] new_options)
    //{
    //    options = new_options;
    //}

    public virtual int conclusion() {
        for (int i = 0; i < options.Length; i++)
        {
            sum += options[i];
            if (options[i] > options[max])
                max = i;
        }

        return max;
    }
}
