using System;
using System.Collections.Generic;
using BrainVR.UnityFramework.Arduino;
using BrainVR.UnityFramework.Scripts.Experiments.DemoExperiment;
using BrainVR.UnityFramework.Scripts.Objects.Beeper;
using UnityEngine;

namespace BrainVR.UnityFramework.Experiments.Demo
{
    public class DemoExperiment : Experiment
    {
        //maybe more to some other abstract class
        protected BeeperManager BeepManager;

        protected ArduinoController Arduino;

        protected float TrialStartTime;
        protected float TrialEndTime;
        protected List<float> TrialTimes = new List<float>();
        public new DemoExperimentSettings Settings;    
        #region Forced API
        public override void AddSettings(ExperimentSettings settings)
        {
            Settings = (DemoExperimentSettings)settings;
        }
        #endregion
        #region Experiment Logic
        protected override void OnExperimentInitialise() { }
        protected override void AfterExperimentInitialise() { }
        protected override void ExperimentUpdate() { }
        protected override void OnExperimentSetup()
        {
            BeepManager = BeeperManager.Instance;
            CanvasManager.Show();
            Arduino = ArduinoController.Instance;
            Arduino.Connect();
        }
        protected override void AfterExperimentSetup() { }
        protected override void OnExperimentStart() { }
        protected override void AfterExperimentStart() { }
        protected override void OnTrialSetup() { }
        protected override void OnTrialStart()
        {
            TrialStartTime = Time.realtimeSinceStartup;
        }
        protected override void OnTrialFinished()
        {
            TrialEndTime = Time.realtimeSinceStartup;
            TrialTimes.Add(TrialEndTime - TrialStartTime);
        }
        protected override void OnTrialClosed() { }
        protected override void OnExperimentFinished() { }
        protected override void AfterExperimentFinished() { }
        protected override void OnExperimentClosed()
        {
            CanvasManager.Show(false);
        }
        protected override void AfterExperimentClosed() { }

        public override string ExperimentHeaderLog()
        {
            throw new NotImplementedException();
        }

        protected override void AfterTrialSetup()
        {
            throw new NotImplementedException();
        }

        protected override void AfterTrialStart()
        {
            throw new NotImplementedException();
        }

        protected override void AfterTrialFinished()
        {
            throw new NotImplementedException();
        }

        protected override void AfterTrialClosed()
        {
            throw new NotImplementedException();
        }

        protected override bool CheckForEnd()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Helpers
        #endregion
    }
}
