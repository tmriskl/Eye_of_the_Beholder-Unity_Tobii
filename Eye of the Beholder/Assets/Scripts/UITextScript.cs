using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITextScript : MonoBehaviour {

    public float time = 7; //Seconds to read the text
    private Text instruction;
    public Text insideText;

    IEnumerator Start()
    {
        instruction = GetComponent<Text>();
        yield return new WaitForSeconds(time);
        instruction.text = "The Knight needs to kill the 3 orc guardians\nand take the keys that opens the Cave of Drapery Falls";
        insideText.text = "The Knight needs to kill the 3 orc guardians\nand take the keys that opens the Cave of Drapery Falls";
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}

