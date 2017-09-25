using System.Collections.Generic;
using System.Linq;
using Assets.ExperimentAssets.DataHolders;
using UnityEngine;

namespace Assets.ExperimentAssets.Scripts.Objects.Goals
{
    public class GoalManager : AreaObjectManager<GoalManager>
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
        /// <param BeeperName="i">index</param>
        /// <returns>GoalController</returns>
        public GoalController GetGoal(int i)
        {
            if (i > Objects.Count)
            {
                Debug.Log("There isnt that many items");
                return null;
            }
            var goalController = Objects[i];
            return (GoalController)goalController;
        }
        /// <summary>
        /// Resizes all the goals based on the multiplier characteristic
        /// </summary>
        /// <param BeeperName="multiplier"></param>
        public void ResizeGoals(float multiplier = 1f)
        {
            foreach (var goalController in Objects)
            {
                Vector3 currentScale = goalController.gameObject.transform.localScale;
                goalController.gameObject.transform.localScale = new Vector3(currentScale.x*multiplier, currentScale.y, currentScale.z*multiplier);
            }
        }
        public void HideAll()
        {
            ShowAll(false);
        }
        public void ShowGoal(int i, bool bo = true)
        {
            var goal = GetGoal(i);
            if (goal != null) goal.Show(bo);
        }
        public void HideGoal(int i)
        {
            ShowGoal(i);
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
        public static GoalManager InstantiateGoalManager(string name = "Goal manager")
        {
            var go = new GameObject();
            go.transform.name = name;
            var goalManager = go.AddComponent<GoalManager>();
            return goalManager;
        }
        #endregion
    }
}
