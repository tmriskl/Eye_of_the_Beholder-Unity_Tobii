
using UnityEngine;
using Tobii.Gaming;


[RequireComponent(typeof(GazeAware))]
public class BehaviourObject : MonoBehaviour
{
    public AIAlgorithm AI;
    public double[] options;
    protected GazeAware _gazeAwareComponent;
    
    void Start()
    {
        _gazeAwareComponent = GetComponent<GazeAware>();

    }

    /*protected void OnMouseDown()
    {
        if ((null != AI) && (!(AI is AIPlayerStats)))
            AI.updateOptions(options);
    }*/
    /*public virtual void updateOptions(AIPlayerStats AI)
    {
        AI.updateOptions(options);
    }*/
    public void updateOptions()
    {
        AI.updateOptions(options);
    }
    void FixedUpdate()
    {
        if (_gazeAwareComponent.HasGazeFocus)
        {
            //if ((null != AI)/*&&(!(AI is AIPlayerStats))*/)
            updateOptions();
        }
    }


}
