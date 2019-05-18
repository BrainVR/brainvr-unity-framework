using BrainVR.UnityFramework.Experiment;
using BrainVR.UnityFramework.Helpers;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

namespace BrainVR.UnityFramework.Scripts.Experiments.DemoExperiment
{
    [CreateAssetMenu(menuName = "BrainVR/Experiment Settings/Demo Settings")]
    public class DemoExperimentSettings : ExperimentSettings
    {
        int WhichBlock = 0;
        public int[] GoalOrder = {0, 1, 2};

    }
}
