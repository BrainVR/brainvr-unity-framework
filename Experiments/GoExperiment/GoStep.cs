using UnityEngine;

namespace BrainVR.UnityFramework.Experiment.GoExperiment
{
    public abstract class GoStep : MonoBehaviour
    {
        #region Monobehaviour
        // Use this for initialization
        void Start ()
        {
		
        }
        // Update is called once per frame
        void Update ()
        {
		
        }
        #endregion

        #region Public API

        #endregion

        #region Step Flow

        public void StepStart()
        {
            OnStepInitiated();
            AfterStepInitiated();
            OnStepStart();
            AfterStepStart();
        }
        public void StepFinish()
        {
            OnStepFinished();
            AfterStepFinished();
            OnStepClosed();
            AfterStepClosed();
        }
        #endregion

        #region ForcedAPI
        public virtual void OnStepInitiated() { }
        public virtual void AfterStepInitiated() { }
        public virtual void OnStepStart() { }
        public virtual void AfterStepStart() { }
        public virtual void OnStepFinished() { }
        public virtual void AfterStepFinished() { }
        public virtual void OnStepClosed() { }
        public virtual void AfterStepClosed() { }
        #endregion
    }
}
