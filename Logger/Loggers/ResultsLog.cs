using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using BrainVR.UnityFramework.Experiments.Helpers;
using Newtonsoft.Json;

namespace BrainVR.UnityLogger
{
    public class ResultData : Attribute { }
    public class TestData : Attribute { }
    public class ResultsLog : ScriptableObject
    {
        [JsonIgnore]
        public string ParticipantId;
        public string ExperimentName = "TEST";
        [JsonIgnore]
        public string CustomData;
        public ResultsLog(string participantId)
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
            var log = new Log(ParticipantId, "results_" + ExperimentName, MasterLog.Instance.CreationTimestamp);
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
            var subList = nums.Where((x, i) => indices.Contains(i)).ToList();
            return Average(subList);
        }
        public float Average(List<float> nums, List<int[]> indices)
        {
            var finishedIndices = indices[0];
            for (var i = 1; i < indices.Count; i++)
            {
                finishedIndices = finishedIndices.Intersect(indices[i]).ToArray();
            }
            return Average(nums, finishedIndices);
        }
        //Counting of booleans
        public int SumBool(List<bool> bools)
        {
            return bools.Count(s => s);
        }
        public int SumBool(List<bool> bools, int[] indices)
        {
            var subList = bools.Where((x, i) => indices.Contains(i)).ToList();
            return SumBool(subList);
        }
        public int SumBool(List<bool> bools, List<int[]> indices)
        {
            var finishedIndices = indices[0];
            for (var i = 1; i < indices.Count; i++)
            {
                finishedIndices = finishedIndices.Intersect(indices[i]).ToArray();
            }
            return SumBool(bools, finishedIndices);
        }
        #endregion    }

    }
    // Allows list interation and searching for inidces of a certain value
    public static class EM
    {
        public static int[] FindAllIndexof<T>(this IEnumerable<T> values, T val)
        {
            return values.Select((b, i) => object.Equals(b, val) ? i : -1).Where(i => i != -1).ToArray();
        }
    }
}

