using System.Collections.Generic;
using UnityEngine;

namespace BrainVR.UnityFramework.Experiment.GoExperiment
{
    public class GoExperiment : Experiment
    {
        private List<GoStep> _tasks = new List<GoStep>();

        #region MonoBehaviour
        void Start()
        {
            var steps = GetComponentsInChildren<GoStep>();
            foreach (var step in steps)
            {
                _tasks.Add(step);
            }
        }
        #endregion

        public override void AddSettings(ExperimentSettings settings)
        {
            throw new System.NotImplementedException();
        }

        public override string ExperimentHeaderLog()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnExperimentInitialise()
        {
            throw new System.NotImplementedException();
        }

        protected override void AfterExperimentInitialise()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnExperimentSetup()
        {
            throw new System.NotImplementedException();
        }

        protected override void AfterExperimentSetup()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnExperimentStart()
        {
            throw new System.NotImplementedException();
        }

        protected override void AfterExperimentStart()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnTrialSetup()
        {
            throw new System.NotImplementedException();
        }

        protected override void AfterTrialSetup()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnTrialStart()
        {
            throw new System.NotImplementedException();
        }

        protected override void AfterTrialStart()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnTrialFinished()
        {
            throw new System.NotImplementedException();
        }

        protected override void AfterTrialFinished()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnTrialClosed()
        {
            throw new System.NotImplementedException();
        }

        protected override void AfterTrialClosed()
        {
            throw new System.NotImplementedException();
        }

        protected override bool CheckForEnd()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnExperimentFinished()
        {
            throw new System.NotImplementedException();
        }

        protected override void AfterExperimentFinished()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnExperimentClosed()
        {
            throw new System.NotImplementedException();
        }

        protected override void AfterExperimentClosed()
        {
            throw new System.NotImplementedException();
        }
    }
}
