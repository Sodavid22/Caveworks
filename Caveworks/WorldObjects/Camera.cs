﻿using Microsoft.Xna.Framework;
using System;

namespace Caveworks
{
    [Serializable]
    public class Camera
    {
        private const int renderDistance = 1;
        public World World;
        public MyVector2 Coordinates;
        public int Scale;
        public MyVector2 ScreenCenter;
        public PlayerBody player;

        public Camera(World world, MyVector2 coordinates, int scale)
        {
            World = world;
            Coordinates = new MyVector2(coordinates.X , coordinates.Y);
            Scale = scale;
            ScreenCenter = new MyVector2(0, 0);
        }

        public void Update()
        {
            ScreenCenter.X = GameWindow.Size.X / 2;
            ScreenCenter.Y = GameWindow.Size.Y / 2;

            if (MyKeyboard.GetScrollWheelMovement() > 0) // zoom in
            {
                Scale += 1;
            }
            if (MyKeyboard.GetScrollWheelMovement() < 0) // zoom out
            {
                Scale -= 1;
            }

            if (Scale > 64) { Scale = 64; } // zoom minimum
            if (!Game.DEVMODE)
            {
                if (Scale < GameWindow.Size.X / 64) { Scale = (int)(GameWindow.Size.X / 64); } // zoom maximum
            }
            else
            {
                if (Scale < 1) { Scale = 1; } // zoom maximum
            }

            if (player != null)
            {
                this.Coordinates = player.Coordinates;
            }
        }

        public void DrawWorld()
        {
            if (Game.DEVMODE)
            {
                try
                {
                    Game.MainSpriteBatch.DrawString(Fonts.DefaultFont, " position: " + this.Coordinates.ToString(), new Vector2(100, 100), Color.White);
                    Game.MainSpriteBatch.DrawString(Fonts.DefaultFont, " zoom: " + this.Scale.ToString(), new Vector2(100, 120), Color.White);
                    Game.MainSpriteBatch.DrawString(Fonts.DefaultFont, " tile cords: " + this.World.GlobalCordsToTile(this.Coordinates.ToMyVector2Int()).Position, new Vector2(100, 140), Color.White);
                }
                catch { Game.MainSpriteBatch.DrawString(Fonts.DefaultFont, "X", new Vector2(GameWindow.Size.X / 2 - 6, GameWindow.Size.Y / 2 - 6), Color.Red); }
            }

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


        public MyVector2 WorldToScreenCords(MyVector2 worldCoordinates) // world coordinastes to screen coordinates
        {
            return new MyVector2(ScreenCenter.X + ((worldCoordinates.X - Coordinates.X) * Scale), ScreenCenter.Y + ((worldCoordinates.Y - Coordinates.Y) * Scale));
        }

        public MyVector2 WorldCordsToChunk(MyVector2 worldCoordinates)
        {
            return new MyVector2((int)(worldCoordinates.X/32), (int)(worldCoordinates.Y/32));
        }
    }
}
