using System;
using Microsoft.Xna.Framework.Input;

namespace Caveworks
{
    [Serializable]
    public class Camera
    {
        private const int renderDistance = 1;
        public World World;
        public MyVector2 Coordinates;
        public int Scale;
        public MyVector2Int ScreenCenter;
        public LightManager LightMap;

        public Camera(World world, MyVector2 coordinates, int scale)
        {
            World = world;
            Coordinates = new MyVector2(coordinates.X , coordinates.Y);
            Scale = scale;
            ScreenCenter = new MyVector2Int(0, 0);
            LightMap = new LightManager(this);
        }

        public void Update()
        {
            ScreenCenter.X = (int)GameWindow.Size.X / 2;
            ScreenCenter.Y = (int)GameWindow.Size.Y / 2;

            if (MyKeyboard.GetScrollWheelMovement() > 0) // zoom in
            {
                Scale += 2;
            }
            if (MyKeyboard.GetScrollWheelMovement() < 0) // zoom out
            {
                Scale -= 2;
            }

            if (Scale > 128) { Scale = 128; } // zoom minimum
            
            if (Scale < GameWindow.Size.X / 64) { Scale = (int)(GameWindow.Size.X / 64); } // zoom maximum

            if (World.PlayerBody != null)
            {
                this.Coordinates = World.PlayerBody.Coordinates;
            }
        }

        public void DrawWorld()
        {
            MyVector2 cameraChunk = WorldCordsToChunk(this.Coordinates);
            int camera_x = (int)cameraChunk.X;
            int camera_y = (int)cameraChunk.Y;

            for (int x = -renderDistance; x <= renderDistance; x++)
            {
                for (int y = -renderDistance; y <= renderDistance; y++)
                {
                    if (0 <= (camera_x + x) && (camera_x + x) < World.WorldSize && 0 <= (camera_y + y) && (camera_y + y) < World.WorldSize)
                    {
                        World.ChunkList[camera_x + x, camera_y + y].Draw(this);
                    }
                }
            }
        }


        public MyVector2Int WorldToScreenCords(MyVector2 worldCoordinates) // world coordinastes to screen coordinates
        {
            return new MyVector2Int((int)MathF.Round(ScreenCenter.X + ((worldCoordinates.X - Coordinates.X) * Scale)), (int)MathF.Round(ScreenCenter.Y + ((worldCoordinates.Y - Coordinates.Y) * Scale)));
        }

        public MyVector2Int WorldToScreenCords(MyVector2Int worldCoordinates) // world coordinastes to screen coordinates
        {
            return new MyVector2Int((int)MathF.Round(ScreenCenter.X + ((worldCoordinates.X - Coordinates.X) * Scale)), (int)MathF.Round(ScreenCenter.Y + ((worldCoordinates.Y - Coordinates.Y) * Scale)));
        }


        public MyVector2 WorldCordsToChunk(MyVector2 worldCoordinates)
        {
            return new MyVector2((int)(worldCoordinates.X/32), (int)(worldCoordinates.Y/32));
        }
    }
}
