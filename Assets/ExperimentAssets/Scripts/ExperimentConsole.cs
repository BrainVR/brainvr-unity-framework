using Assets.ExperimentAssets.Scripts;
using Assets.ExperimentAssets.Scripts.Player;
using DevConsole;
using UnityEngine;

public class ExperimentConsole : MonoBehaviour {

    void Start()
    {
        Console.AddCommand(new ActionCommand(ExperimentManager.Instance.StartExperiment, "Experiment", "start", "Starts the experiment if closed"));
        Console.AddCommand(new ActionCommand(ExperimentManager.Instance.StopExperiment, "Experiment", "stop", "Stops the experiment if running"));
        Console.AddCommand(new ActionCommand(ExperimentManager.Instance.RestartExperiment, "Experiment", "restart", "Restarts experiment from the first trial"));

        Console.AddCommand(new ActionCommand<int>(ExperimentManager.Instance.SetTrial, "Trial", "set", "Set trial to specific iteration"));

        Console.AddCommand(new ActionCommand(PlayerController.Instance.MoveToCenter, "Player", "center", "Move player to the center"));

    }
}
