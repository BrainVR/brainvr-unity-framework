using System.Collections.Generic;
using System.Linq;
using BrainVR.UnityFramework.Helpers;
using UnityEngine;

namespace BrainVR.UnityFramework.Objects
{
    public class AreaObjectManager<T> : Singleton<T> where T : MonoBehaviour
    {
        public List<ArenaObject> Objects = new List<ArenaObject>();
        public void SetColor(Color color)
        {
            foreach (var controller in Objects)
                controller.SetColor(color);
        }
        public void ResetColor()
        {
            foreach (var controller in Objects)
                controller.ResetColor();
        }
        public void SetType(string s)
        {
            foreach (var controller in Objects)
                controller.SetType(s);
        }
        public void ShowAll(bool bo = true)
        {
            foreach (var obj in Objects)
                obj.Show(bo);
        }
        public void MoveObjectsCircumference(int number, int[] positions, float radius = 15, Vector3 center = default(Vector3))
        {
            //validations
            if (positions.Max() > number - 1)
            {
                Debug.Log("Cannot move goals on positions higher than number of goals");
                return;
            }
            var angle = Mathf.PI * 2 / number;
            for (var i = 0; i < Objects.Count; i++)
            {
                var obj = Objects[i];
                var newPosition = InstantiatingHelper.GetWorldPositionFromVector2(MathHelper.GetCirclePoint(positions[i] * angle, radius), center);
                obj.SetPosition(new Vector3(newPosition.x, obj.transform.position.y, newPosition.z));
                //ooking into the center
                obj.SetRotation(Quaternion.LookRotation(new Vector3(0, obj.transform.position.y, 0) - obj.transform.position));
            }
        }
    }
}
