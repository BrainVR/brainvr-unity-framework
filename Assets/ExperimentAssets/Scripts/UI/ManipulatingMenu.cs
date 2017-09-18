﻿using Assets.ExperimentAssets.Scripts.Experiments;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.ExperimentAssets.Scripts.UI
{
    public class ManipulatingMenu : MonoBehaviour
    {
        public GameObject DynamicGroupQuests;
        public GameObject NewButtonPrefab;

        void Start()
        {
            TestManager testManager = TestManager.Instance;
            if (testManager)
            {
                foreach (var experiment in testManager.Experiments)
                {
                    AddButton(experiment);
                }
            }
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