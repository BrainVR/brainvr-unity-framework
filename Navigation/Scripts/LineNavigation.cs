using BrainVR.UnityFramework.Player;
using UnityEngine;
using UnityEngine.AI;

namespace BrainVR.UnityFramework.Navigation
{
    public class LineNavigation : NavigationController
    {
        LineRenderer _lineRenderer;
        private int _lastPositionCount = -1;
        #region MonoBehaviour
        void Start()
        {
            _lineRenderer = gameObject.GetComponent<LineRenderer>();
        }
        #endregion
        #region Public API
        public void Show()
        {
            _lineRenderer.enabled = true;
        }
        public void Hide()
        {
            _lineRenderer.enabled = false;
        }
        public void Clear()
        {
            _lineRenderer.positionCount = 0;
        }
        #region Implementations of Controller
        protected override void OnNavigationStart()
        {
            Show();
        }
        protected override void OnNavigationStop()
        {
            Hide();
        }
        protected override void OnUpdate()
        {
            var path = UpdatePath();
            DrawLine(path);
        }   
        #endregion
        #endregion
        private void DrawLine(NavMeshPath path)
        {
            if (path.corners.Length <= 0) return;
            var positionCount = path.corners.Length; //set the array of positions to the amount of corners
            _lineRenderer.SetPosition(0, PlayerController.Instance.transform.position);
            //we don't redraw this until its needed. - until position count doesn't change we are still looking only for the line to the first "node"
            if (_lastPositionCount == positionCount) return;
            _lastPositionCount = positionCount;
            _lineRenderer.positionCount = positionCount;
            for (var i = 1; i < path.corners.Length; i++)
            {
                _lineRenderer.SetPosition(i, path.corners[i]); //go through each corner and set that to the line renderer's position
            }
        }
    }
}
