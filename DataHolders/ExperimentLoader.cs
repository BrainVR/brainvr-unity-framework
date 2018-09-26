﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BrainVR.UnityFramework.Experiment;
using BrainVR.UnityFramework.Helpers;
using UnityEngine;

namespace BrainVR.UnityFramework.DataHolders
{
    public static class ExperimentLoader
    {
        public static Experiment.BaseExperiment CreateExperimentGO(string expName, ExperimentSettings settings = null)
        {
            var expGO = new GameObject();
            expGO.transform.name = expName;
            expGO.transform.parent = ExperimentManager.Instance.transform;
            var experimentClass = TypeHelper.GetTypeByName(expName);
            if (experimentClass == null)
            {
                Debug.Log("BaseExperiment of name " + expName + "does not exist");
                return null;
            }
            var exp = expGO.AddComponent(experimentClass) as BaseExperiment;
            if (exp == null) return null;
            exp.AddSettings(settings);
            //this is here so that logs are created properly
            exp.Name = expName;
            return exp;
        }
        public static ExperimentSettings PopulateExperimentSettings(string expName, string path)
        {
            var type = ExperimentSettingsType(expName);
            if (type == null) return null;
            var settings = ScriptableObject.CreateInstance(type) as ExperimentSettings;
            // ReSharper disable once PossibleNullReferenceException
            settings.DeserialiseSettings(path);
            return settings;
        }
        #region Helpers
        private static Type ExperimentSettingsType(string expName)
        {
            var settingsName = expName + "Settings";
            var type = TypeHelper.GetTypeByName(settingsName);
            if(type == null) Debug.Log("There is no Class of name" + settingsName + ". You need to add it to the assembly");
            return type;
        }
        //http://stackoverflow.com/questions/4943817/mapping-object-to-dictionary-and-vice-versa
        public static T PopulateClassDictionary<T>(this IDictionary<string, object> source) where T : class, new()
        {
            var someObject = new T();
            var someObjectType = someObject.GetType();

            foreach (KeyValuePair<string, object> item in source)
            {
                someObjectType.GetProperty(item.Key).SetValue(someObject, item.Value, null);
            }

            return someObject;
        }
        //http://stackoverflow.com/questions/9854900/instantiate-a-class-from-its-textual-name
        private static object MagicallyCreateInstance(string className)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var type = assembly.GetTypes()
                .First(t => t.Name == className);

            return Activator.CreateInstance(type);
        }
#endregion
    }
}
