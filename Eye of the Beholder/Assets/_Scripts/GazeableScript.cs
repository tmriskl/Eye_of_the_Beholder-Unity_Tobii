
using UnityEngine;
using Tobii.Gaming;

[RequireComponent(typeof(GazeAware))]
//[RequireComponent(typeof(TextMesh))]
public abstract class GazeableScript : Finals {

    protected string gazeName = "";
    protected string action = "";
    protected GazeAware _gazeAwareComponent;
    public PlayerGazeScript player;
    protected bool gazed = false;
   // protected TextMesh text;

    public abstract void Use();

    protected void OnMouseDown()
    {
        Use();
    }
    public string GetGazeableName()
    {
        return gazeName;
    }

    public string GetGazeableAction()
    {
        return action;
    }

    protected void checkGaze()
    {
       /* if (_gazeAwareComponent.HasGazeFocus)
        {
            gazed = true;
            player.setGazedObject(this);
        }
        else if (gazed)
        {
            gazed = false;
            player.resetGazedObject();
        }*/
    }
}
