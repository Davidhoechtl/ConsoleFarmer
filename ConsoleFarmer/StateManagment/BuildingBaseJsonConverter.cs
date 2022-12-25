using ConsoleGame.Buildings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace ConsoleGame.StateManagment
{
    internal class BaseSpecifiedConcreteClassConverter : DefaultContractResolver
    {
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (typeof(BuildingBase).IsAssignableFrom(objectType) && !objectType.IsAbstract)
                return null; // pretend TableSortRuleConvert is not specified (thus avoiding a stack overflow)
            return base.ResolveContractConverter(objectType);
        }
    }

    internal class BuildingBaseJsonConverter : JsonConverter
    {
        static JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings() { ContractResolver = new BaseSpecifiedConcreteClassConverter() };

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(BuildingBase));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            switch (jo["Name"].Value<string>())
            {
                case "Spawner":
                    return JsonConvert.DeserializeObject<RessourceSpawner>(jo.ToString(), SpecifiedSubclassConversion);
                case "Factory":
                    return JsonConvert.DeserializeObject<Factory>(jo.ToString(), SpecifiedSubclassConversion);
                case "Booster":
                    return JsonConvert.DeserializeObject<Booster>(jo.ToString(), SpecifiedSubclassConversion);
                case "Bank":
                    return JsonConvert.DeserializeObject<Bank>(jo.ToString(), SpecifiedSubclassConversion);
                default:
                    throw new Exception();
            }
            throw new NotImplementedException();
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException(); // won't be called because CanWrite returns false
        }
    }
}
