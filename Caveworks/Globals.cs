using Microsoft.Xna.Framework;

namespace Caveworks
{
    public static class Globals // all global variables
    {
        private static IScene activeScene;// witch screen should be loaded, 0 - main menu

        private static bool existsSaveFile = false; // is there a game to load

        private static float globalVolume = 1.0f; // global game volume

        public static IScene GetActiveScene()
        {
            return activeScene;
        }

        public static void SetActiveScene(IScene scene)
        {
            activeScene = scene;
        }

        public static bool ExistsSaveFile()
        {
            return existsSaveFile;
        }

        public static float GetGlobalVolume()
        {
            return globalVolume;
        }
    }
}
