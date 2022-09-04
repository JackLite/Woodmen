using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;

namespace Woodman.Utils
{
    public class UnityContractResolver : DefaultContractResolver
    {
        public override JsonContract ResolveContract(Type type)
        {
            var contract = base.CreateContract(type);
            if (type == typeof(Vector3))
            {
                contract.Converter = new Vector3Converter();
            }

            return contract;
        }
    }

    public class Vector3Converter : JsonConverter<Vector3>
    {
        public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
        {
            writer.WriteValue(JsonUtility.ToJson(value));
        }

        public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            return JsonUtility.FromJson<Vector3>((string)reader.Value);
        }
    }
}