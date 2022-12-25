

namespace ConsoleGame.Achievement
{
    using ConsoleGame.Buildings;
    using ConsoleGame.HUD;
    using ConsoleGame.StateManagment;

    internal class QuestManager
    {
        public readonly List<Quest> Quests = new List<Quest>();

        public QuestManager(List<Quest> quests)
        {
            Quests = quests;
        }

        public QuestManager()
        {
            CreateNewQuests();
        }

        public void CreateNewQuests()
        {
            // Building Quests
            Quests.Add(new BuildingQuest(
                descritpion: "Build 3 Factories.",
                reward: 10,
                difficulty: 1,
                buildingCount: 3,
                type: BuildingType.Factory)
            );
            Quests.Add(new BuildingQuest(
                descritpion: "Build 1 more Spawner.",
                reward: 15,
                difficulty: 4,
                buildingCount: 2,
                type: BuildingType.Spawner)
            );
            Quests.Add(new BuildingQuest(
                descritpion: "Build 2 Booster.",
                reward: 25,
                difficulty: 7,
                buildingCount: 2,
                type: BuildingType.Booster)
            );
            Quests.Add(new BuildingQuest(
                descritpion: "Build 5 Factories.",
                reward: 50,
                difficulty: 10,
                buildingCount: 3,
                type: BuildingType.Factory)
            );
            Quests.Add(new BuildingQuest(
                descritpion: "Build 1 Bank.",
                reward: 800,
                difficulty: 13,
                buildingCount: 3,
                type: BuildingType.Bank)
            );

            // Ressource Quests
            Quests.Add(new RessourceQuest(
                descritpion: "Collect a total of 50 Ressources.",
                reward: 5,
                difficulty: 2,
                neededRessourceGathered: 50)
            );
            Quests.Add(new RessourceQuest(
                descritpion: "Collect a total of 100 Ressources.",
                reward: 15,
                difficulty: 5,
                neededRessourceGathered: 100)
            );
            Quests.Add(new RessourceQuest(
                descritpion: "Collect a total of 200 Ressources.",
                reward: 20,
                difficulty: 8,
                neededRessourceGathered: 200)
            );
            Quests.Add(new RessourceQuest(
                descritpion: "Collect a total of 500 Ressources.",
                reward: 75,
                difficulty: 11,
                neededRessourceGathered: 500)
            );
            Quests.Add(new RessourceQuest(
                descritpion: "Collect a total of 1000 Ressources.",
                reward: 200,
                difficulty: 14,
                neededRessourceGathered: 1000)
            );
            Quests.Add(new RessourceQuest(
                descritpion: "Collect a total of 2500 Ressources.",
                reward: 500,
                difficulty: 17,
                neededRessourceGathered: 2500)
            );
            Quests.Add(new RessourceQuest(
                descritpion: "Collect a total of 5000 Ressources.",
                reward: 1000,
                difficulty: 20,
                neededRessourceGathered: 5000)
            );
            Quests.Add(new RessourceQuest(
                descritpion: "Collect a total of 10000 Ressources.",
                reward: 2000,
                difficulty: 23,
                neededRessourceGathered: 10000)
            );
            Quests.Add(new RessourceQuest(
                descritpion: "Collect a total of 50000 Ressources.",
                reward: 2000,
                difficulty: 26,
                neededRessourceGathered: 50000)
            );
            Quests.Add(new RessourceQuest(
                descritpion: "Collect a total of 100000 Ressources.",
                reward: 2000,
                difficulty: 29,
                neededRessourceGathered: 100000)
            );
            Quests.Add(new RessourceQuest(
                descritpion: "Int overflow (2147483647)",
                reward: 2000,
                difficulty: 32,
                neededRessourceGathered: int.MaxValue)
            );

            // Ressource holding Quests
            Quests.Add(new RessourceHoldingQuest(
                descritpion: "Hold 50 Ressources.",
                reward: 10,
                difficulty: 3,
                neededRessourceGathered: 50)
            );
            Quests.Add(new RessourceHoldingQuest(
                descritpion: "Hold 100 Ressources.",
                reward: 20,
                difficulty: 6,
                neededRessourceGathered: 100)
            );
            Quests.Add(new RessourceHoldingQuest(
                descritpion: "Hold 200 Ressources.",
                reward: 40,
                difficulty: 9,
                neededRessourceGathered: 200)
            );
            Quests.Add(new RessourceHoldingQuest(
                descritpion: "Hold 500 Ressources.",
                reward: 100,
                difficulty: 12,
                neededRessourceGathered: 500)
            );
            Quests.Add(new RessourceHoldingQuest(
                descritpion: "Hold 1000 Ressources.",
                reward: 200,
                difficulty: 15,
                neededRessourceGathered: 1000)
            );
            Quests.Add(new RessourceHoldingQuest(
                descritpion: "Hold 2500 Ressources.",
                reward: 500,
                difficulty: 18,
                neededRessourceGathered: 2500)
            );
            Quests.Add(new RessourceHoldingQuest(
                descritpion: "Hold 5000 Ressources.",
                reward: 1000,
                difficulty: 21,
                neededRessourceGathered: 5000)
            );
        }

        public void Update(GameState state)
        {
            foreach(Quest quest in Quests)
            {
                bool completed = quest.UpdateProgress(state);

                if (completed && !quest.Completed)
                {
                    state.player.RessourceCount += quest.Reward;
                    quest.Completed = true;
                    EventOutput.AddMessage($"Quest completed. Reward: {quest.Reward}!");
                }
            }

            state.Quests = Quests;
        }

        public Quest GetCurrentQuest()
        {
            return Quests.OrderBy(q => q.Index).FirstOrDefault(q => !q.Completed);
        }
    }
}
