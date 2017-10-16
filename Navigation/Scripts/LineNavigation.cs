﻿using BrainVR.UnityFramework.Player;
using UnityEngine;
using UnityEngine.AI;

namespace BrainVR.UnityFramework.Navigation
{
    public class LineNavigation : NavigationController
    {
        LineRenderer _lineRenderer;
        public new string Name = "Line";
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
        public override void OnNavigationStart()
        {
            Show();
        }
        public override void OnNavigationStop()
        {
            Hide();
        }
        public override void OnUpdate()
        {
            var path = UpdatePath();
            DrawLine(path);
        }   
        #endregion
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
