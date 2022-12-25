
namespace ConsoleGame.Sounds
{
    using System.Media;

    /// <summary>
    /// Manage to Play specific sounds
    /// </summary>
    internal static class SoundManager
    {
        public static void PlayMenuItemSelected()
        {
            soundPlayer.SoundLocation = SoundFilePaths.MenutItemSelected;
            soundPlayer.Play();
        }

        public static void PlayRessourceCollected()
        {
            soundPlayer.SoundLocation = SoundFilePaths.RessourceCollected;
            soundPlayer.Play();
        }

        public static void PlayCommandEntered()
        {
            soundPlayer.SoundLocation = SoundFilePaths.CommandEntered;
            soundPlayer.Play();
        }        
        
        public static void PlayCommandEnteredFailed()
        {
            soundPlayer.SoundLocation = SoundFilePaths.CommandEnteredFailed;
            soundPlayer.Play();
        }

        private static readonly SoundPlayer soundPlayer = new SoundPlayer();
    }
}
