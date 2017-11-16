using System.Collections.Generic;
using System.Linq;
using BrainVR.UnityFramework.DataHolders;
using BrainVR.UnityFramework.Scripts.Objects.Goals;
using UnityEngine;

namespace BrainVR.UnityFramework.Objects.Goals
{
    public class GoalManager : ArenaObjectManager<GoalManager>
    {
        public GameObject GoalPrefab;
        public List<GoalController> Goals { get { return Objects.Cast<GoalController>().ToList(); } }

        #region Monobehaviour stuff
        void OnEnable()
        {
            GoalPrefab = GoalPrefab ?? ExperimentAssetHolder.Instance.FindPrefab("Goal");
        }
        #endregion
        #region Public API
        #region Manipulation
        /// <summary>
        /// Returns a Goal controller script given by index
        /// </summary>
        /// <param name="i">index</param>
        /// <returns>GoalController</returns>
        public GoalController GetGoal(int i)
        {
            var obj = GetObject(i);
            return obj == null ? null : (GoalController)obj;
        }
        /// <summary>
        /// Resizes all the goals based on the multiplier characteristic
        /// </summary>
        /// <param name="multiplier"></param>
        public void ResizeGoals(float multiplier = 1f)
        {
            foreach (var goalController in Objects)
            {
                var currentScale = goalController.gameObject.transform.localScale;
                goalController.gameObject.transform.localScale = new Vector3(currentScale.x*multiplier, currentScale.y, currentScale.z*multiplier);
            }
        }
        #endregion
        /// <summary>
        /// Instantiates goals around the base of the arena
        /// </summary>
        /// <param name="number">How many goas shoudl be instantiated</param>
        /// <param name="positions">Indices of goals to actually be instantiated</param>
        /// <param name="radius">Radius of the arena</param>
        /// <param name="center">XYZ coordinates of the perfect center of the area</param>
        /// <returns></returns>
        public List<GoalController> InstantiateGoalsCircumference(int number, int[] positions = null, float radius = 15, Vector3 center = default(Vector3))
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
            foreach (var i in positions)
            {
                var goal = Instantiate(GoalPrefab, default(Vector3), default(Quaternion));
                if (goal == null) continue;
                goal.transform.SetParent(gameObject.transform);
                var goalController = goal.GetComponent<GoalController>();
                Objects.Add(goalController);
            }
            MoveObjectsCircumference(number, positions, radius, center);
            return Objects.Cast<GoalController>().ToList();
        }
        #endregion
    }
}
