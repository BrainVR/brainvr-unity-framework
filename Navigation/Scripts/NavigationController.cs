using UnityEngine;

namespace BrainVR.UnityFramework.Navigation
{
    public enum NavigatingState
    {
        Stopped,
        Navigating
    }

    public abstract class NavigationController : MonoBehaviour
    {
        public NavigatingState State;
        public Transform Target { get; set; }
        public const string Name = "Default";

        #region MonoBehaviour
        void Update()
        {
            if (State == NavigatingState.Navigating) OnUpdate();
        }
        #endregion
        #region Public API
        #region Abstracts
        public abstract void OnNavigate();
        public abstract void OnStop();
        public abstract void OnUpdate();
        #endregion
        #region Implemented
        public void NavigateTo(GameObject obj)
        {
            Target = obj.transform;
            Navigate();
        }
        public void Navigate()
        {
            State = NavigatingState.Navigating;
            OnNavigate();
        }
        public void StopNavigation()
        {
            State = NavigatingState.Stopped;
            OnStop();
        }
        #endregion
        #endregion

    }
}