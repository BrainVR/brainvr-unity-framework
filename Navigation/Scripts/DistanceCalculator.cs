#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace BrainVR.UnityFramework.Navigation
{
    public class DistanceCalculator : MonoBehaviour
    {
        public Transform Start;
        public Transform End;

        public NavMeshPath CalculatePath()
        {
            var path = new NavMeshPath();
            return NavMesh.CalculatePath(Start.position, End.position, NavMesh.AllAreas, path) ? path : null;
        }

        public float CalculateDistance()
        {
            var path = CalculatePath();
            return CalculateDistance(path);
        }

        public float CalculateDistance(NavMeshPath path)
        {
            if (path == null) return 0;
            var distance = 0f;
            for (var i = 1; i < path.corners.Length; i++)
            {
                var twoPointDistance = Vector3.Distance(path.corners[i - 1], path.corners[i]);
                distance += twoPointDistance;
            }
            return distance;
        }

    }

    [CustomEditor(typeof(DistanceCalculator), true)]
    public class DistanceCalculatorEditor : Editor
    {
        private DistanceCalculator _distanceCalculator;
        private Vector3[] _path;

        void OnEnable()
        {
            _distanceCalculator = (DistanceCalculator) target;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("calculate distance"))
            {
                var path = _distanceCalculator.CalculatePath();
                if (path == null)
                {
                    Debug.Log("Couldn't calculate path");
                    return;
                }
                _path = path.corners;
                Debug.Log(_distanceCalculator.CalculateDistance());
            }
        }

        void OnSceneGUI()
        {
            if (_path == null) return;
            Handles.Label(_path.First(), "Start");
            Handles.Label(_path.Last(), "Goal");
            Handles.color = Color.green;
            for (var i = 1; i < _path.Length; i++)
            {
                Handles.DrawLine(_path[i - 1], _path[i]);
            }
        }
    }
}
#endif