using Microsoft.Xna.Framework;


namespace Caveworks
{
    public static class Globals // all global variables
    {
        public static IScene ActiveScene { get; set; }// witch screen should be loaded
        public static bool ExistsSaveFile { get; set; } = false; // is there a world to load
        public static float GlobalVolume { get; set; } = 0.5f; // global game volume
        public static Vector4 UIButtonColor { get; set; } = new Vector4(0, 0.6f, 1, 1); // default color
        public static Vector4 UITextBoxColor { get; set; } = new Vector4(0, 0.8f, 1, 1); // default color
    }
}
