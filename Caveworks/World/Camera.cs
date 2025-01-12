using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Caveworks
{
    public class Camera
    {
        public World World { get; private set; }
        public Vector2 Coordinates {  get; set; }
        public int Scale { get; set; }
        public Vector2 ScreenCenter { get; set; }

        public Camera(World world, Vector2 coordinates, int scale)
        {
            World = world;
            Coordinates = coordinates;
            Scale = scale;
        }

        public void Update()
        {
            ScreenCenter = new Vector2(GameWindow.WindowSize.X / 2, GameWindow.WindowSize.Y / 2);

            if(MyKeyboard.IsPressed(Keys.NumPad9))
            {
                Scale += 1;
            }
            if (MyKeyboard.IsPressed(Keys.NumPad3))
            {
                Scale -= 1;
            }
        }

        public void DrawWorld()
        {
            foreach (Chunk chunk in World.Chunks)
            {
                chunk.Draw(this);
            }
        }

        public Vector2 WrldToScrnCords(Vector2 worldCoordinates) // world coordinastes to screen coordinates
        {
            Vector2 screenCoordinates = new Vector2();
            screenCoordinates.X = ScreenCenter.X + ((worldCoordinates.X - Coordinates.X) * Scale);
            screenCoordinates.Y = ScreenCenter.Y + ((worldCoordinates.Y - Coordinates.Y) * Scale);
            return screenCoordinates;
        }
    }
}
