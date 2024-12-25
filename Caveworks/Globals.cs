using Microsoft.Xna.Framework;

namespace Caveworks
{
    public static class Globals // all global variables
    {
        private static IScene activeScene;// witch screen should be loaded, 0 - main menu

        private static bool existsSaveFile = false; // is there a game to load

        private static float globalVolume = 0.5f; // global game volume

        private static Vector4 UIButtonColor = new Vector4(0, 0.6f, 1, 1);

        private static Vector4 UITextBoxColor = new Vector4(0, 0.8f, 1, 1);

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

        public static void SetGlobalVolume(float volume)
        {
            globalVolume = volume;
        }

        public static Vector4 GetUIButtonColor()
        {
            return UIButtonColor;
        }

        public static Vector4 GetUITextBoxColor()
        {
            return UITextBoxColor;
        }
    }
}
