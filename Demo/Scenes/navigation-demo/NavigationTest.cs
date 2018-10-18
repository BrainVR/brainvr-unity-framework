using BrainVR.UnityFramework.Navigation;
using UnityEngine;

public class NavigationTest : MonoBehaviour
{
    public GameObject[] GameObjects;

    private NavigationManager _navigationManager;
	// Use this for initialization
	void Start ()
	{
	    _navigationManager = NavigationManager.Instance;
	    _navigationManager.SetTargets(GameObjects);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("o")) _navigationManager.StartNavigation();
        if (Input.GetKeyDown("m")) _navigationManager.SetNavigationMode("Arrow");
        if (Input.GetKeyDown("l")) _navigationManager.SetNavigationMode("Line");
	}
}
