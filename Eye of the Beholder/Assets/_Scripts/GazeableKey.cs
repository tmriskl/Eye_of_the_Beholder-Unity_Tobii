
using UnityEngine;
using Tobii.Gaming;

public class GazeableKey : GazeableScript
{
    public bool hasPicked = false;
    public Renderer[] keys = { };
    public Collider[] colliders = { };

    public override void Use()
    {
        hasPicked = true;
        for (int i = 0; i < keys.Length; i++)
            keys[i].enabled = false;
        for (int i = 0; i < colliders.Length; i++)
            colliders[i].enabled = false;
        GetComponent<GazeAware>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }

    // Use this for initialization
    void Start () {

        gazeName = Key_Name;
        action = Action + Next_Line + Key_Action;
        hasPicked = false;
        for (int i = 0; i < keys.Length; i++)
            keys[i].enabled = true;
        for (int i = 0; i < colliders.Length; i++)
            colliders[i].enabled = true;
        _gazeAwareComponent = GetComponent<GazeAware>();
    }

    /*void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Contains("Player"))
        {
            Use();
        }
    }*/

    void Update()
    {
        checkGaze();
    }

}
