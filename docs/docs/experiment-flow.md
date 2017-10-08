
Each experiment is separated to following phases

1. ExperimentInitialise
2. ExperimentSetup
3. ExperimentStart
4. ExperimentFinish
5. ExperimentClose

Each stage has OnExperiment[Setup/Start] and AfterExperiment[Setup/Start..] Functions interface that needs to be implemented.

# Flow of Trial

Each Trial is set to following stages.

1. TrialSetup
2. TrialStart
3. TrialFinish
4. TrialClose

Each stage has OnTrial[Setup/Start] and AfterTrial[Setup/Start ..] Functions interface that needs to be implemented.

# Required functions
```{c#}
//Doesnt need to be called from outside the experiment - its just to make things clearer in the code
public abstract string ExperimentHeaderLog();
//happends when the experiment is started - non monobehaviour logic
protected abstract void OnExperimentInitialise();
//sets up the pieces
protected abstract void OnExperimentSetup();
protected abstract void AfterExperimentSetup();
protected abstract void OnExperimentStart();
protected abstract void AfterExperimentStart();
//called when new trial is prepaired
protected abstract void OnTrialSetup();
//called after the new trial is prepaired
protected abstract void AfterTrialSetup();
//called when the trial is actually started
protected abstract void OnTrialStart();
//called after everything been instantiated
protected abstract void AfterTrialStart();
//when the task has been successfully finished
protected abstract void OnTrialFinished();
//called after the task has been successfully finished
protected abstract void AfterTrialFinished();
//called after Trial has been finished and close - cleanup
protected abstract void OnTrialClosed();
//called after Trial has been finished and close - cleanup
protected abstract void AfterTrialClosed();
//called automatically after trialCleanup
protected abstract bool CheckForEnd();
//when trials end
protected abstract void OnExperimentFinished();
protected abstract void AfterExperimentFinished();
//after OnExperiemntFinished is called
protected abstract void OnExperimentClosed();
protected abstract void AfterExperimentClosed();
```