using System.Collections.Generic;
using Assets.GeneralScripts;
using UnityEngine;

namespace Assets.ExperimentAssets.Scripts.Experiments
{
    public class TestManager : Singleton<TestManager> {

        public List<Experiment> Experiments = new List<Experiment>();

        // Use this for initialization
        void Awake ()
        {
            UpdateTests();
        }

        private void UpdateTests()
        {
            var experiments = GetComponentsInChildren<Experiment>();
            foreach (var experiment in experiments)
            {
                Experiments.Add(experiment);
            }
        }
    }
}
