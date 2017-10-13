using System.Collections.Generic;
using System.Linq;
using BrainVR.UnityFramework.Player;
using UnityEngine;
using UnityEngine.AI;

namespace BrainVR.UnityFramework.Navigation
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

        List<NavigationController> _controllers = new List<NavigationController>();

        public bool IsNavigating;
        public Transform Target;
        public NavigationMode NavigationMode;

        private bool CanNavigate
        {
            get { return (_agent != null) && (Target != null); }
        }
        #region MonoBehaviour
        void Start()
        {
            _lineNavigation = _lineNavigation ?? GetComponentInChildren<LineNavigation>();
            _agent = _agent ?? PlayerController.Instance.gameObject.GetComponentInChildren<NavMeshAgent>();

            _controllers = GetComponentsInChildren<NavigationController>().ToList();
        }
        void Update()
        {
            if (!IsNavigating) return;
            if (!CanNavigate) return;
            Navigate();
        }
        #endregion
        #region PublicAPI
        public NavigationController GetController(int i)
        {
            return _controllers[i];
        }
        public void SetAgent(NavMeshAgent agent)
        {
            _agent = agent;
        }
        public void StopNavigation()
        {
            Target = null;
            IsNavigating = false;
            switch (NavigationMode)
            {
                case NavigationMode.Line:
                    _lineNavigation.Hide();
                    break;
            }
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
            var path = new NavMeshPath();
            _agent.CalculatePath(Target.position, path);
            switch (NavigationMode)
            {
                case NavigationMode.Line:
                    _lineNavigation.UpdatePath(path);
                    break;
            }
        }
        #endregion
    }
}
