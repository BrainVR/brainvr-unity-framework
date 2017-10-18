using System.Collections.Generic;
using System.Linq;
using BrainVR.UnityFramework.Player;
using UnityEngine;
using UnityEngine.AI;

namespace BrainVR.UnityFramework.Navigation
{
    public class NavigationManager : Singleton<NavigationManager>
    {
        private NavMeshAgent _agent;
        List<NavigationController> _controllers = new List<NavigationController>();

        public bool IsNavigating;
        public Transform Target;

        private int _selectedNav;
        public string SelectedNavigation
        {
            get { return _controllers[_selectedNav].name; }
        }
        private NavigationController CurrentNavigationController
        {
            get { return _controllers[_selectedNav]; }
        }
        private bool CanNavigate
        {
            get { return _agent != null && Target != null; }
        }
        #region MonoBehaviour
        void Start()
        {
            _agent = _agent ?? PlayerController.Instance.gameObject.GetComponentInChildren<NavMeshAgent>();

            _controllers = GetComponentsInChildren<NavigationController>().ToList();
            SetControllersAgent();
        }
        #endregion
        #region PublicAPI
        public void SetAgent(NavMeshAgent agent)
        {
            _agent = agent;
            SetControllersAgent();
        }
        public void StartNavigation()
        {
            if (!CanNavigate) return;
            IsNavigating = true;
            CurrentNavigationController.Target = Target;
            CurrentNavigationController.StartNavigation();
        }
        public void StopNavigation()
        {
            IsNavigating = false;
            CurrentNavigationController.StopNavigation();
        }
        public void SetTarget(GameObject go)
        {
            Target = go.transform;
        }
        public void SetNavigationMode(string controllerName)
        {
            var index = _controllers.FindIndex(c => c.Name == controllerName);
            if (index > -1)
            {
                SetNavigationMode(index);
                return;
            }
            Debug.Log("There is no controller of such name.");
        }
        public void SetNavigationMode(int iController)
        {
            if (iController < _controllers.Count)
            {
                //need to restart the navigation process
                //cant call stop navigation because that would set IsNavigating to false
                if(IsNavigating) CurrentNavigationController.StopNavigation();
                _selectedNav = iController;
                if (IsNavigating) StartNavigation();
            }
            else Debug.Log("There aren't that many controllers in the manager");
        }
        #endregion
        private void SetControllersAgent()
        {
            foreach (var controler in _controllers)
                controler.SetAgent(_agent);
        }
}
}
