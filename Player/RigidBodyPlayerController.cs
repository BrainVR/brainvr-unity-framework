using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace BrainVR.UnityFramework.Player
{
    public class RigidBodyPlayerController : PlayerController
    {
        private RigidbodyFirstPersonController _rigidbodyScript;
        private Rigidbody _rigidbody;
        private CapsuleCollider _collider;

        #region Monobehaviour
        void Awake()
        {
            _collider = gameObject.GetComponent<CapsuleCollider>();
            _rigidbodyScript = gameObject.GetComponent<RigidbodyFirstPersonController>();
            _rigidbody = gameObject.GetComponent<Rigidbody>();

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
        public override Vector2 PointingDirection { get { return Rotation; } }
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
        //this is suboptimal solution, but it works
        public void Stop()
        {
            StartCoroutine(Sttoper());
        }
        //The problem without yield is that i couldn't figure out how to stop player completely
        //for some reason one fixed update deletes the values in velocity, but reinstantiates them during a next fixed update.
        //There is some "leftover" force being still applied, so I just blck the movemembt for 0.2 s
        private IEnumerator Sttoper()
        {
            EnableMovement(false);
            _rigidbodyScript.movementSettings.CurrentTargetSpeed = 0f;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            yield return new WaitForSeconds(0.2f);
            EnableMovement();
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
            return "Position; Rotation.X; Rotation.Y;";
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
