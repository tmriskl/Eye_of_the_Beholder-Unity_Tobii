using UnityEngine;
using System.Collections;

public class DenTileScript : MonoBehaviour
{
    public GameObject voronoiTile;
    public int timesHit=0;
    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.name);
        timesHit++;
        if (col.gameObject.tag == "Player" && timesHit >= 3)
        {
            Instantiate(voronoiTile, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

