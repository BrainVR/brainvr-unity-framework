using System.Collections.Generic;
using BrainVR.UnityLogger.Interfaces;
using UnityEngine;

namespace BrainVR.UnityFramework.Player
{
    public abstract class PlayerController : Singleton<PlayerController>, IPlayerController
    {
        #region Public API
        #region Moving
        public abstract void EnableMovement(bool bo = true);
        public virtual Vector3 Position
        {
            get { return transform.position; }
            set { gameObject.transform.position = value; }
        }
        public void MoveToCenter()
        {
            MoveToPosition(new Vector2(0, 0));
        }
        public void MoveToPosition(Vector2 position)
        {
            Vector3 movePosition = new Vector3(0, gameObject.transform.position.y, 0);
            Position = movePosition;
        }
        public void Unstuck()
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, transform.position.y, 0), 1);
            EnableMovement();
        }
        #endregion
        #region Rotating
        public virtual Vector2 Rotation
        {
            get { return new Vector2(transform.eulerAngles.y, Camera.main.transform.eulerAngles.x); }
        }
        public abstract void EnableRotation(bool bo = true);
        public abstract void LookAtPosition(Vector2 point);
        public abstract void LookAtPosition(Vector3 point);
        #endregion
        #region Setting parameters
        public abstract void SetHeight(float height);
        public abstract void SetSpeed(float speed);
        #endregion
        #endregion
        #region PRIVATE FUCNTIONS
        #endregion
        #region Interface implementation
        public abstract string HeaderLine();
        public abstract List<string> PlayerInformation();
        #endregion

    }
}
