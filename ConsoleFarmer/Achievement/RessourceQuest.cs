
namespace ConsoleGame.Achievement
{
    using ConsoleGame.Infrastructure;
    using ConsoleGame.StateManagment;

    internal class RessourceQuest : Quest
    {
        public int NeededRessourceGathered { get; init; }
        public override QuestType Type => QuestType.Ressource;

        public RessourceQuest(string descritpion, int reward, int difficulty, int neededRessourceGathered)
            : base(descritpion, reward, difficulty)
        {
            NeededRessourceGathered = neededRessourceGathered;
        }


        public override bool UpdateProgress(GameState state)
        {
            Progress = GameStatistics.RecourssenGathered;

            if (GameStatistics.RecourssenGathered >= NeededRessourceGathered)
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
