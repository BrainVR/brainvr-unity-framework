using System.Collections;
using System.Collections.Generic;
using BrainVR.UnityFramework.Objects.Goals;
using BrainVR.UnityFramework.Player;
using UnityEngine;

public class Demo : MonoBehaviour {

    void Start()
    {
        GoalManager.Instance.Hide(0);
    }

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
