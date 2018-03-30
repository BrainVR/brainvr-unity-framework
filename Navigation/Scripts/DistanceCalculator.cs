using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace BrainVR.UnityFramework.Navigation
{
    public class DistanceCalculator : MonoBehaviour
    {
        public NavMeshAgent Agent;
        public Transform Goal;

        public float CalculateDistance()
        {
            var path = new NavMeshPath();
            NavMesh.CalculatePath(Agent.gameObject.transform.position, Goal.position, NavMesh.AllAreas, path);
            Debug.Log(path);
            return path.corners.Length;
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