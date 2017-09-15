using Newtonsoft.Json;

namespace Assets.GeneralScripts.Serialisation
{
    public abstract class Serialisable<T>
    {
        public string Serialise()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                //TypeNameHandling = TypeNameHandling.All,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                Formatting = Formatting.Indented
            };
            string strJson = JsonConvert.SerializeObject(this, settings);
            return strJson;
        }

        public static T Deserialise(string path, T settingsClass)
        {
            var settings = JSONDeserialiser.GetArrayField(path, "Settings", 0);
            //TODO - checking for correct json
            JsonConvert.PopulateObject(settings, settingsClass);
            //TODO - throwing exception if wrong
            return settingsClass;
        }

        public void Deserialise(string path)
        {
            var settings = JSONDeserialiser.GetArrayField(path, "Settings", 0);
            //TODO - checking for correct json
            JsonConvert.PopulateObject(settings, this);
            //TODO - throwing exception if wrong
        }
    }
}
