using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace Assets.ExperimentAssets.Scripts.Player
{
    public abstract class PlayerController : Singleton<PlayerController>
    {
        #region Public API
        #region Moving
        public void MoveToCenter()
        {
            MoveToPosition(new Vector2(0, 0));
        }
        public void MoveToPosition(Vector2 position)
        {
            Vector3 movePosition = new Vector3(0, gameObject.transform.position.y, 0);
            MoveToPosition(movePosition);
        }
        public void MoveToPosition(Vector3 position)
        {
            gameObject.transform.position = position;
        }
        public abstract void EnableMovement(bool bo = true);
        public void Unstuck()
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, transform.position.y, 0), 1);
            EnableMovement();
        }
        #endregion
        #region Rotating
        public abstract void EnableRotation(bool bo = true);
        public abstract void LookAtPosition(Vector2 point);
        public abstract void LookAtPosition(Vector3 point);
        #endregion
        #region Information
        /// <summary>
        /// returns positon in world coordinates ignoring Y axis
        /// </summary>
        /// <returns>Vector2 coordinates (X, Z)</returns>
        public Vector2 GetVector2Position()
        {
            return new Vector2(transform.position.x, transform.position.z);
        }
        #endregion
        #endregion
        #region PRIVATE FUCNTIONS
        #endregion
    }
}
