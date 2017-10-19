using System;
using BrainVR.UnityFramework.Experiments.Helpers;
using BrainVR.UnityFramework.Menu;
using BrainVR.UnityLogger.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace BrainVR.UnityFramework.UI.InGame
{
    public class TestStarterButton : MonoBehaviour
    {
        public Text Text;
        private Experiment _experiment;

        public void AddExperiment(Experiment experiment)
        {
            _experiment = experiment;
            _experiment.ExperimentStateChanged += ButtonStateChanged;
            SetButtonStart();
        }
        private void ButtonStateChanged(object sender, ExperimentStateArgs args)
        {
            ExperimentState toState = (ExperimentState) Enum.Parse(typeof(ExperimentState), args.ToState);
            switch (toState)
            {
                case ExperimentState.Started:
                    SetButtonStop();
                    break;
                case ExperimentState.Closed:
                    SetButtonStart();
                    break;
                default:
                    break;
            }
        }
        private void SetButtonStart()
        {
            SetText("Start " + _experiment.name);
            AddListenerStart();
        }
        private void SetButtonStop()
        {
            SetText("Stop " + _experiment.name);
            AddListenerQuit();
        }
        private void AddListenerStart()
        {
            var myButton = GetComponent<Button>();
            myButton.onClick.RemoveAllListeners();
            myButton.onClick.AddListener(() => MenuExperiment.Instance.TurnMenuOff());
            myButton.onClick.AddListener(() => _experiment.StartExperiment());
        }

        private void AddListenerQuit()
        {
            var myButton = GetComponent<Button>();
            myButton.onClick.RemoveAllListeners();
            myButton.onClick.AddListener(() => MenuExperiment.Instance.TurnMenuOff());
            myButton.onClick.AddListener(() => _experiment.FinishExperiment());
        }
        private void SetText(string s)
        {
            Text.text = s;
        }
    }
}
