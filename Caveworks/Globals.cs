using Microsoft.Xna.Framework;


namespace Caveworks
{
    public static class Globals // all global variables
    {
        public static IScene ActiveScene { get; set; }// witch screen should be loaded
        public static float GlobalVolume { get; set; } = 0.5f; // global game volume
        public static Vector4 UIButtonColor { get; set; } = new Vector4(0, 0.6f, 1, 1); // default color
        public static Vector4 UITextBoxColor { get; set; } = new Vector4(0, 0.8f, 1, 1); // default color
        public static Vector4 InventoryButtonColor { get; set; } = new Vector4(0.4f, 0.5f, 0.55f, 1); // default color
        public static Vector4 InventoryBoxColor { get; set; } = new Vector4(0.3f, 0.4f, 0.45f, 1); // default color
        public static World World { get; set; } = null;
        public static int LightDistance { get; set; } = 24;
        public static bool ExistsSave {  get; set; } = false;
    }
}
