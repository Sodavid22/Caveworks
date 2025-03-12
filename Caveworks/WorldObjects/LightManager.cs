using System;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;
using System.Threading;

namespace Caveworks
{
    [Serializable]
    public class LightManager // LIGHT LOSES 3 STRENGTH FOR EACH TILE TRAVELLED
    {
        public MyVector2Int LightMapSize;
        public int[,] Lightmap;
        public const int MaxLightRange = 16;
        public const int MaxLightStrength = MaxLightRange * 3;
        public const int MinLightForMaxBrightness = 24;
        Tile CenterTile;

        public LightManager(Camera camera)
        {
            LightMapSize = new MyVector2Int(65 + 30, 37 + 30);
            Lightmap = new int[LightMapSize.X, LightMapSize.Y];
            CreateLightmap(camera, camera.World);
        }


        public void CreateLightmap(Camera camera, World world)
        {
            CenterTile = world.GlobalCordsToTile(camera.Coordinates.ToMyVector2Int());
            Tile tile;
            MyVector2Int globalCords = new MyVector2Int(0, 0);

            for (int x = 0; x < LightMapSize.X; x++)
            {
                for (int y = 0; y < LightMapSize.Y; y++)
                {
                    globalCords.X = CenterTile.Position.X - LightMapSize.X / 2 + x;
                    globalCords.Y = CenterTile.Position.Y - LightMapSize.Y / 2 + y;
                    tile = world.TryGlobalCordsToTile(globalCords);
                    if (tile != null)
                    {
                        if (tile.Wall != null)
                        {
                            Lightmap[x, y] = -1;
                        }
                        else if (tile.Building != null)
                        {
                            Lightmap[x, y] = tile.Building.GetLightLevel();
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
            Lightmap[LightMapSize.X / 2, LightMapSize.Y / 2] = MaxLightStrength;
        }

        public bool UpdateLightmap()
        {
            for (int i = 0; i < MaxLightRange; i++)
            {
                SmoothenLightMap();
            }
            FinishLightMap();
            return true;
        }


        public void DrawLightMap(Camera camera)
        {
            MyVector2 worldCords = new MyVector2(0, 0);
            MyVector2 screenCords = new MyVector2(0, 0);
            Vector4 color;
            float light;

            for (int x = 0; x < LightMapSize.X; x++)
            {
                for (int y = 0; y < LightMapSize.Y; y++)
                {
                    worldCords.X = CenterTile.Position.X - LightMapSize.X / 2 + x;
                    worldCords.Y = CenterTile.Position.Y - LightMapSize.Y / 2 + y;
                    screenCords = camera.WorldToScreenCords(worldCords);

                    light = Lightmap[x, y];
                    if (light < 0)
                    {
                        light = 0;
                    }
                    if (light > MinLightForMaxBrightness)
                    {
                        light = MinLightForMaxBrightness;
                    }
                    color = new Vector4(0, 0, 0, 1 - (light / MinLightForMaxBrightness));

                    Game.ShadowSpriteBatch.Draw(Textures.EmptyTexture, new Rectangle((int)screenCords.X, (int)screenCords.Y, camera.Scale, camera.Scale), Color.FromNonPremultiplied(color));
                }
            }
        }


        public void SmoothenLightMap()
        {
            for (int x = 0; x < LightMapSize.X; x++)
            {
                for (int y = 0; y < LightMapSize.Y; y++)
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
                            if (x >= 0 && y >= 0 && x < LightMapSize.X && y < LightMapSize.Y) // not outside map
                            {
                                if (x == tileX || y == tileY) // direct neighbour
                                {
                                    if (Lightmap[x, y] - 3 > brightness)
                                    {
                                        brightness = Lightmap[x, y] - 3;
                                    }
                                }
                                else // diagonal neighbour
                                {
                                    if (Lightmap[x, y] - 4 > brightness)
                                    {
                                        brightness = Lightmap[x, y] - 4;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return brightness;
        }


        public void FinishLightMap()
        {
            int[,] newLightmap = new int[LightMapSize.X, LightMapSize.Y];

            for (int x = 0; x < LightMapSize.X; x++)
            {
                for (int y = 0; y < LightMapSize.Y; y++)
                {
                    newLightmap[x, y] = GetFinalBrightness(x, y);
                }
            }

            Lightmap = newLightmap;
        }


        public int GetFinalBrightness(int tileX, int tileY)
        {
            int brightness = Lightmap[tileX, tileY];

            if (brightness == -1) // wall
            {
                for (int x = tileX - 1; x <= tileX + 1; x++)
                {
                    for (int y = tileY - 1; y <= tileY + 1; y++)
                    {
                        if (!(x == tileX && y == tileY)) // not self
                        {
                            if (x >= 0 && y >= 0 && x < LightMapSize.X && y < LightMapSize.Y) // not outside map
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
