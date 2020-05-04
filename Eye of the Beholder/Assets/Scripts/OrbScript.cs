using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OrbScript : MonoBehaviour
{

    public Text insideText;
    public Text outsideText;
    void Start()
    {
        insideText = GameObject.Find("TextShaddow").GetComponent<Text>();
        outsideText = GameObject.Find("TextO").GetComponent<Text>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            insideText.text = "You Won!";
            outsideText.text = "You Won!";
            //insideText.enabled = true;
            //outsideText.enabled = true;
            Destroy(gameObject);
        }
    }
}
