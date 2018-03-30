using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace BrainVR.UnityFramework.Navigation
{
    public class DistanceCalculator : MonoBehaviour
    {
        public Transform Start;
        public Transform End;

        public float CalculateDistance()
        {
            var path = new NavMeshPath();
            NavMesh.CalculatePath(Start.position, End.position, NavMesh.AllAreas, path);
            var distance = 0f;
            for (var i = 1; i < path.corners.Length; i++)
            {
                var twoPointDistance = Vector3.Distance(path.corners[i - 1], path.corners[i]);
                distance += twoPointDistance;
            }
            return distance;
        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(DistanceCalculator), true)]
    public class DistanceCalculatorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var myScript = (DistanceCalculator)target;
            if(GUILayout.Button("calculate distance"))
            {
                Debug.Log(CalculatePath(myScript));
            }
        }

        private static float CalculatePath(DistanceCalculator calc)
        {
            return calc.CalculateDistance();
        }
    }
#endif
}