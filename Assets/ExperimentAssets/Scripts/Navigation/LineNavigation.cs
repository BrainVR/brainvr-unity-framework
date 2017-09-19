using UnityEngine;
using UnityEngine.AI;

namespace Assets.ExperimentAssets.Scripts.Navigation
{
    public class LineNavigation : MonoBehaviour
    {
        LineRenderer _lineRenderer;
        private bool _drawing;

        #region MonoBehaviour
        void Start()
        {
            _lineRenderer = gameObject.GetComponent<LineRenderer>();
        }
        #endregion
        #region Public API
        public void UpdatePath(NavMeshPath path)
        {
            if (_drawing) DrawLine(path);
        }
        public void ShowLine()
        {
            _drawing = true;
        }
        public void HideLine()
        {
            _drawing = false;
        }
        #endregion
        private void DrawLine(NavMeshPath path)
        {
            if (path.corners.Length < 2) return;
            _lineRenderer.positionCount = path.corners.Length; //set the array of positions to the amount of corners
            for (var i = 1; i < path.corners.Length; i++)
            {
                _lineRenderer.SetPosition(i, path.corners[i]); //go through each corner and set that to the line renderer's position
            }
        }

    }
}
