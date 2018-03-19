using UnityEngine;

namespace BrainVR.UnityFramework.Navigation
{
    public enum ArrowMode
    {
        AimDirectly,
        RespectLayout
    }
    public class ArrowNavigation : NavigationController
    {
        public GameObject Arrow;
        public float Speed = 5f;
        public Vector3 PlayerOffset = new Vector3(0, -0.2f, 0.5f);

        private ArrowMode ArrowMode;

        void OnEnable()
        {
            if (!Arrow) Arrow = transform.GetChild(0).gameObject;
        }
        public void SetMode(ArrowMode arrowMode)
        {
            ArrowMode = arrowMode;
        }
        #region IMplementation of the controller
        protected override void OnNavigationStart()
        {
            PlayerHook();
            Arrow.SetActive(true);
        }
        protected override void OnNavigationStop()
        {
            Arrow.SetActive(false);
            PlayerUnhook();
        }
        protected override void OnUpdate()
        {
            RotateArrow();
        }
        protected override void OnTargetChange()
        {

        }
        #endregion
        #region Private functions
        private void PlayerHook()
        {
            Arrow.transform.SetParent(Camera.main.transform, false);
            Arrow.transform.localPosition = PlayerOffset;
        }
        private void PlayerUnhook()
        {
            Arrow.transform.SetParent(transform, false);
            Arrow.transform.SetPositionAndRotation(default(Vector3), default(Quaternion));
        }
        private void RotateArrow()
        {
            var difference = Vector3.zero;
            switch (ArrowMode)
            {
                case ArrowMode.AimDirectly:
                    difference = Target.position - Arrow.transform.position;
                    break;
                case ArrowMode.RespectLayout:
                    //need the updated path from the navmesh and point towards the closes point on the navmesh path
                    difference = Vector3.back;
                    break;
                default:
                    break;
            }
            difference.y = 0F;     // Flatten the vector, assuming you're not concerned with indicating height difference
            Arrow.transform.rotation = Quaternion.Slerp(Arrow.transform.rotation, Quaternion.LookRotation(difference.normalized), Time.deltaTime * Speed);
        }
        #endregion

    }
}
