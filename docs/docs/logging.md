Logging in the framework is handled by the brainvr-unity-logger package. There are several forced and several optional logging modules that you can implement in your experiment.

Every experiment is output in a simillar manner, with specific parts distinguished by *** and ended by ---.

!!! Example

    \*\*\*SESSION HEADER\*\*\*
    
    {
        "Participant": "NEO",      
        "Timestamp": "16-01-20-13-09-2017"  
    }

    ---SESSION HEADER---

Anything below these sections is the log itself, which 

Logs are flagged with the participant id and time at which they were created. They also get saved in a folder, which is flagged with the id and the date at which they were created. This allows all logs from the same participant and the same day to be grouped together, but also keep the progressive informaation if multiple tests were made during that particpar day.

# Main logging classses
## Master log
Responsible for handeling and keeping track of all the logs that are happening. Turning logging on and off, instantiating logs etc. should be done through Master log. 

### Functions
Variable          | Purpose       
----------------- | ------------- 
[Instantiate](logging/master-log.md#instantiate) | Sets the obejct rotation with the Quaternion.
[StartLogging](logging/master-log.md#start logging)  | If the object is rotating, stops the rotation.
[StopLogging](logging/master-log.md#stop logging)  | If the object is rotating, stops the rotation.
[CloseLogs](logging/master-log.md#close logs)  | If the object is rotating, stops the rotation.

## Player log
Player log logs player information as is sent to him from IPlayerController inferface.

### IPlayerController
this interface forces each player controller to implement custom header and custom player informatino to log. This is because some VR or 2D games might require different infomration to be passed and logged, so the reponsibility to log it properly is based on the player controller that is used. 

Property | Type | Purpose
-------- | ---- | -------
HeaderLine | string | what will be the header line of the log
PlayerInformation | List<string\> | List of strings of values corresponsing to the HeaderLine

## Experiment info log

## Test log

# Optional logging classes
## Results