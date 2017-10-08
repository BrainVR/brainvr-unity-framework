## Arena objects
Arena objects are game objects with implemented functions for positionings, rotations etc. Arena objects come with Arena object manager as well, which allows grouping of controllers of the same type and doing madifications groupwise (such as setting same height or colour). Specific implementations of arena object is the Goal object ([Goal controller](#goal controller)and [Goal manager](#goal manager)) and mark object ([Mark controller](#mark controller)and [Mark manager](#mark manager)), see those sections for info.

### Arena object controller
Arena obejct (AO) controller shoudl consist of a single empty game object with only controller script attached. It has `PossibleObjects` and `ActiveObject` parameters which are usually children obejcts, that AO can change "into". When creating an AO object, don't forget to add it's child object as well. Goal object prefab has one such example prepared.

#### Variables

Variable          | Purpose       
----------------- | ------------- 
ActiveObject      | active game object if any
PossibleObjects   | Contains an array of possible nested gameobjects to be played with with `SetType`. Can be set in unity
Position          | Vector3 position in world coordinates

#### Functions
Function          | Purpose       
----------------- | ------------- 
[Show](objects/arena-object.md#show) | Shows the object and enables all non trigger collider.
[Hide](objects/arena-object.md#hide) | If the ocject is visible, it becomes invisible
[SetRotation](objects/arena-object.md#arena-object-controller#setrotation) | Sets the obejct rotation with the Quaternion.
[StartRotation](objects/arena-object.md#startrotation)  | Starts rotating the object in a given vector.
[StopRotation](objects/arena-object.md#stoprotation)  | If the object is rotating, stops the rotation.
[SetSize](objects/arena-object.md#setsize)  | Sets the scale according to passed vector.
[SetColor](objects/arena-object.md#setcolor) | Sets the colour of the main material of the object. Saves the previous colour so you can call `ResetColor()` 
[ResetColor](objects/arena-object.md#resetcolor)  | resets the color to the original one.
[SetType](objects/arena-object.md#settype) | Sets the object to the one desired, based on its string name.
[Switch](objects/arena-object.md#switch)| Switches betwen visible and invisible state and returns the current state.

### Arena object manager
Arena object manager allows to manage and controll all assigned controllers of a given type. Common practice is to have a single manager for each type of object that you have in scene. 
By defualt, Arena object manager is a singleton, so you can't have multiple in the same scene. If you need more, you can override the base class or create multiple child classes of distinct names out of the single implementation of a single manager (eg. Start manager can have two children, Star1Manager and Start2Manager which inherit from the a base class)

#### Variables
Variable          | Purpose       
----------------- | ------------- 
Objects | Generic list of all the obejct of the fiven type that manager holds.


#### Functions
Function          | Purpose       
----------------- | ------------- 
[ShowAll](objects/arena-object-manager.md#showall) | Shows the object and enables all non trigger collider.
[HideAll](objects/arena-object-manager.md#hideall) | If the ocject is visible, it becomes invisible
[SetColor](objects/arena-object-manager.md#set-color) | Sets the colour of all assigned objects.
[ResetColor](objects/arena-object-manager.md#reset-color)  | resets the color to the original one.
[SetType](objects/arena-object-manager.md#set-type) | Sets all assigned objects to the one desired, based on its string name.

### Goal Object
Goal object adds some additional functionality to the arena object by adding event functionality. It inherits all the functionality from the [Arena Object]() and [].

#### Goal controller
##### Events

#### Goal manager

#### Mark manager

## Experiment manager
Experiemnt manager is reponsible for loading expeirment from the data and manipulating expeirment states.

### Functions
Function | Purpose
----------------- | ------------- 
[LoadExperiment](objects/experiment-manager.md#loadexperiment) | initialises the expeirment form name and possible settings file.
[StartExperiment](objects/experiment-manager.md#) | Starts the expeirment.
[StopExperiment](objects/experiment-manager.md#)| Stops the expeirment.
[RestartExperiment](objects/experiment-manager.md#) | Stops and then starts expperiment. buggy ATM.
[SwitchExperimentState](objects/experiment-manager.md#) | Doens't do anything at this point.
[SetTrial](objects/experiment-manager.md#) | Sets the trial to designated number.

## Player controllers
PLayer controllers build upon the player controller and IPlayerController class to proovide bothe control as well as loggin informiaton necessary for conprehensive logs across differnet modalities of play (PC. VR, mobile etc.). Some functiuon needs to be implemented in order ot create sa new player log.

### Variables
Variable          | Purpose       
----------------- | ------------- 
Objects | Generic list of all the obejct of the fiven type that manager holds.

### Functions
Function          | Purpose       
----------------- | ------------- 
[MoveToCenter](objects/player-controller.md#movetocenter) | Generic list of all the obejct of the fiven type that manager holds.


### Rigidbody player controller


## Beeper
Beepers are designed to simply play sounds when certain events happen within the game. Beeper objects have beeper manager and beeper controlers.

### Beeper Manager
[BeeperManager](objects/beeper-manager) takes care of cntrolling all attached controllers and plays souds based on string designation

#### Variables
Variable          | Function       
----------------- | ------------- 
[BeeperControlles](objects/beeper-manager.md#beepercontrolers) | list of all attached BeeperControllers.

#### Function
Function          | Function       
----------------- | ------------- 
[Play](objects/beeper-manager.md#beepercontrolers) | Plays one shot of the sounds in the controller

##

## Data holders

### Settings holder

## Input manager

## Console manager