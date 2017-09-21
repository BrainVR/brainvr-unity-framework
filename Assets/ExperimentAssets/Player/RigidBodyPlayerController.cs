using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace Assets.ExperimentAssets.Scripts.Player
{
    public class RigidBodyPlayerController : PlayerController
    {
        private RigidbodyFirstPersonController _rigidbodyScript;
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
        #region Public API
        public override void LookAtPosition(Vector2 point)
        {
            Vector3 lookingPoint = new Vector3(point.x, Camera.main.transform.position.y, point.y);
            LookAtPosition(lookingPoint);
        }
        public override void LookAtPosition(Vector3 point)
        {
            gameObject.transform.LookAt(point);
        }
        public override void EnableMovement(bool bo = true)
        {
            _rigidbodyScript.BlockMovemennt = !bo;
        }
        public override void EnableRotation(bool bo = true)
        {
            _rigidbodyScript.BlockRotation = !bo;
        }
        #endregion
    }
}
