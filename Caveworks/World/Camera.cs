using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Caveworks
{
    public class Camera
    {
        private const int renderDistance = 1;
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

            if (MyKeyboard.GetScrollWheelMovement() > 0) // zoom in
            {
                Scale += 1;
            }
            if (MyKeyboard.GetScrollWheelMovement() < 0) // zoom out
            {
                Scale -= 1;
            }
            if (Scale > 64 && !Game.DEVMODE) { Scale = 64; } // zoom minimum
            if (Scale < GameWindow.WindowSize.X / 64 &&!Game.DEVMODE) { Scale = (int)(GameWindow.WindowSize.X / 64); } // zoom maximum


            if (MyKeyboard.IsHeld(Keys.NumPad4))
            {
                Coordinates = new Vector2(Coordinates.X - 0.1f / Scale, Coordinates.Y);
            }
            if (MyKeyboard.IsHeld(Keys.NumPad6))
            {
                Coordinates = new Vector2(Coordinates.X + 0.1f / Scale, Coordinates.Y);
            }
            if (MyKeyboard.IsHeld(Keys.NumPad8))
            {
                Coordinates = new Vector2(Coordinates.X, Coordinates.Y - 0.1f / Scale);
            }
            if (MyKeyboard.IsHeld(Keys.NumPad2))
            {
                Coordinates = new Vector2(Coordinates.X, Coordinates.Y + 0.1f / Scale);
            }
        }

        public void DrawWorld()
        {
            if (Game.DEVMODE)
            {
                Game.MainSpriteBatch.DrawString(Fonts.DefaultFont, this.Coordinates.ToString() + " zoom: " + this.Scale.ToString(), new Vector2(100, 100), Color.White);
            }

            Vector2 cameraChunk = WorldCordsToChunk(this.Coordinates);
            int camera_x = (int)cameraChunk.X;
            int camera_y = (int)cameraChunk.Y;
            for (int x = -renderDistance; x <= renderDistance; x++)
            {
                for (int y = -renderDistance; y <= renderDistance; y++)
                {
                    if (0 <= (camera_x + x) && (camera_x + x) < World.WorldSize && 0 <= (camera_y + y) && (camera_y + y) < World.WorldSize)
                    {
                        World.Chunks[camera_x + x, camera_y + y].Draw(this);
                    }
                }
            }
        }


        public Vector2 WorldToScreenCords(Vector2 worldCoordinates) // world coordinastes to screen coordinates
        {
            Vector2 screenCoordinates = new Vector2();
            screenCoordinates.X = ScreenCenter.X + ((worldCoordinates.X - Coordinates.X) * Scale);
            screenCoordinates.Y = ScreenCenter.Y + ((worldCoordinates.Y - Coordinates.Y) * Scale);
            return screenCoordinates;
        }

        public Vector2 WorldCordsToChunk(Vector2 worldCoordinates)
        {
            return new Vector2((int)(worldCoordinates.X/32), (int)(worldCoordinates.Y/32));
        }
    }
}
