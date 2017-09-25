using System;
using BrainVR.UnityFramework.Objects;
using UnityEngine;

namespace BrainVR.UnityFramework.Scripts.Objects.Goals
{
    public delegate void GoalEnterAction(GoalController sender, EventArgs e);

    public class GoalController : ArenaObject
    {
        public event GoalEnterAction OnEnter;
        public event GoalEnterAction OnExit;
        public string GoalName;
        public bool PlayerInside { get; private set; }

        #region GOAL API

        #endregion
        #region Monobiabiour stuff - don't touch

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                PlayerInside = true;
                if (OnEnter != null) OnEnter(this, EventArgs.Empty);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                PlayerInside = false;
                if (OnExit != null) OnExit(this, EventArgs.Empty);
            }
        }
        #endregion

    }
}

