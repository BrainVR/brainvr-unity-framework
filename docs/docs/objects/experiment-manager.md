Experiment manager takes care for the loading and control of the expeirment flow. You can load experiment through `ExperimentLoader`, but Experiment manager offers more to the pont clear loading experience.

Experiment manager needs to be in the scene as it keeps track of hte [Experiment](experiment/experiment) active script and allows certain level of control over it. you shouldn't call experiment directly, just refere to expeirment manager to do all necessary changes and switches ot the Experiment flow.

## Functions
### StartExperiment
`public void StartExperiment()`

If the experiment is not runnign, it starts hte expeirment.

### StopExpeirment
`public void StopExperiment()`

Stops the expeirmenting if the experiment is running.

### RestartExperiment
`public void RestartExperiment()`
Calls `StopExpeirment` and then `StartExpoeirment`, but it is broken after second iteration. Not sure why.

###  SwitchExperimentState
`public void SwitchExperimentState()`

Does absolutely nothing at this point.

###SetTrial
`public void SetTrial(int i)`

Sets trial to the designated number. Finishes trial if running. Runs Tiral setup after setting the trial.
