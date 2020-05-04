
using System;
using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;
using System.IO;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PowerScript))]
[RequireComponent(typeof(MemoryScript))]
[RequireComponent(typeof(AIPlayerStats))]
public class PlayerGazeScript : MonoBehaviour
{

    GazeableScript gazedObject;
    BehaviourObject behaviourObject;
    Light torchLight;
    float distance = 5;
    public Text gazedObjectAction;
    public Text gazedObjectName;
    public TextMesh text;
    private bool firstRead = true;
    public Transform playerPos;
    private bool npc = false;


    void Start()
    {
        torchLight = GetComponent<Light>();
        gazedObject = null;
    }


    void FixedUpdate()
    {

        if (firstRead)
        {
            if (MemoryScript.isReady())
            {
                firstRead = false;
                GetComponent<AIPlayerStats>().ReadFromFile();
                GetComponent<AIPlayerStats>().saveToFile();
                GetComponent<PowerScript>().ReadFromFile();
                GetComponent<PowerScript>().saveToFile();
                saveToFile();
            }
        }
        else
        {
            /**/
            ManageFocusedObject();
        }
    }
    private void ManageFocusedObject()
    {
        GameObject focusedObject = TobiiAPI.GetFocusedObject();
        NPCNavMeshScript gazeableNPCScript = null;
        if (null != focusedObject)
        {
            if (null != gazedObject)
                gazedObject.player = null;
            behaviourObject = null;
            gazedObject = null;
            gazedObject = focusedObject.GetComponent<GazeableScript>();
            if (null != gazedObject)
            {
                gazedObject.player = this;
            }
            gazeableNPCScript = focusedObject.GetComponent<NPCNavMeshScript>();
            if (null != gazeableNPCScript)
            {
                //Debug.Log("NPC: " + gazeableNPCScript.gameObject.name);
                //gazeableNPCScript.playerPos = gameObject.transform;
                playerPos.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
                gazeableNPCScript.playerPos = playerPos;
                gazeableNPCScript.maxDistanceToTarget = distance*2;
                //Debug.Log("gazeableNPCScript.maxDistanceToTarget: " + gazeableNPCScript.maxDistanceToTarget);

            }
            behaviourObject = focusedObject.GetComponent<BehaviourPlayerStats>();
            if (null != behaviourObject)
            {
                ((BehaviourPlayerStats)behaviourObject).updateOptions(GetComponent<AIPlayerStats>());
            }
        }
        /**/

        if (!npc&&((null == gazedObject) || (Vector3.Distance(this.gameObject.transform.position, gazedObject.transform.position) > distance)))
        {
            text.text = "";
            gazedObjectAction.text = "";
            gazedObjectName.text = "";
            if ((gazeableNPCScript == null)&&(null != gazedObject))
                gazedObject.player = null;
            gazedObject = null;
        }
        else if(!npc)
        {
            gazedObjectAction.text = gazedObject.GetGazeableAction();
            gazedObjectName.text = gazedObject.GetGazeableName();
            /*if (gazedObject is GazeableNPCScript)
            {
                gazedObject.gameObject.transform.LookAt(gameObject.transform.position);
                gazedObject.gameObject.GetComponent<NPCAnimationScript>().StandAnimation();
            }*/

            /**/
            //text.text = gazedObject.GetGazeableAction() + " " + gazedObject.GetGazeableName();
            //text.transform.position = gazedObject.transform.position;
            //text.transform.LookAt(gameObject.transform.position);
            //text.transform.Rotate(0, 180, 0);
            /**/
        }
        if (!npc&&Input.GetKeyDown(KeyCode.E))
        {
            use();
            setTexts("", "", "");
            if (null != gazedObject)
                gazedObject.player = null;
            gazedObject = null;
        }
    }

    public void setTexts(string v1, string v2, string v3)
    {

        text.text = v1;
        gazedObjectAction.text = v3;
        gazedObjectName.text = v2;
    }

    public void setNPC(bool v)
    {
        npc = true;
    }

    public void setGazedObject(GazeableScript gazeable)
    {
        //gazedObject = gazeable;
    }
    public void use()
    {
        if (gazedObject != null)
        {
            if (Vector3.Distance(this.gameObject.transform.position, gazedObject.transform.position) < distance)
                gazedObject.Use();
        }
    }


    public void resetGazedObject()
    {
        gazedObject = null;
    }

    public void lightOn()
    {
        torchLight.enabled = true;
    }
    public void saveToFile()
    {
        //Debug.Log(GetComponent<MemoryScript>().isReady()) ;
        string filePath = MemoryScript.GetCurrentLevelFilePath();
        using (StreamWriter sw = File.CreateText(filePath))
        {
            sw.WriteLine(SceneManager.GetActiveScene().buildIndex);
        }
    }
}