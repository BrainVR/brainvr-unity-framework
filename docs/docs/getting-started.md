One of the major problems in deciding how to implement this framework was packaging. Because I wanted this to be self serving and ready to start, I included many Standard Assets as well and modified project settings. This ultimately prevents the project from being effectively packaged into .unity package. So you have to donwload it as it is and open it as a new project. If you are familiar with forking, that is probably the safest and easiest solution.

This framework uses submodule for the unity-logger. It might change in the future, but so far you have to initialize and update it. 

So the full shell for cloning this repo would be:

```sh
git clone git@github.com:BrainVRbrainvr-unity-framework.git
git submodule init
git submodule update
```

When you are ready, open the unity and you can start modding.

## Preparing the scene
You can take a look into the Demo scene, to get the feeling of how the scene shoudl/could be organized. 

### Necessary objects

- [Player controller](objects.md#player) needs to be in the scene. Player controller is just an abstract class you need to extend. The framwork comes with RigidBodyPlayerController which has a prefab in the Brainvr/Player folder. If not using VR, you can safely go with that.

- [Experiment manager](objects.md#experiement manager) Experiment manager takes care of settings loading and starting up the scene.

- [Log manager](objects.md#log manager) Takes care of all the logging. It resides in the Libraries folder under the brain-vr-logger. 

- [Input manager](objects.md#input manager) loggs and sends events about particular key presses. It's importance is mainly in conjunction with the logger.

- [Crosshair](objects.md#crosshair) needs to be present, even if you don't want to show it. It deals with raycasting as well, which your experiment might almost certainly need.

- [Menu](objects.md#menu) allows starting and stopping of experiment as well as returning to main menu.

### Optional objects

- [Info canvas](objects.md#info canvas) deals with displaying information on the screen. You don't have to include it if your game does not need it.

- [Navigation manager](objects.md#navigation manager) allows to show gps like navigation on the floor or other means of navigating player.

- [Beeper manager](objects.md#beeper manager) allows to play sounds during the experiment.

- [Goal manager](objects.md#goal manager) allows controlling, placement, visibility settings etc. of target destinations. Each manager controls assigned objects with a goal controller script attached.

- [Mark manager](objects.md#mark manager) similar to goal manager, but it doesn't implement any functions related to trigger entering etc.

- [Arduino controller](objects.md#experiemgoalent manager) is useful if you need to send synchronisation to other devices, let's say to the EEG or EKG recordings. It is based on the [Arduino iEEG controller](https://github.com/hejtmy/iEEGArduinoConnector/tree/NET_3.5) built dll.

After you have these objects in the scene, you need to implement your new experiment. Read more on the coding part [later](new-experiment.md). Let's work with the demo experiment so far.

## Managing the experiment in scene
So now you have all the objects ready, it is time to put the expriment files and settings where they belong and test out, whether we can run this thing.

### Managing settings
It is important to understand how the experiment loading works because it is not fully straightworward. Expeirment settings are either parsed from json into [SettingsHolder](objects.md#Settings holder) class or they can be inserted in the scene itself, for faster development. In the built game, only parsed settings are allowed.

The parsing happens in the MainMenu scene, so if you are developping the paradigm from the game, you need to serialise your settings into asset and use that from within the scene. This allows you faster editing and testgin as well.

In the experiment manager, you have parrented Settings object. This object is either empty, or holds DemoExperimentSettings asset. If it doens't, search for it and move it there. 

This shoudl be all :) 

### Loading proper experiment
When you play the scene, the experiment Manager will try and parse the settings and set the proper experiment with `LoadExperiment` function. This function searches the asset/json for the experimentName parameter and then searches the assembly for a class of that name. If it finds it, it instantiates Experiment GameOObject with the Expeirment component attached.

### Managing experiment
Experiment does load automatically throught the manager object, but it doesn't start automatically. That is what the Menu item is for. When the experiment is loaded, Menu binds it's buttons to Experiment manager functions - start, stop etc. You can enter the menu and click on Start the experiment. If it runs, great :) You can move around and then escape and stop it. That will end the experiment and save the logs.

## Logging
You can now take a look into the `logs/` folder that resides above assets. It should contain very brief log of player activity, information about the PC settings, and lastly the exprimental procedure. If logs aren't created, check that the asset witht he settings has `ShouldLog` property set to true.

## Next steps
Now you should be able to implement your first experiment. Go to [new experiment](new-experiment.md) to start.