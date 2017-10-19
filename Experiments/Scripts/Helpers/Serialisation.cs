using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BrainVR.UnityFramework.Experiments.Helpers
{
    public class SerialisationConstants
    {
        public static readonly List<string> UnwantedFields = new List<string> { "hideFlags", "name" };

        public static JsonSerializerSettings SerialisationSettings()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                Formatting = Formatting.Indented,
                ContractResolver = new ShouldSerializeContractResolver()
            };
            return settings;
        }

        public static JsonSerializerSettings SerialisationSettings(DefaultContractResolver resolver)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                Formatting = Formatting.Indented,
                ContractResolver = resolver
            };
            return settings;
        }
    }

    public class ShouldSerializeContractResolver : DefaultContractResolver
    {
        public static readonly ShouldSerializeContractResolver Instance = new ShouldSerializeContractResolver();

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);
            properties = properties.Where(p => !SerialisationConstants.UnwantedFields.Contains(p.PropertyName)).ToList();
            return properties;
        }
    }

    public class AttributeContractResolver<T> : DefaultContractResolver where T : Attribute
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var properties = type.GetProperties()
                .Where(x => x.GetCustomAttributes(true).Any(a => a.GetType() == typeof(T)))
                .Select(p => new JsonProperty
                {
                    PropertyName = p.Name,
                    PropertyType = p.PropertyType,
                    Readable = true,
                    Writable = true,
                    ValueProvider = CreateMemberValueProvider(p)
                }).ToList();

            var fields = type.GetFields()
                .Where(x => x.GetCustomAttributes(true).Any(a => a.GetType() == typeof(T)))
                .Select(p => new JsonProperty
                {
                    PropertyName = p.Name,
                    PropertyType = p.FieldType,
                    Readable = true,
                    Writable = true,
                    ValueProvider = CreateMemberValueProvider(p)
                }).ToList();

            properties.AddRange(fields);
            
            //removes _hideFlags and name
            properties = properties.Where(p => !SerialisationConstants.UnwantedFields.Contains(p.PropertyName)).ToList();
            return properties;
        }
    }
}
