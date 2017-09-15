using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace Assets.ExperimentAssets.Scripts.Player
{
    public class PlayerController : Singleton<PlayerController>
    {
        private RigidbodyFirstPersonController _rigidbodyScript ;
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
        public void EnableMovement(bool bo = true)
        {
            _rigidbodyScript.BlockMovemennt = !bo;
        }

        public void Unstuck()
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, transform.position.y, 0), 1);
            EnableMovement();
        }

        #endregion
        #region Rotating

        public void EnableRotation(bool bo = true)
        {
            _rigidbodyScript.BlockRotation = !bo;
        }

        public void LookAtPosition(Vector2 point)
        {
            Vector3 lookingPoint = new Vector3(point.x, Camera.main.transform.position.y,point.y);
            LookAtPosition(lookingPoint);
        }

        public void LookAtPosition(Vector3 point)
        {
            gameObject.transform.LookAt(point);
        }
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
        #region Monobehaviour
        void Start()
        {
            _rigidbodyScript = gameObject.GetComponent<RigidbodyFirstPersonController>();
            if (_rigidbodyScript == null)
            {
                Debug.LogError("Player log doesn't have a rigid body attached");
                Debug.Break();
            }
        }
        #endregion
        #region PRIVATE FUCNTIONS
        #endregion
    }
}
