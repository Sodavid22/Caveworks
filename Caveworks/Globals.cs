using Caveworks.UiElements;
using Microsoft.Xna.Framework;

namespace Caveworks
{
    public static class Globals // all global variables
    {
        public static GameScreen activeScreen;// witch screen should be loaded, 0 - main menu

        public static bool activeGame = false; // is there a game to load

        public static float gameVolume = 1.0f; // global game volume
    }
}
