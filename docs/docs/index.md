## What it is?

BrainVR unity framework is a collection of unity objects, wrappers and placeholders, that should speed up development of unity based research paradigms. Its most beneficial addition is unified logging solution that has appropriate loading scripts in the [brainvr-logger-reader](https://github.com/BrainVR/brainvr-unity-logger-reader) thata are being kept updated with the changes done in the framework. The framewok also allows paring of json based settings, which makes it possible to build experiments irrespective of particular initiasiation values and removes the necessity to randomize things in Unity, which is not always a good idea.


### Main Features

- Unified logging solution for noting participant's behaviour within the experiment and ready to use scripts in R to load these logs

- JSON parsing modules for loading and serialising custom settings for each experiment, which allows modifying the paradigm without necessity to code new script for each different setting. 

- Unity controllers for many objects to allow simple and fast implementation of player triggered actions, such as moving objects, playing sounds, changing materials etc.

- Unified Experiment framework designed to conceptualize adn enfforce structure to most scientific trial based experiments.

- Ugly but functional Main menu to load experiments through and simple in-game menu.

- Canvas for displaying info throughout the experiment as well as tutorial/training information etc.

## Is it user friendly?
Unlike other solutions, this framework is not particularly user friendly. It doesnt provide many components to dragg and dropp and requires understanding of both Unity and C# to an intermediate level. You should be familiar with all that Unity offers, such as basic transform and material modifications, sounds and navigation, as well as C# concepts such as interfaces, abstract classes and delegates with events. 

Nevertheless, if you don't feel comfortable but would like trying it out anyway, there are already implemented projects that might help you through your first paradigm. And you can always contact us if you need any help.

## Why and when to use it?
If you are somewhat fluent in C# and would like to work with something that is going to speed up research development in respect to logging standards and settings parsing, this framework shoudl alleviate a lot of work. It will have some learning curve to it, but many of the troublesome parts of paradigm development have been taken care of :)

## Quick start
This framework uses submodule for the unity logger. It might change in the future, but so far you have to initialize and update it.

```sh
git clone git@github.com:BrainVRbrainvr-unity-framework.git
git submodule init
git submodule update
```

## Who are we?
This framework has been coded by [Lukáš Hejtmánek](hejtmy.com) while working at the National Institute of Mental Health and Inistitute of Physiology, ASCR. Some parts were developed in collaboration with researchers at the Center for Neuroscince at University of California, Davis.

## Acknowledgements
