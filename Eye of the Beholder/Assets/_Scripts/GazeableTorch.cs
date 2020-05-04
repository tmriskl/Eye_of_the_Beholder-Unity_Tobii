
using UnityEngine;
using Tobii.Gaming;

public class GazeableTorch : GazeableScript
{

    void Start()
    {
        gazeName = Torch_Name;
        action = Action + Next_Line + Torch_Action;
        _gazeAwareComponent = GetComponent<GazeAware>();
    }

    void Update()
    {
        checkGaze();
    }

    override public void Use()
    {
        player.lightOn();
        player.resetGazedObject();
        Destroy(this.gameObject);
    }
}


