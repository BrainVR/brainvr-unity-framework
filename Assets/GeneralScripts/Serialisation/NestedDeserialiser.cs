using System.Linq;
using Newtonsoft.Json.Linq;
/* 
as found here https://stackoverflow.com/questions/6416017/json-net-deserializing-nested-dictionaries/6417753
*/
namespace Assets.GeneralScripts.Serialisation
{
    public static class NestedDeserialiser
    {
        public static object Deserialize(string json)
        {
            return ToObject(JToken.Parse(json));
        }

        private static object ToObject(JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    return token.Children<JProperty>().ToDictionary(prop => prop.Name, prop => ToObject(prop.Value));
                case JTokenType.Array:
                    return (from JToken value in token.Values<object>() select ToObject(value)).ToList();
                default:
                    return ((JValue)token).Value;
            }
        }
    }
}

