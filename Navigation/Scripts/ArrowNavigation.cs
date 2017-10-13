using UnityEngine;

namespace BrainVR.UnityFramework.Navigation
{
    public class ArrowNavigation : NavigationController
    {
        public GameObject Arrow;
        public new const string Name = "Arrow";
        public float Speed = 5f;
        public Vector3 PlayerOffset = new Vector3(0, -0.1f, 0.1f);

        void OnEnable()
        {
            if (!Arrow) Arrow = transform.GetChild(0).gameObject;
        }
        public override void OnNavigate()
        {
            PlayerHook();
            Arrow.SetActive(true);
        }
        public override void OnStop()
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
            var difference = Target.position - Arrow.transform.position;
            difference.y = 0F;     // Flatten the vector, assuming you're not concerned with indicating height difference
            Arrow.transform.rotation = Quaternion.Slerp(Arrow.transform.rotation, Quaternion.LookRotation(difference.normalized), Time.deltaTime * Speed);
        }
        private void RotateArrow2()
        {
            Arrow.transform.LookAt(Target);
        }
    }
}
