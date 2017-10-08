using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Assets.GeneralScripts.Serialisation
{
    public static class JSONDeserialiser {

        public static string GetField(string filePath, string fieldName)
        {
            var text = File.ReadAllText(filePath);
            JObject o = JObject.Parse(text);
            var value = (string)o[fieldName];
            return value;
        }

        public static string GetArrayField(string filePath, string fieldName, int index)
        {
            var arrays = GetArrayField(filePath, fieldName);
            return arrays[index];
        }

        public static List<string> GetArrayField(string filePath, string fieldName)
        {
            var text = File.ReadAllText(filePath);
            JObject o = JObject.Parse(text);
            JArray arrays = (JArray)o[fieldName];
            return arrays.Select(arr => arr.ToString()).ToList();
        }
    }
}
