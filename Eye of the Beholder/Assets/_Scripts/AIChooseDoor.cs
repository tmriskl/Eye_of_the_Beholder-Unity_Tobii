using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PuzzleScript))]
public class AIChooseDoor : AIAlgorithm {

    public DoorScript[] doors;
    private PuzzleScript puzzle;

    public override void updateOptions(double[] add)
    {
        base.updateOptions(add);
        conclusion();
    }
    public override int conclusion()
    {
        base.conclusion();
        puzzle.setDoor(doors[max]);
        return 0;
    }

    // Use this for initialization
    void Start ()
    {
        puzzle = GetComponent<PuzzleScript>();
        options = new double[doors.Length];
        for(int i = 0; i < options.Length; i++)
        {
            options[i] = 0;
        }
        conclusion();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
