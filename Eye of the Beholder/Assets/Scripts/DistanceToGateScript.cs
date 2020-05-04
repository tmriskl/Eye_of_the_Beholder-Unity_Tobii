using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DistanceToGateScript : MonoBehaviour
{
    public GameObject player;
    public GameObject gate;
    private Vector3 playerPos;
    private Text text;
    // Use this for initialization
    void Start()
    {
        playerPos = player.transform.position;
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = new Vector3(player.transform.position.x,0, player.transform.position.z);
        text.text = ((int)((playerPos - gate.transform.position).sqrMagnitude)/100).ToString();
       
    }
}
