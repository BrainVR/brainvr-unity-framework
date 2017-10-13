﻿using System;
using BrainVR.UnityFramework.UI.InGame;
using BrainVR.UnityLogger;
using BrainVR.UnityLogger.Interfaces;
using UnityEngine;

namespace BrainVR.UnityFramework.Experiments
{
    public enum ExperimentState
    {
        Inactive,
        Closed,
        Finished,
        Initialised,
        WaitingToStart,
        Started,
        //the event running is never thrown
        Running
    }
    public enum ExperimentEvent
    {
        Started,
        ForceFinished,
        Quit
    }
    public enum TrialState
    {
        Paused,
        WaitingToStart,
        Running,
        Finished,
        Closed
    }
    public enum TrialEvent
    {
        ForceFinished
    }
    public abstract class Experiment : MonoBehaviour, IExperiment
    {
        #region interface implementation delegates
        public delegate void ExperimentStateHandler(Experiment ex, ExperimentState fromState, ExperimentState toState);

        protected string _name = "SomeTask";
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int TrialNumber { get; protected set; }
        public int ExperimentNumber { get; protected set; }
        public event EventHandler<ExperimentStateArgs> ExperimentStateChanged;

        public event EventHandler<ExperimentEventArgs> ExpeirmentEventSent;
        public event EventHandler<TrialStateArgs> TrialStateChanged;
        public event EventHandler<TrialEventArgs> TrialEventSent;
        public event EventHandler<ExperimentMessageArgs> MessageSent;
        #endregion
        // Useful state variables
        public ExperimentState ExperimentState { get; set; }
        protected TrialState TrialState { get; set; }

        //managing variables
        protected ExperimentManager ExperimentManager;
        protected ExperimentCanvasManager CanvasManager;
        protected bool ShouldLog = true;
        protected TestLog TestLog;
        protected ExperimentSettings Settings;

        // Unity MonoBehaviour stuff --- dont't use explicitly, use refefined functions inesteadS
        #region Monobehaviour funcions - don't usually need to touch
        void OnEnable()
        {
            ExperimentManager = ExperimentManager.Instance;
        }
        void Update()
        {
            if (ExperimentState == ExperimentState.Running) ExperimentUpdate();
        }
        #endregion
        #region Experiment lifetime
        public void StartExperiment()
        {
            if (ExperimentState >= ExperimentState.Finished)
            {
                Debug.Log("Expeirment is not fully closed. Cannot initialise again. Stop the experiment first.");
                return;
            }
            StartingSequence();
        }
        //default - is Initialise, Setup, Start, can be overriden in child class for some reason
        private void StartingSequence()
        {
            ExperimentInitialise();
            ExperimentSetup();
            ExperimentStart();
        }
        public void FinishExperiment()
        {
            if (ExperimentState < ExperimentState.Finished)
            {
                Debug.Log("Expeirment is not running. Cannot finish.");
                return;
            }
            SendExperimentEvent(ExperimentEvent.ForceFinished);
            StopingSequence();
        }
        protected void StopingSequence()
        {
            if (TrialState < TrialState.Finished) ForceFinishTrial();
            if (TrialState == TrialState.Finished) TrialClose();
            ExperimentFinish();
            ExperimentClose();
        }
        //called every frame if expeirment is active
        protected virtual void ExperimentUpdate() { }      
        //happends when the experiment is started - non monobehaviour logic
        //collects all important variables, creates log
        private void ExperimentInitialise()
        {
            TrialNumber = 0;
            OnExperimentInitialise();
            CanvasManager = ExperimentCanvasManager.Instance;
            SendExperimentStateChanged(ExperimentState.Initialised);
            ExperimentState = ExperimentState.Initialised;
            AfterExperimentInitialise();
        }
        //sets up the pieces 
        // - initializes the log, 
        private void ExperimentSetup()
        {
            OnExperimentSetup();
            if (ShouldLog) StartLogging();
            SendExperimentStateChanged(ExperimentState.WaitingToStart);
            ExperimentState = ExperimentState.WaitingToStart;
            AfterExperimentSetup();
        }
        private void ExperimentStart()
        {
            OnExperimentStart();
            SendExperimentStateChanged(ExperimentState.Started);
            ExperimentState = ExperimentState.Running;
            AfterExperimentStart();
            TrialSetup();
        }
        private void ExperimentFinish()
        {
            OnExperimentFinished();
            SendExperimentStateChanged(ExperimentState.Finished);
            ExperimentState = ExperimentState.Finished;
            AfterExperimentFinished();
        }
        private void ExperimentClose()
        {
            OnExperimentClosed();
            SendExperimentStateChanged(ExperimentState.Closed);
            StopLogging();
            MasterLog.Instance.CloseLogs();
            ExperimentState = ExperimentState.Closed; 
            AfterExperimentClosed();
        }
        #endregion
        #region Each trial lifetime - can be overriden in child class to each ones liking
        public virtual void TrialSetNext()
        {
            if (TrialState == TrialState.Finished) TrialClose();
            //Necessary for quitting - close usually ends the experiment, but the trail of setting new trial continues
            if (ExperimentState <= ExperimentState.Finished)
            {
                Debug.Log("Experiment is finished.");
                return;
            }
            //normal passing of trial - first or last
            if (TrialState != TrialState.Closed) Debug.Log("Cannot setup next, trial not closed");
            TrialNumber++;
            TrialSetup();
        }
        public void ForceNextTrial()
        {
            ForceFinishTrial();
            TrialSetNext();
        }
        public void ForceSetTrial(int i)
        {
            var currentTrial = TrialNumber;
            if (i < 0)
            {
                Debug.Log("Cannot set Trial to lower than 0");
                return;
            }
            //weird setup, but it has to be done without long refactoring
            TrialNumber = i;
            if (CheckForEnd())
            {
                Debug.Log("Cannot set to trial which would end the expeirment.");
                TrialNumber = currentTrial;
                return;
            }
            TrialNumber = currentTrial;
            ForceFinishTrial();
            TrialNumber = i;
            TrialSetup();
        }
        public void ForceFinishTrial()
        {
            if (TrialState > TrialState.Finished) return;
            SendTrialEvent("ForceFinished");
            TrialFinish();
            TrialClose();
        }
        //called when new trial is prepaired
        protected void TrialSetup()
        {
            OnTrialSetup();
            SendTrialStateChanged(TrialState.WaitingToStart);
            TrialState = TrialState.WaitingToStart;
            AfterTrialSetup();
        }
        //called when the trial is actually started
        protected void TrialStart()
        {
            OnTrialStart();
            SendTrialStateChanged(TrialState.Running);
            TrialState = TrialState.Running;
            AfterTrialStart();
        }
        //when the task has been successfully finished
        protected void TrialFinish()
        {
            OnTrialFinished();
            SendTrialStateChanged(TrialState.Finished);
            TrialState = TrialState.Finished;
            AfterTrialFinished();
            if (CheckForEnd()) StopingSequence();
        }
        //called before new trial is set up
        protected void TrialClose()
        {
            OnTrialClosed();
            SendTrialStateChanged(TrialState.Closed);
            TrialState = TrialState.Closed;
            AfterTrialClosed();
        }
        #endregion
        #region Forced Experiment API - needs to be impemented
        //Necessary to instantiate the experiment
        public abstract void AddSettings(ExperimentSettings settings);
        //Doesnt need to be called from outside the experiment - its just to make things clearer in the code
        public abstract string ExperimentHeaderLog();
        //happends when the experiment is started - non monobehaviour logic
        protected abstract void OnExperimentInitialise();
        protected abstract void AfterExperimentInitialise();
        //sets up the pieces
        protected abstract void OnExperimentSetup();
        protected abstract void AfterExperimentSetup();
        protected abstract void OnExperimentStart();
        protected abstract void AfterExperimentStart();
        //called when new trial is prepaired
        protected abstract void OnTrialSetup();
        //called after the new trial is prepaired
        protected abstract void AfterTrialSetup();
        //called when the trial is actually started
        protected abstract void OnTrialStart();
        //called after everything been instantiated
        protected abstract void AfterTrialStart();
        //when the task has been successfully finished
        protected abstract void OnTrialFinished();
        //called after the task has been successfully finished
        protected abstract void AfterTrialFinished();
        //called after Trial has been finished and close - cleanup
        protected abstract void OnTrialClosed();
        //called after Trial has been finished and close - cleanup
        protected abstract void AfterTrialClosed();
        //called automatically after trialCleanup
        protected abstract bool CheckForEnd();
        //when trials end
        protected abstract void OnExperimentFinished();
        protected abstract void AfterExperimentFinished();
        //after OnExperiemntFinished is called
        protected abstract void OnExperimentClosed();
        protected abstract void AfterExperimentClosed();
        #endregion
        #region Some logging helpers
        /// <summary>
        /// Creates Test log file if no has been created before
        /// </summary>
        protected void StartLogging()
        {
            //reinstantiates player log if it doesn't exist
            MasterLog.Instance.Instantiate();
            MasterLog.Instance.StartLogging();
            CreateTestLog();
            if (TestLog) TestLog.StartLogging();
        }
        protected void CreateTestLog()
        {            
            if (!TestLog && ShouldLog) TestLog = TestLog.StartNewTest(this);
        }
        protected void StopLogging()
        {
            MasterLog.Instance.StopLogging();
            if (TestLog) TestLog.StopLogging(this);
            TestLog = null;
        }
        #endregion
        #region Event helpers
        private void SendTrialStateChanged(TrialState toState)
        {
            if (TrialStateChanged != null) TrialStateChanged(this, new TrialStateArgs{Experiment = this, FromState = TrialState.ToString(), ToState = toState.ToString()});
        }
        private void SendExperimentStateChanged(ExperimentState toState)
        {
            if (ExperimentStateChanged != null) ExperimentStateChanged(this, new ExperimentStateArgs{ Experiment = this, FromState = ExperimentState.ToString(), ToState = toState.ToString() }); 
        }
        protected void SendExperimentEvent(ExperimentEvent experimentEvent)
        {
            if (ExpeirmentEventSent != null) ExpeirmentEventSent(this, new ExperimentEventArgs{Experiment = this, Event = experimentEvent.ToString()});
        }
        protected void SendTrialEvent(string s)
        {
            if (TrialEventSent != null) TrialEventSent(this, new TrialEventArgs{Experiment = this, Event = s});
        }
        #endregion
        #region General Functions
        //public void AddSettings(ExperimentSettings settings)
        //{
        //    Settings = settings;
        //}
        #endregion
    }
}