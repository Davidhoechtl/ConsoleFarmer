
namespace ConsoleGame.Buildings
{
    internal interface IHasRessourceGainOverTime
    {
        DateTime LastTick { get; }
        int TimeToNextTickInSeconds { get; }
        int CurrentGainPerTick { get; }
    }
}
