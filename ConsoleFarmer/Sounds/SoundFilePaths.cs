
namespace ConsoleGame.Sounds
{
    internal static class SoundFilePaths
    {
        private static string WorkingDirectory = Directory.GetCurrentDirectory();
        public static string MenutItemSelected = Path.Combine(WorkingDirectory, "Sounds\\Main_Menu_Selected.wav");
        public static string RessourceCollected = Path.Combine(WorkingDirectory, "Sounds\\Ressource_Collected.wav");
        public static string CommandEntered = Path.Combine(WorkingDirectory, "Sounds\\Command_Entered.wav");
        public static string CommandEnteredFailed = Path.Combine(WorkingDirectory, "Sounds\\Command_Entered_Failed.wav");
    }
}
