
namespace ConsoleGame.Infrastructure
{
    internal class GameStatistics
    {
        public static int RecourssenGathered;
        public TimeSpan TimePlayed = new TimeSpan(0, 0, 0);
        public int BuildingsPlaced;
        public int CellsMoved;
    }
}
