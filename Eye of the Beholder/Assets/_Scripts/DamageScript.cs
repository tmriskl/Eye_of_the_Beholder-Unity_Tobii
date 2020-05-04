using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour {

    float power = 0;
    float[] effects = { 0 };
    
    public void setPower(float newPower)
    {
        power = newPower;
    }

    public void setEffects(float[] newEffect)
    {
        effects = new float[newEffect.Length];
        for(int i = 0; i<newEffect.Length; i++)
        {
            effects[i] = newEffect[i];
        }
    }

    public float getPower()
    {
        return power;
    }


    public float[] getEffects()
    {
        return effects;
    }

    public float getEffect(int effectNum)
    {
        if (effectNum < effects.Length)
            return effects[effectNum];
        return 0;
    }
}
