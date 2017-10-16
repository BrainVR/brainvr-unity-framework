using UnityEngine;
using UnityEngine.AI;

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
        public NavMeshAgent Agent; //usually set by navigation manager
        #region MonoBehaviour
        void Update()
        {
            if (State == NavigatingState.Navigating) OnUpdate();
        }
        #endregion
        #region Public API
        public void StartNavigation()
        {
            State = NavigatingState.Navigating;
        }
        public void StopNavigation()
        {
            State = NavigatingState.Stopped;
            OnNavigationStop();
        }
        #region Abstracts
        public abstract void OnNavigationStart();
        public abstract void OnNavigationStop();
        public abstract void OnUpdate();
        #endregion
        #endregion
        protected void UpdatePath()
        {
            var path = new NavMeshPath();
            Agent.CalculatePath(Target.position, path);
        }

    }
}