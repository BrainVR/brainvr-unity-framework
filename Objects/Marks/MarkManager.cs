using System.Collections.Generic;
using System.Linq;
using BrainVR.UnityFramework.Scripts.Objects.Marks;
using UnityEngine;

namespace BrainVR.UnityFramework.Objects.Marks
{
    public enum MarkType
    {
        Start,
        Mark
    };

    public class MarkManager : ArenaObjectManager<MarkManager>
    {
        public GameObject MarkPrefab;
        public float DefaultMarkRadius;
        public List<MarkController> Goals { get { return Objects.Cast<MarkController>().ToList(); } }
        #region Monobehaviour
        void OnEnable()
        {
            MarkPrefab = MarkPrefab ?? ExperimentAssetHolder.Instance.FindPrefab("Mark");
        }
        #endregion
        #region Public API
        public List<MarkController> InstantiateObjectsOnCircleCircumference(int number, int[] positions = null, float radius = 20, Vector3 center = default(Vector3), string MarkType = "")
        {
            //validations
            if (positions != null)
            {
                if (positions.Max() > number - 1)
                {
                    Debug.Log("Cannot instantiate goals on positions higher than number of goals");
                    return null;
                }
            }
            else
            {
                positions = Enumerable.Range(0, number).ToArray();
            }
            Objects.Clear();
            foreach(var i in positions)
            {
                GameObject mark = Instantiate(MarkPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0)) as GameObject;
                if (mark != null)
                {
                    mark.transform.SetParent(gameObject.transform);
                    mark.SetActive(true);
                    MarkController markController = mark.GetComponent<MarkController>();
                    Objects.Add(markController);
                    if (!string.IsNullOrEmpty(MarkType)) markController.SetType(MarkType);
                }
                else
                {
                    Debug.Log("Couldnt instantiate for some reason!");
                    return null;
                }
            }
            MoveObjectsCircumference(number, positions, radius, center);
            return Objects.Cast<MarkController>().ToList();
        }
        #region Manipulation
        public void SetHight(float height)
        {
            foreach (MarkController Mark in Objects)
            {
                Vector3 currentPosition = Mark.gameObject.transform.position;
                Mark.transform.position = new Vector3(currentPosition.x, height, currentPosition.z);
            }
        }
        #endregion
        public static MarkManager InstantiateMarkManager(string name = "Mark manager")
        {
            var go = new GameObject();
            go.transform.name = name;
            var markManager = go.AddComponent<MarkManager>();
            return markManager;
        }
        #endregion
    }
}
	
