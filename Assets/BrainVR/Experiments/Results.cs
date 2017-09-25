using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using BrainVR.UnityLogger;

namespace BrainVR.UnityFramework.Experiments
{
    public class ResultData : Attribute{}
    public class TestData : Attribute { }
    public class Results : ScriptableObject
    {
        [JsonIgnore]
        public string ParticipantId;
        public string ExperimentName = "TEST";
        [JsonIgnore]
        public string CustomData;
        public Results(string participantId)
        {
            ParticipantId = participantId;
        }
        #region Serialisation
        public string Serialise()
        {
            return JsonConvert.SerializeObject(this, SerialisationConstants.SerialisationSettings());
        }
        public string SerialiseResults()
        {
            var settings = SerialisationConstants.SerialisationSettings(new AttributeContractResolver<ResultData>());
            return JsonConvert.SerializeObject(this, settings);
        }
        public string SerialiseData()
        {
            var settings = SerialisationConstants.SerialisationSettings(new AttributeContractResolver<TestData>());
            return JsonConvert.SerializeObject(this, settings);
        }
        #endregion
        #region Manipulation
        public void AddData(string newline)
        {
            CustomData += newline + "/n";
        }
        #endregion
        #region Saving
        public void Save()
        {
            var log = new Log(ParticipantId, "results_" + ExperimentName);
            log.WriteLine("***RESULTS***");
            log.WriteLine(SerialiseResults());
            log.WriteLine("---RESULTS---");
            log.WriteLine("***CUSTOM***");
            log.WriteLine(FormatData());
            log.WriteLine("---CUSTOM---");
            log.WriteLine("***DATA***");
            log.WriteLine(SerialiseData());
            log.WriteLine("---DATA---");
            log.Close();
        }
        protected virtual string FormatData()
        {
            return CustomData;
        }
        #endregion
        #region Statistics
        public float Average(List<float> nums)
        {
            return nums.Count > 0 ? nums.Average() : 0f;
        }
        public float Average(List<float> nums, int[] indices)
        {
            var subList = nums.Where((x, i) => indices.Contains(i));
            return nums.Count > 0 ? subList.Average() : 0f;
        }
        public int SumBool(List<bool> bools)
        {
            return bools.Count(s => s);
        }
        public int SumBool(List<bool> bools, int[] indices)
        {
            var subList = bools.Where((x, i) => indices.Contains(i));
            return subList.Count(s => s);
        }
        #endregion
    }

}
// Allows list interation and searching for inidces of a certain value
public static class EM
{
    public static int[] FindAllIndexof<T>(this IEnumerable<T> values, T val)
    {
        return values.Select((b, i) => object.Equals(b, val) ? i : -1).Where(i => i != -1).ToArray();
    }
}

