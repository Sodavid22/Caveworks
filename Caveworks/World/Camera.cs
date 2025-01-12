using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Caveworks
{
    public class Camera
    {
        public World World { get; private set; }
        public Vector2 Coordinates {  get; set; }
        public float Scale { get; set; }
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

            if(MyKeyboard.IsHeld(Keys.NumPad9))
            {
                Scale *= 1.01f;
            }
            if (MyKeyboard.IsHeld(Keys.NumPad3))
            {
                Scale *= 0.99f;
            }

            if (MyKeyboard.IsHeld(Keys.NumPad4))
            {
                Coordinates = new Vector2(Coordinates.X - 1f / Scale, Coordinates.Y);
            }
            if (MyKeyboard.IsHeld(Keys.NumPad6))
            {
                Coordinates = new Vector2(Coordinates.X + 1f / Scale, Coordinates.Y);
            }
            if (MyKeyboard.IsHeld(Keys.NumPad8))
            {
                Coordinates = new Vector2(Coordinates.X, Coordinates.Y - 1f / Scale);
            }
            if (MyKeyboard.IsHeld(Keys.NumPad2))
            {
                Coordinates = new Vector2(Coordinates.X, Coordinates.Y + 1f / Scale);
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
