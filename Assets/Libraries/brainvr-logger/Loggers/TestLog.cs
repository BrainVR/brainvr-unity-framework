using System.Collections.Generic;
using UnityEngine;
using BrainVR.UnityLogger.Interfaces;

namespace BrainVR.UnityLogger
{
    public class TestLog : MonoLog
    {
        private IExperiment _experiment;

        public override void Instantiate(string str)
        {
            throw new System.NotImplementedException();
        }
        public static TestLog StartNewTest(IExperiment experiment)
        {
            //instantiates empty game objects
            var experimentInfo = ExperimentInfo.Instance;
            var id = experimentInfo == null ? "NEO" : experimentInfo.Participant.Id;
            GameObject go = new GameObject();
            var testLog = go.AddComponent<TestLog>();
            testLog._experiment = experiment;
            go.transform.name = testLog._experiment.Name + "_test_log";
            return testLog.PrepareLogging(go, testLog._experiment, id);
        }
        private TestLog PrepareLogging(GameObject go, IExperiment experiment, string id)
        {
            //parents itself under Logging thing - under master
            go.transform.SetParent(MasterLog.Instance.transform);
            Log = new Log(id, "test_" + experiment.Name);
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
            List<string> toWrite = new List<string> {args.Message};
            WriteEvent(toWrite);
        }
        private void LogTrialStateChanged(object obj, TrialStateArgs args)
        {
            List<string> toWrite = new List<string> {"Trial", args.Experiment.TrialNumber.ToString(), args.ToState };
            WriteEvent(toWrite);
        }
        private void LogTrialEvent(object obj, TrialEventArgs args)
        {
            List<string> toWrite = new List<string> { "Trial", args.Experiment.TrialNumber.ToString(), args.Event};
            WriteEvent(toWrite);
        }
        private void LogExperimentStateChanged(object obj, ExperimentStateArgs args)
        {
            List<string> toWrite = new List<string> {"Experiment", args.Experiment.ExperimentNumber.ToString(), args.ToState};
            WriteEvent(toWrite);
        }
        private void LogExperimentEvent(object obj, ExperimentEventArgs args)
        {
            List<string> toWrite = new List<string> { "Experiment", args.Experiment.ExperimentNumber.ToString(), args.Event };
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
