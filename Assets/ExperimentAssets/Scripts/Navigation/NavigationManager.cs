using UnityEngine;
using UnityEngine.AI;

namespace Assets.ExperimentAssets.Scripts.Navigation
{

    public enum NavigationMode
    {
        Line,
        Arrow
    }

    public class NavigationManager : Singleton<NavigationManager>
    {
        NavMeshAgent _agent;
        LineNavigation _lineNavigation;

        public bool IsNavigating;
        public Transform Target;
        public NavigationMode NavigationMode;

        private bool CanNavigate
        {
            get { return (_agent != null) && (Target != null); }
        }
        #region MonoBehaviour
        void Update()
        {
            if (!IsNavigating) return;
            if (!CanNavigate) return;
            Navigate();
        }
        #endregion
        #region PublicAPI
        public void SetAgent(NavMeshAgent agent)
        {
            _agent = agent;
        }
        public void StopNavigation()
        {
            Target = null;
            IsNavigating = false;
        }
        public void NavigateTo(GameObject go)
        {
            Target = go.transform;
            IsNavigating = true;
        }

        public void SetNavigationMode(NavigationMode mode)
        {
            NavigationMode = mode;
            //update all the crap
        }
        #endregion

        #region Navigation functions
        private void Navigate()
        {
            _agent.SetDestination(Target.position);
            switch (NavigationMode)
            {
                case NavigationMode.Line:
                    _lineNavigation.UpdatePath(_agent.path);
                    break;
            }
        }
        #endregion
    }
}
