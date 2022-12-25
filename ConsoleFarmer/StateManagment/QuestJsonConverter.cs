using ConsoleGame.Achievement;
using ConsoleGame.Buildings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace ConsoleGame.StateManagment
{
    internal class QuestConcreteClassConverter : DefaultContractResolver
    {
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (typeof(Quest).IsAssignableFrom(objectType) && !objectType.IsAbstract)
                return null; // pretend TableSortRuleConvert is not specified (thus avoiding a stack overflow)
            return base.ResolveContractConverter(objectType);
        }
    }

    internal class QuestJsonConverter : JsonConverter
    {
        static JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings() { ContractResolver = new QuestConcreteClassConverter() };

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Quest));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            switch ((QuestType)jo["Type"].Value<int>())
            {
                case QuestType.Building:
                    return JsonConvert.DeserializeObject<BuildingQuest>(jo.ToString(), SpecifiedSubclassConversion);
                case QuestType.Ressource:
                    return JsonConvert.DeserializeObject<RessourceQuest>(jo.ToString(), SpecifiedSubclassConversion);
                case QuestType.RessourceHolding:
                    return JsonConvert.DeserializeObject<RessourceHoldingQuest>(jo.ToString(), SpecifiedSubclassConversion);
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
