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
        public new const string Name = "Arrow";
        public float Speed = 5f;
        public Vector3 PlayerOffset = new Vector3(0, -0.1f, 0.1f);

        private ArrowMode ArrowMode;

        public ArrowNavigation(ArrowMode arrowMode)
        {
            ArrowMode = arrowMode;
        }

        void OnEnable()
        {
            if (!Arrow) Arrow = transform.GetChild(0).gameObject;
        }
        public override void OnNavigationStart()
        {
            PlayerHook();
            Arrow.SetActive(true);
        }
        public override void OnNavigationStop()
        {
            Arrow.SetActive(false);
            PlayerUnhook();
        }
        public override void OnUpdate()
        {
            RotateArrow();
        }
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
            }
            difference.y = 0F;     // Flatten the vector, assuming you're not concerned with indicating height difference
            Arrow.transform.rotation = Quaternion.Slerp(Arrow.transform.rotation, Quaternion.LookRotation(difference.normalized), Time.deltaTime * Speed);
        }
    }
}
