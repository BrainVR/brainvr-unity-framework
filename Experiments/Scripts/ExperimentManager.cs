using BrainVR.UnityFramework.DataHolders;
using BrainVR.UnityFramework.UI.InGame;
using UnityEngine;

namespace BrainVR.UnityFramework.Experiments.Helpers
{
    public class ExperimentManager : Singleton<ExperimentManager>
    {
        public Experiment Experiment;

        #region MonoBehaviour
        void Start ()
        {
            var settings = SettingsHolder.Instance.CurrentExperimentSettings();
            if (settings != null) LoadExperiment(settings.ExperimentName, settings);
        }
        void Update()
        {
            if (Input.GetButtonDown("HideUI"))
            {
                var ui = ExperimentCanvasManager.Instance.gameObject;
                ui.SetActive(!ui.activeInHierarchy);
            }
        }
        #endregion
        public void LoadExperiment(string experimentName, ExperimentSettings settings = null)
        {
            var experiment = ExperimentLoader.CreateExperimentGO(experimentName, settings);
            if (Experiment) Destroy(Experiment.gameObject);
            Experiment = experiment;
        }
        #region Experiment control
        public void StartExperiment()
        {
            if (Experiment != null && Experiment.ExperimentState <= ExperimentState.Closed) Experiment.StartExperiment();
        }
        public void StopExperiment()
        {
            if (Experiment && Experiment.ExperimentState > ExperimentState.Closed) Experiment.FinishExperiment();
        }
        public void RestartExperiment()
        {
            StopExperiment();
            StartExperiment();
        }
        public void SwitchExperimentState()
        {
            
        }
        public void SetTrial(int i)
        {
            if (Experiment != null && Experiment.ExperimentState > ExperimentState.Closed) Experiment.ForceSetTrial(i);
        }
        #endregion
    }
}
