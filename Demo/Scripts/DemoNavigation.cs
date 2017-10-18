using BrainVR.UnityFramework.Navigation;
using UnityEngine;

public class DemoNavigation : MonoBehaviour
{

    public GameObject NavigationTarget;

    private NavigationManager _manager;
    void Start()
    {
        _manager = NavigationManager.Instance;
        _manager.Target = NavigationTarget.transform;
        _manager.StartNavigation();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) _manager.StopNavigation();
        if (Input.GetKeyDown(KeyCode.L)) _manager.StartNavigation();
        if (Input.GetKeyDown(KeyCode.J)) SwitchType();
    }

    void SwitchType()
    {
        switch (_manager.SelectedNavigation)
        {
            case "Arrow":
                _manager.SetNavigationMode("Line");
                //needs to return, toherwise we call the next function as well
                return;
            case "Line":
                _manager.SetNavigationMode("Arrow");
                break;
        }
    }
}
