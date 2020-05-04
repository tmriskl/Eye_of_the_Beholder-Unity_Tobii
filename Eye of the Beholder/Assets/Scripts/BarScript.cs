using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BarScript : MonoBehaviour {
    [SerializeField]
    private float fillAmount;
    [SerializeField]
    private Image content;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (fillAmount!=content.fillAmount)
            content.fillAmount = fillAmount;
    }

    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
            fillAmount = Map(value, 0, MaxValue, 0, 1);
            if (value < 0.5 * MaxValue)
            {
                if (value < 0.25 * MaxValue)
                    content.color = Color.red;
                else
                    content.color = Color.yellow;
            }
            else content.color = Color.green;
        }
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
