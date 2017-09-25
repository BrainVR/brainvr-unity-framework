using BrainVR.UnityFramework.DataHolders;
using BrainVR.UnityFramework.Menu;
using BrainVR.UnityFramework.Player;
using UnityEngine;

namespace BrainVR.UnityFramework.Experiments
{
    public class ExperimentManager : Singleton<ExperimentManager>
    {
        public MenuExperiment Menu;
        public PlayerController PlayerController;
        public GameObject UI;

        public Experiment Experiment;

        #region MonoBehaviour
        void Start ()
        {
            Menu = MenuExperiment.Instance;
            PlayerController = PlayerController.Instance;
            ExperimentSettings settings = SettingsHolder.Instance.CurrentExperimentSettings();
            if (settings != null) LoadExperiment(settings.ExperimentName, settings);
        }
        void Update()
        {
            if(Input.GetButtonDown("HideUI")) UI.SetActive(!UI.activeInHierarchy);
        }
        #endregion
        public void LoadExperiment(string experimentName, ExperimentSettings settings = null)
        {
            Experiment experiment = ExperimentLoader.CreateExperimentGO(experimentName, settings);
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
            if (Experiment && Experiment.ExperimentState > ExperimentState.Closed) Experiment.StopExperiment();
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
