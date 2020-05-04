using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GateScript : MonoBehaviour {

    public int scene = 1;
    public Transform returnPoint = null;
    public LocationSaveScript locationSave = null;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //if (GameObject.Find("Player").GetComponent<PlayerScript>().moveScene) {
            if(returnPoint != null)
                col.gameObject.transform.position = returnPoint.position;
            if(locationSave != null)
                locationSave.saveToFile();
            SceneManager.LoadScene(scene/*SceneManager.GetActiveScene().buildIndex + 1*/);
               
            //}
        }
    }
}
