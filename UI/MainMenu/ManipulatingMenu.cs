using BrainVR.UnityFramework.Experiments.Helpers;
using BrainVR.UnityFramework.UI.InGame;
using UnityEngine;

namespace BrainVR.UnityFramework.UI.MainMenu
{
    public class ManipulatingMenu : MonoBehaviour
    {
        public GameObject DynamicGroupQuests;
        public GameObject NewButtonPrefab;

        void Start()
        {
            ExperimentManager manager = ExperimentManager.Instance;
            if (!manager) return;
            AddButton(manager.Experiment);
        }

        private void AddButton(Experiment experiment)
        {
            var go = Instantiate(NewButtonPrefab, default(Vector3), default(Quaternion)) as GameObject;
            go.transform.SetParent(DynamicGroupQuests.transform);
            TestStarterButton button = go.GetComponent<TestStarterButton>();
            button.AddExperiment(experiment);
        }
    }
}
