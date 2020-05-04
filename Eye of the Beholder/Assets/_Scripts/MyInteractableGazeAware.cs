//-----------------------------------------------------------------------
// Copyright 2016 Tobii AB (publ). All rights reserved.
//-----------------------------------------------------------------------

using System.Collections;
using Tobii.Gaming;
using UnityEngine;

public class MyInteractableGazeAware : MyGazeAware
{
	public string Label;
	private bool _running;
    private int c = 0;

	public virtual void Interact()
	{
		//transform.Rotate(transform.up, 10.0f);
		if (!_running)
			StartCoroutine(Pickup());
	}

	private IEnumerator Pickup()
	{
		/*var oldScale = transform.localScale;*/
		_running = true;
        /*while (transform.localScale.magnitude > Mathf.Epsilon)
		{
			transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * 10f);
			yield return null;
		}
*/
        c++;
        print(""+c);
		yield return new WaitForSeconds(2);
		/*transform.localScale = oldScale;*/
		_running = false;
	}
}