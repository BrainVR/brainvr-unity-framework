using Assets.ExperimentAssets.Experiments;
using Assets.ExperimentAssets.Player;
using DevConsole;
using UnityEngine;

namespace Assets.ExperimentAssets.Console
{
    public class ExperimentConsole : MonoBehaviour
    {

        void Start()
        {
            DevConsole.Console.AddCommand(new ActionCommand(ExperimentManager.Instance.StartExperiment, "Experiment", "start", "Starts the experiment if closed"));
            DevConsole.Console.AddCommand(new ActionCommand(ExperimentManager.Instance.StopExperiment, "Experiment", "stop", "Stops the experiment if running"));
            DevConsole.Console.AddCommand(new ActionCommand(ExperimentManager.Instance.RestartExperiment, "Experiment", "restart", "Restarts experiment from the first trial"));

            DevConsole.Console.AddCommand(new ActionCommand<int>(ExperimentManager.Instance.SetTrial, "Trial", "set", "Set trial to specific iteration"));

            DevConsole.Console.AddCommand(new ActionCommand(PlayerController.Instance.MoveToCenter, "Player", "center", "Move player to the center"));

        }
    }
}
