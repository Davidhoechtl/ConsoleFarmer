
namespace ConsoleGame.Achievement
{
    using ConsoleGame.StateManagment;

    internal class RessourceHoldingQuest : Quest
    {
        public int NeededRessourceGathered { get; init; }
        public override QuestType Type => QuestType.RessourceHolding;

        public RessourceHoldingQuest(string descritpion, int reward, int difficulty, int neededRessourceGathered)
            :base(descritpion, reward, difficulty)
        {
            NeededRessourceGathered = neededRessourceGathered;
        }

        public override bool UpdateProgress(GameState state)
        {
            Progress = state.RessourceCount;

            if (state.RessourceCount >= NeededRessourceGathered)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"{Description} ({Progress}/{NeededRessourceGathered})";
        }
    }
}
