When awake, it iterates whourgh all its children and binds controller names so that it knows what it can play and what not.

## Monobehaviour
### Awake
Iterates through all the children that have [BeeperController](beeper-controller) script attached and get their names. It saves the name into [BeeperControllers](#beeper-controllers) variable.

## Variables
### BeeperControllers
`public Dictionary<string, BeeperController> BeeperControllers`

List of all the [BeeperController](beeper-controller)  as well as their names as appears in [BeeperName](beeper-controller.md#beeper-name). 

## Functions
### Play
`void Play(string beeperName)`

Tries to get a beepre controller of a certain name. If that controller exists, it plays one shot of that particular beeper. If not, returns `KeyNotFoundException`.