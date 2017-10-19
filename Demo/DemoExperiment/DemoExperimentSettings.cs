using BrainVR.UnityFramework.Experiments.Helpers;
using BrainVR.UnityFramework.Helpers;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

namespace BrainVR.UnityFramework.Scripts.Experiments.DemoExperiment
{
    public class DemoExperimentSettings : ExperimentSettings
    {
        int WhichBlock = 0;

#if UNITY_EDITOR
        [MenuItem("Assets/Experiment/DemoExperimentSettings")]
        public static void CreateDialogueLine()
        {
            ScriptableObjectUtility.CreateAsset<DemoExperimentSettings>();
        }
        [CustomEditor(typeof(DemoExperimentSettings))]
        public class SettingsEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();
                DemoExperimentSettings myScript = (DemoExperimentSettings)target;
                if (GUILayout.Button("Serialise settings")) Debug.Log(myScript.SerialiseOut());
            }
        }
#endif
    }
}
