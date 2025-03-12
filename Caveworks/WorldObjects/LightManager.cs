using System;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Caveworks
{
    [Serializable]
    public class LightManager
    {
        public int LightMapSize;
        public int[,] Lightmap;
        public const int MaxLightStrength = 32;
        Tile CenterTile;

        public LightManager(Camera camera, int renderDistance)
        {
            LightMapSize = (renderDistance * 2 + 1) * Chunk.chunkSize;
            Lightmap = new int[LightMapSize, LightMapSize];
        }


        public void CreateLightmap(Camera camera, World world)
        {
            CenterTile = world.GlobalCordsToTile(camera.Coordinates.ToMyVector2Int());
            Tile tile;
            MyVector2Int globalCords = new MyVector2Int(0, 0);

            for (int x = 0; x < LightMapSize - 1; x++)
            {
                for (int y = 0; y < LightMapSize - 1; y++)
                {
                    globalCords.X = CenterTile.Position.X - LightMapSize / 2 + x;
                    globalCords.Y = CenterTile.Position.Y - LightMapSize / 2 + y;
                    tile = world.TryGlobalCordsToTile(globalCords);
                    if (tile != null)
                    {
                        if (tile.Wall != null)
                        {
                            Lightmap[x, y] = -1;
                        }
                        else if (tile.Building != null)
                        {
                            Lightmap[x, y] = 64;
                        }
                        else
                        {
                            Lightmap[x, y] = 0;
                        }
                    }
                    else
                    {
                        Lightmap[x, y] = 4;
                    }
                }
            }
            if (MyKeyboard.IsPressed(Keys.L))
            {
                Debug.WriteLine("--------------------- cords: " + CenterTile.Position.X + " - " + CenterTile.Position.Y);
            }
        }

        public void UpdateLightmap()
        {
            for (int i = 0; i < MaxLightStrength/2; i++)
            {
                SmoothenLightMap();
            }
        }


        public void DrawLightMap(Camera camera)
        {
            MyVector2 worldCords = new MyVector2(0, 0);
            MyVector2 screenCords = new MyVector2(0, 0);
            Vector4 color;
            float light;

            for (int x = 0; x < LightMapSize - 1; x++)
            {
                for (int y = 0; y < LightMapSize - 1; y++)
                {
                    worldCords.X = CenterTile.Position.X - LightMapSize / 2 + x;
                    worldCords.Y = CenterTile.Position.Y - LightMapSize / 2 + y;
                    screenCords = camera.WorldToScreenCords(worldCords);

                    light = Lightmap[x, y];
                    if (light < 0)
                    {
                        light = 0;
                    }
                    if (light > MaxLightStrength)
                    {
                        light = MaxLightStrength;
                    }
                    color = new Vector4(0, 0, 0, 1 - (light / MaxLightStrength));

                    Game.ShadowSpriteBatch.Draw(Textures.EmptyTexture, new Rectangle((int)screenCords.X, (int)screenCords.Y, camera.Scale, camera.Scale), Color.FromNonPremultiplied(color));
                
                }
            }
        }


        public void SmoothenLightMap()
        {
            for (int x = 0; x < LightMapSize - 1; x++)
            {
                for (int y = 0; y < LightMapSize - 1; y++)
                {
                    Lightmap[x, y] = GetBrightness(x, y);
                }
            }
        }


        public int GetBrightness(int tileX, int tileY)
        {
            int brightness = Lightmap[tileX, tileY];

            if (brightness != -1) // not wall
            {
                for (int x = tileX - 1; x <= tileX + 1; x++)
                {
                    for (int y = tileY - 1; y <= tileY + 1; y++)
                    {
                        if (!(x == tileX && y == tileY)) // not self
                        {
                            if (x > 0 && y > 0 && x < LightMapSize && y < LightMapSize) // not outside map
                            {
                                if (x == tileX || y == tileY) // direct neighbour
                                {
                                    if (Lightmap[x, y] - 4 > brightness)
                                    {
                                        brightness = Lightmap[x, y] - 4;
                                    }
                                }
                                else // diagonal neighbour
                                {
                                    if (Lightmap[x, y] - 5 > brightness)
                                    {
                                        brightness = Lightmap[x, y] - 5;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return brightness;
        }
    }
}
