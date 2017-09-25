using Assets.ExperimentAssets.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.ExperimentAssets.Navigation
{
    public class LineNavigation : MonoBehaviour
    {
        LineRenderer _lineRenderer;

        #region MonoBehaviour
        void Start()
        {
            _lineRenderer = gameObject.GetComponent<LineRenderer>();
        }
        #endregion
        #region Public API
        public void UpdatePath(NavMeshPath path)
        {
            Show();
            DrawLine(path);
        }
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
        #endregion
        private void DrawLine(NavMeshPath path)
        {
            _lineRenderer.positionCount = path.corners.Length; //set the array of positions to the amount of corners
            _lineRenderer.SetPosition(0, PlayerController.Instance.transform.position);
            for (var i = 1; i < path.corners.Length; i++)
            {
                _lineRenderer.SetPosition(i, path.corners[i]); //go through each corner and set that to the line renderer's position
            }
        }

    }
}
