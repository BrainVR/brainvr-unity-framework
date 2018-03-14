using BrainVR.UnityFramework.Helpers;
using UnityEngine;

public class DemoTimer : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    var timer = new Stopwatch(5f);
        timer.TimeRanOut += TimerOnTimeRanOut;
	    StartCoroutine(timer.Countdown());
	}

    private void TimerOnTimeRanOut(Stopwatch stopwatch)
    {
        Debug.Log("genius");
    }
}
