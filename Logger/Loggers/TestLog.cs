using System.Collections.Generic;
using BrainVR.UnityFramework.Logger.Interfaces;
using UnityEngine;

namespace BrainVR.UnityFramework.Logger
{
    public class TestLog : MonoLog
    {
        private IExperiment _experiment;

        protected override string LogName => "test_" + _experiment.Name;

        public static TestLog StartNewTest(IExperiment experiment)
        {
            //instantiates empty game objects
            var experimentInfo = ExperimentInfo.Instance;
            var id = experimentInfo == null ? "NEO" : experimentInfo.Participant.Id;
            var go = new GameObject();
            var testLog = go.AddComponent<TestLog>();
            testLog._experiment = experiment;
            go.transform.name = testLog._experiment.Name + "_test_log";
            return testLog.PrepareLogging(go, id);
        }
        private TestLog PrepareLogging(GameObject go, string id)
        {
            //parents itself under IsLogging thing - under master
            go.transform.SetParent(MasterLog.Instance.transform);
            Log = new Log(id, LogName);
            return this;
        }
        public void StartLogging()
        {
            WriteTestHeader();
            //subscribes to events
            Subscribe(_experiment);
        }
        private void WriteTestHeader()
        {
            Log.WriteLine("***TEST HEADER***");
            Log.WriteLine(_experiment.ExperimentHeaderLog());
            Log.WriteLine("---TEST HEADER---");
            Log.WriteLine(TestLogHeaderLine());
        }
        private string TestLogHeaderLine()
        {
            return "Time;Sender;Index;Event;";
        }
        private void Subscribe(IExperiment experiment)
        {
            experiment.ExperimentStateChanged += LogExperimentStateChanged;
            experiment.TrialStateChanged += LogTrialStateChanged;
            experiment.ExpeirmentEventSent += LogExperimentEvent;
            experiment.TrialEventSent += LogTrialEvent;
            experiment.MessageSent += LogCustomExperimentMessage;
        }
        private void Unsubcribe(IExperiment experiment)
        {
            experiment.ExperimentStateChanged -= LogExperimentStateChanged;
            experiment.TrialStateChanged -= LogTrialStateChanged;
            experiment.MessageSent -= LogCustomExperimentMessage;
            experiment.ExpeirmentEventSent -= LogExperimentEvent;
            experiment.TrialEventSent -= LogTrialEvent;
        }
        private void WriteEvent(List<string> strgs)
        {
            AddTimestamp(ref strgs);
            WriteLine(strgs);
        }
        private void LogCustomExperimentMessage(object obj, ExperimentMessageArgs args)
        {
            var toWrite = new List<string> {args.Message};
            WriteEvent(toWrite);
        }
        private void LogTrialStateChanged(object obj, TrialStateArgs args)
        {
            var toWrite = new List<string> {"Trial", args.Experiment.TrialNumber.ToString(), args.ToState };
            WriteEvent(toWrite);
        }
        private void LogTrialEvent(object obj, TrialEventArgs args)
        {
            var toWrite = new List<string> { "Trial", args.Experiment.TrialNumber.ToString(), args.Event};
            WriteEvent(toWrite);
        }
        private void LogExperimentStateChanged(object obj, ExperimentStateArgs args)
        {
            var toWrite = new List<string> {"Experiment", args.Experiment.ExperimentNumber.ToString(), args.ToState};
            WriteEvent(toWrite);
        }
        private void LogExperimentEvent(object obj, ExperimentEventArgs args)
        {
            var toWrite = new List<string> { "Experiment", args.Experiment.ExperimentNumber.ToString(), args.Event };
            WriteEvent(toWrite);
        }
        public void StopLogging(IExperiment experiment)
        {
            //closes stream
            Close();
            //cleans its game object
            Unsubcribe(experiment);
            Destroy(gameObject);
        }
    }
}
