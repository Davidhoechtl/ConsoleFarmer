
namespace ConsoleGame.Achievement
{
    using ConsoleGame.StateManagment;
    using System.Text.Json.Serialization;

    [JsonConverter(typeof(QuestJsonConverter))]
    internal abstract class Quest
    {
        public abstract QuestType Type { get; }

        public string Description { get; init; }
        public int Reward { get; init; }
        public int Index { get; init; }
        public int Progress { get; protected set; }
        public bool Completed { get; set; }
        public Quest(string description, int reward, int index)
        {
            Description = description;
            Reward = reward;
            Index = index;
        }

        public abstract bool UpdateProgress(GameState state);

        public abstract override string ToString();
    }
}
