using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace BrainVR.UnityFramework.Player
{
    public class RigidBodyPlayerController : PlayerController
    {
        private RigidbodyFirstPersonController _rigidbodyScript;
        private CapsuleCollider _collider;
        #region Monobehaviour

        void Awake()
        {
            _collider = gameObject.GetComponent<CapsuleCollider>();
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

        public override void SetHeight(float height)
        {
            _collider.height = height / 10;
        }

        public override void SetSpeed(float speed)
        {
            _rigidbodyScript.movementSettings.ForwardSpeed = speed;
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
        #region logging
        public override string HeaderLine()
        {
            return "Time; Position; Rotation.X; Rotation.Y; FPS; Input;";
        }
        public override List<string> PlayerInformation()
        {
            List<string> strgs = new List<string>();
            // position 
            strgs.Add(transform.position.ToString("F4"));
            // rotation Y
            strgs.Add(transform.eulerAngles.y.ToString("F4"));
            // rotation X
            strgs.Add(Camera.main.transform.eulerAngles.x.ToString("F4"));
            return strgs;
        }
        #endregion
    }
}
