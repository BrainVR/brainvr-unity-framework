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
        //You can change it to allow multiple different instances of the same navigation to be created
        public string Name = "DefaultNavigation"; 
        private NavMeshAgent _agent; //usually set by navigation manager
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
            OnNavigationStart();
        }
        public void StopNavigation()
        {
            State = NavigatingState.Stopped;
            OnNavigationStop();
        }
        public void SetAgent(NavMeshAgent agent)
        {
            _agent = agent;
        }
        public void TargetChange(Transform target)
        {
            Target = target;
            OnTargetChange();
        }
        #region Abstracts
        protected abstract void OnNavigationStart();
        protected abstract void OnNavigationStop();
        protected abstract void OnUpdate();
        protected abstract void OnTargetChange();
        #endregion
        #endregion
        protected NavMeshPath UpdatePath()
        {
            var path = new NavMeshPath();
            _agent.CalculatePath(Target.position, path);
            return path;
        }
    }
}