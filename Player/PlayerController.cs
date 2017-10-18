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
        public abstract Vector3 Position { get; set; }
        public Vector2 Vector2Position
        {
            get { return new Vector2(Position.x, Position.z); }
        }
        public void MoveToCenter()
        {
            MoveToPosition(new Vector2(0, 0));
        }
        public void MoveToPosition(Vector2 position)
        {
            Vector3 movePosition = new Vector3(position.x, gameObject.transform.position.y, position.y);
            Position = movePosition;
        }
        public void Unstuck()
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, transform.position.y, 0), 1);
            EnableMovement();
        }
        #endregion
        #region Rotating
        public abstract Vector2 Rotation { get; }
        public abstract void EnableRotation(bool bo = true);
        public abstract void LookAtPosition(Vector2 point);
        public abstract void LookAtPosition(Vector3 point);
        #endregion
        #region Setting parameters
        public abstract void SetHeight(float height);
        public abstract void SetSpeed(float speed);
        #endregion
        #region Information
        public abstract Vector2 PointingDirection { get; }
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
