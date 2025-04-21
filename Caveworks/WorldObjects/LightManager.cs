using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using SharpDX.DXGI;

namespace Caveworks
{
    [Serializable]
    public class LightManager
    {
        public MyVector2Int LightMapSize;
        public MyVector2Int FinalLightmapSize;
        public int[,] CalculatedLightmap;
        public int[,] FinalLightMap;
        int[,] UpscaleMap1;
        int[,] UpscaleMap2;
        public static int MaxLightRange;
        public static int MaxLightStrength;
        public static int MinLightForMaxBrightness;
        public int CalculationStage;
        public int CalculationStageCount = 5; // lightmap calculation is distributed to multiple frames to improve performance at the cost of responsivnes (max 17)
        public const int DirectLightLoss = 1000;
        public const int InDirectLightLoss = 1414;
        Tile CalculatedCenterTile;
        Tile CurrentCenterTile;

        public LightManager(Camera camera)
        {
            CalculateLightRanges(Globals.LightDistance);
            CalculationStage = 0;
        }



        public void CalculateLightRanges(int maxLightRange)
        {
            MaxLightRange = maxLightRange;
            MaxLightStrength = MaxLightRange * DirectLightLoss;
            MinLightForMaxBrightness = MaxLightStrength - 8000;

            LightMapSize = new MyVector2Int(63 + maxLightRange*2, 33 + maxLightRange*2);
            CalculatedLightmap = new int[LightMapSize.X, LightMapSize.Y];
            UpscaleMap1 = new int[LightMapSize.X * 2, LightMapSize.Y * 2];
            UpscaleMap2 = new int[LightMapSize.X * 2, LightMapSize.Y * 2];
            FinalLightMap = new int[LightMapSize.X * 2, LightMapSize.Y * 2];
            FinalLightmapSize = new MyVector2Int(LightMapSize.X * 2, LightMapSize.Y * 2);
        }


        public void CreateLightmap(Camera camera, World world)
        {
            CalculatedCenterTile = world.GlobalCordsToTile(camera.Coordinates.ToMyVector2Int());
            Tile tile;
            MyVector2Int globalCords = new MyVector2Int(0, 0);

            for (int x = 0; x < LightMapSize.X; x++)
            {
                for (int y = 0; y < LightMapSize.Y; y++)
                {
                    globalCords.X = CalculatedCenterTile.Position.X - LightMapSize.X / 2 + x;
                    globalCords.Y = CalculatedCenterTile.Position.Y - LightMapSize.Y / 2 + y;
                    tile = world.TryGlobalCordsToTile(globalCords);
                    if (tile != null)
                    {
                        if (tile.Wall != null)
                        {
                            CalculatedLightmap[x, y] = -1;
                        }
                        else if (tile.Building != null)
                        {
                            CalculatedLightmap[x, y] = tile.Building.GetLightLevel();
                        }
                        else
                        {
                            CalculatedLightmap[x, y] = 0;
                        }
                    }
                    else
                    {
                        CalculatedLightmap[x, y] = -1;
                    }
                }
            }
            if (CalculatedLightmap[LightMapSize.X / 2, LightMapSize.Y / 2] < MinLightForMaxBrightness)
            {
                CalculatedLightmap[LightMapSize.X / 2, LightMapSize.Y / 2] = MinLightForMaxBrightness;
            }
        }

        public bool UpdateLightmap(Camera camera)
        {
            if (CalculationStage == 0)
            {
                if (Globals.LightDistance != MaxLightRange)
                {
                    CalculateLightRanges(Globals.LightDistance);
                }
                CreateLightmap(camera, camera.World);
            }
            else if (CalculationStage > 0 && CalculationStage < CalculationStageCount)
            {
                for (int i = 0; i <= MaxLightRange/(CalculationStageCount - 1); i++)
                {
                    SmoothenLightMap();
                }
            }
            else if (CalculationStage == CalculationStageCount)
            {
                Upscale(CalculatedLightmap, LightMapSize);
                CurrentCenterTile = CalculatedCenterTile;
                CalculationStage = -1;
            }
            CalculationStage += 1;
            return true;
        }


        public void DrawUpscaled(Camera camera)
        {
            if (CurrentCenterTile != null)
            {
                MyVector2 worldCords = new MyVector2(0, 0);
                MyVector2Int screenCords = new MyVector2Int(0, 0);
                Vector4 color;
                float light;

                for (int x = 0; x < FinalLightmapSize.X; x++)
                {
                    for (int y = 0; y < FinalLightmapSize.Y; y++)
                    {
                        worldCords.X = CurrentCenterTile.Position.X - (FinalLightmapSize.X / 4) + ((float)x/2);
                        worldCords.Y = CurrentCenterTile.Position.Y - (FinalLightmapSize.Y / 4) + ((float)y/2);
                        screenCords = camera.WorldToScreenCords(worldCords);
                        if (screenCords.X >= -camera.Scale && screenCords.Y >= -camera.Scale & screenCords.X < GameWindow.Size.X && screenCords.Y < GameWindow.Size.Y)
                        {
                            light = FinalLightMap[x, y];
                            if (light < 0)
                            {
                                light = 0;
                            }
                            if (light > MinLightForMaxBrightness)
                            {
                                light = MinLightForMaxBrightness;
                            }
                            color = new Vector4(0.1f, 0.1f, 0.1f, 1 - (light / MinLightForMaxBrightness));

                            Game.ShadowSpriteBatch.Draw(Textures.EmptyTexture, new Rectangle(screenCords.X, screenCords.Y, camera.Scale / 2, camera.Scale / 2), Color.FromNonPremultiplied(color));
                        }
                    }
                }
            }
        }


        public void Upscale(int[,] lightmap, MyVector2Int lightmapSize)
        {
            for (int x = 0; x < lightmapSize.X * 2; x++)
            {
                for (int y = 0; y < lightmapSize.Y * 2; y++)
                {
                    UpscaleMap1[x, y] = lightmap[(x / 2), (y / 2)];
                }
            }

            for (int x = 2; x < lightmapSize.X * 2 - 2; x++)
            {
                for (int y = 2; y < lightmapSize.Y * 2 - 2; y++)
                {
                    UpscaleMap2[x, y] = GetUpscaleWallBrightness(UpscaleMap1, lightmapSize, x, y);
                }
            }

            for (int x = 1; x < lightmapSize.X * 2 - 1; x++)
            {
                for (int y = 1; y < lightmapSize.Y * 2 - 1; y++)
                {
                    FinalLightMap[x, y] = GetUpscaleBrightness(UpscaleMap2, lightmapSize, x, y, UpscaleMap1);
                }
            }
        }


        public void SmoothenLightMap()
        {
            for (int x = 0; x < LightMapSize.X; x++)
            {
                for (int y = 0; y < LightMapSize.Y; y++)
                {
                    CalculatedLightmap[x, y] = GetBrightness(x, y);
                }
            }
        }


        public int GetBrightness(int tileX, int tileY)
        {
            int brightness = CalculatedLightmap[tileX, tileY];

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
                                    if (CalculatedLightmap[x, y] - DirectLightLoss > brightness)
                                    {
                                        brightness = CalculatedLightmap[x, y] - DirectLightLoss;
                                    }
                                }
                                else // diagonal neighbour
                                {
                                    if (CalculatedLightmap[x, y] - InDirectLightLoss > brightness)
                                    {
                                        brightness = CalculatedLightmap[x, y] - InDirectLightLoss;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return brightness;
        }


        public int GetUpscaleBrightness(int[,] lightmap, MyVector2Int lightmapSize, int tileX, int tileY, int[,] wallLigtmap)
        {
            int brightness = lightmap[tileX, tileY];
            int neighbours = 1;

            if (wallLigtmap[tileX, tileY] != -1) // not wall
            {
                if (wallLigtmap[tileX + 1, tileY] != -1)
                {
                    if (lightmap[tileX + 1, tileY] > 0)
                    {
                        brightness += lightmap[tileX + 1, tileY];
                        neighbours += 1;
                    }
                }
                else
                {
                    neighbours += 0;
                }
                if (wallLigtmap[tileX - 1, tileY] != -1)
                {
                    if (lightmap[tileX - 1, tileY] > 0)
                    {
                        brightness += lightmap[tileX - 1, tileY];
                        neighbours += 1;
                    }
                }
                else
                {
                    neighbours += 0;
                }
                if (wallLigtmap[tileX, tileY + 1] != -1)
                {
                    if (lightmap[tileX, tileY + 1] > 0)
                    {
                        brightness += lightmap[tileX, tileY + 1];
                        neighbours += 1;
                    }
                }
                else
                {
                    neighbours += 0;
                }
                if (wallLigtmap[tileX, tileY - 1] != -1)
                {
                    if (lightmap[tileX, tileY - 1] > 0)
                    {
                        brightness += lightmap[tileX, tileY - 1];
                        neighbours += 1;
                    }
                }
                else
                {
                    neighbours += 0;
                }
            }
            return brightness / neighbours;
        }

        public int GetUpscaleWallBrightness(int[,] lightmap, MyVector2Int lightmapSize, int tileX, int tileY)
        {
            int brightness = lightmap[tileX, tileY];

            if (brightness == -1) // wall
            {
                for (int x = tileX - 2; x <= tileX + 2; x += 2)
                {
                    for (int y = tileY - 2; y <= tileY + 2; y += 2)
                    {
                        if (x == tileX || y == tileY)
                        {
                            if (lightmap[x, y] - DirectLightLoss > brightness && lightmap[x, y] > 0)
                            {
                                brightness = lightmap[x, y] - DirectLightLoss;
                            }
                        }
                        else
                        {
                            if (lightmap[x, y] - InDirectLightLoss > brightness && lightmap[x, y] > 0)
                            {
                                brightness = lightmap[x, y] - InDirectLightLoss;
                            }
                        }
                    }
                }
            }
            return brightness;
        }
    }
}
