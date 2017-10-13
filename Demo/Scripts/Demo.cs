using System.Collections;
using System.Collections.Generic;
using BrainVR.UnityFramework.Player;
using UnityEngine;

public class Demo : MonoBehaviour {

	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.C))
	    {
	        var controller = (RigidBodyPlayerController) PlayerController.Instance;
	        controller.Stop();
	    }
	}
}
