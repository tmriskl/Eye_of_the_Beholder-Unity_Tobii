using UnityEngine;
using System.Collections;

public class GateTriggerScript : MonoBehaviour {

    public GameObject gateDoor;
    public GameObject gateOpeningSphere;
    public float speed;
    private Vector3 movePos;
    private bool moveDoor=false;
    
    void Update()
    {
        if (moveDoor)
        {
            movePos = new Vector3(gateDoor.transform.position.x, -7, gateDoor.transform.position.z);
            gateDoor.transform.position = Vector3.Lerp(gateDoor.transform.position, movePos, Time.deltaTime * speed);
            if (movePos.y < -300)
                moveDoor = false;
        }
            
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            if (GameObject.Find("Player").GetComponent<PlayerScript>().keysCollected == 3)
            {
                gateOpeningSphere.SetActive(true);
                moveDoor = true;
                GameObject.Find("Player").GetComponent<PlayerScript>().moveScene = true;
            }
        }
    }
}
