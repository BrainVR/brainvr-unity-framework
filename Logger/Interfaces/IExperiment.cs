using System;

namespace BrainVR.UnityLogger.Interfaces
{
    public class ExperimentStateArgs : EventArgs
    {
        public IExperiment Experiment;
        public string FromState;
        public string ToState;
    }
    public class ExperimentEventArgs : EventArgs
    {
        public IExperiment Experiment;
        public string Event;
    }
    public class TrialStateArgs : EventArgs
    {
        public IExperiment Experiment;
        public string FromState;
        public string ToState;
    }
    public class TrialEventArgs : EventArgs
    {
        public IExperiment Experiment;
        public string Event;
    }
    public class ExperimentMessageArgs : EventArgs
    {
        public IExperiment Experiment;
        public string Message;
    }
    public interface IExperiment
    {
        string Name { get; }
        int TrialNumber { get; }
        int ExperimentNumber { get; }

        event EventHandler<ExperimentStateArgs> ExperimentStateChanged;
        event EventHandler<ExperimentEventArgs> ExpeirmentEventSent;
        event EventHandler<TrialStateArgs> TrialStateChanged;
        event EventHandler<TrialEventArgs> TrialEventSent;
        event EventHandler<ExperimentMessageArgs> MessageSent;
        string ExperimentHeaderLog();
    }
}