using System.Collections;
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
        public override Vector3 Position
        {
            get { return transform.position; }
            set { gameObject.transform.position = value; }
        }
        public override Vector2 Rotation
        {
            //in common practice player rotates in Y and camera on X, but this should be reimplemented in VR
            get { return new Vector2(transform.eulerAngles.y, Camera.main.transform.eulerAngles.x); }
        }
        public override void LookAtPosition(Vector2 point)
        {
            Vector3 lookingPoint = new Vector3(point.x, Camera.main.transform.position.y, point.y);
            LookAtPosition(lookingPoint);
        }
        public override void LookAtPosition(Vector3 point)
        {
            //calculating actually from the center of the player object, because it shoudl better correspond
            //to the central aiming dot - games are werid
            var relativePos = point - gameObject.transform.position;
            var targetAngle = Quaternion.LookRotation(relativePos).eulerAngles;
            gameObject.transform.eulerAngles = new Vector3(0, targetAngle.y, 0);
            Camera.main.transform.localEulerAngles = new Vector3(targetAngle.x, 0, 0);
            //rests mouse look
            _rigidbodyScript.mouseLook.Init(transform, Camera.main.transform);
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
        #region Helpers
        IEnumerator TimeLooker(Vector3 point, float time)
        {
            EnableRotation(false);
            LookAtPosition(point);
            yield return new WaitForSeconds(time);
            EnableRotation();
        }
        #endregion
        #region logging
        public override string HeaderLine()
        {
            return "Time; Position; Rotation.X; Rotation.Y; FPS; Input;";
        }
        public override List<string> PlayerInformation()
        {
            var strgs = new List<string>
            {
                Position.ToString("F4"),
                Rotation.x.ToString("F4"),
                Rotation.y.ToString("F4")
            };
            return strgs;
        }
        #endregion
    }
}
