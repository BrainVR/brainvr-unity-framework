using BrainVR.UnityFramework.Player;
using UnityEngine;
using UnityEngine.AI;

namespace BrainVR.UnityFramework.Navigation
{
    public class LineNavigation : NavigationController
    {
        LineRenderer _lineRenderer;
        #region MonoBehaviour
        void Start()
        {
            _lineRenderer = gameObject.GetComponent<LineRenderer>();
            _lineRenderer.positionCount = 1; //precaution
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
            _lineRenderer.positionCount = 1;
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
        protected override void OnTargetChange()
        {
            Clear();
        }
        #endregion
        #endregion
        private void DrawLine(NavMeshPath path)
        {
            if (path.corners.Length <= 1) return;
            _lineRenderer.SetPosition(0, PlayerController.Instance.transform.position);
            //we don't redraw this until its needed. - until position count doesn't change we are still looking only for the line to the first "node"
            if (_lineRenderer.positionCount == path.corners.Length) return;
            _lineRenderer.positionCount = path.corners.Length;
            for (var i = 1; i < path.corners.Length; i++)
            {
                _lineRenderer.SetPosition(i, path.corners[i]); //go through each corner and set that to the line renderer's position
            }
        }
    }
}
