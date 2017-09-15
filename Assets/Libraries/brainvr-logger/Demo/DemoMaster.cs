using BrainVR.UnityLogger;
using UnityEngine;

public class DemoMaster : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		MasterLog.Instance.Instantiate();
        MasterLog.Instance.StartLogging();
	}
}
