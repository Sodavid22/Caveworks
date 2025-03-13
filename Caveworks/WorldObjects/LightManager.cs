using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Caveworks
{
    [Serializable]
    public class LightManager // LIGHT LOSES 3 STRENGTH FOR EACH TILE TRAVELLED
    {
        public MyVector2Int LightMapSize;
        public MyVector2Int FinalLightmapSize;
        public int[,] CalculatedLightmap;
        public int[,] FinalLightMap;
        public static int MaxLightRange;
        public static int MaxLightStrength;
        public static int MinLightForMaxBrightness;
        public int CalculationStage;
        public int CalculationStageCount = 5; // lightmap calculation is distributed to multiple frames to improve performance at the cost of responsivnes
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
            MaxLightStrength = MaxLightRange * 3;
            MinLightForMaxBrightness = MaxLightStrength - 0;

            LightMapSize = new MyVector2Int(61 + maxLightRange*2 - 61, 31 + maxLightRange*2 - 31);
            CalculatedLightmap = new int[LightMapSize.X, LightMapSize.Y];
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
                        CalculatedLightmap[x, y] = 16;
                    }
                }
            }
            if (CalculatedLightmap[LightMapSize.X / 2, LightMapSize.Y / 2] < 48)
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
            else if (CalculationStage > 0 && CalculationStage < CalculationStageCount - 1)
            {
                for (int i = 0; i < MathF.Ceiling((float)MaxLightRange/(CalculationStageCount - 2)); i++)
                {
                    SmoothenLightMap();
                }
            }
            else if (CalculationStage == CalculationStageCount)
            {
                FinalLightMap = Upscale(CalculatedLightmap, LightMapSize);
                FinalLightmapSize = new MyVector2Int(LightMapSize.X * 2, LightMapSize.Y * 2);
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

                        light = FinalLightMap[x, y];
                        if (light < 0)
                        {
                            light = 0;
                        }
                        if (light > MinLightForMaxBrightness)
                        {
                            light = MinLightForMaxBrightness;
                        }
                        color = new Vector4(0, 0, 0, 1 - (light / MinLightForMaxBrightness));

                        Game.ShadowSpriteBatch.Draw(Textures.EmptyTexture, new Rectangle(screenCords.X, screenCords.Y, camera.Scale/2, camera.Scale/2), Color.FromNonPremultiplied(color));
                    }
                }
            }
        }


        public int[,] Upscale(int[,] lightmap, MyVector2Int lightmapSize)
        {
            int[,] upscaledLightmap = new int[lightmapSize.X * 2, lightmapSize.Y * 2];
            int[,] secondUpscaledLightmap = new int[lightmapSize.X * 2, lightmapSize.Y * 2];
            for (float x = 0; x < lightmapSize.X * 2; x++)
            {
                for (float y = 0; y < lightmapSize.Y * 2; y++)
                {
                    upscaledLightmap[(int)x, (int)y] = lightmap[(int)(x / 2), (int)(y / 2)];
                }
            }
            for (int x = 1; x < lightmapSize.X * 2 - 1; x++)
            {
                for (int y = 1; y < lightmapSize.Y * 2 - 1; y++)
                {
                    secondUpscaledLightmap[x, y] = GetUpscaleBrightness(upscaledLightmap, lightmapSize, x, y);
                }
            }

            for (int x = 1; x < lightmapSize.X * 2 - 1; x++)
            {
                for (int y = 1; y < lightmapSize.Y * 2 - 1; y++)
                {
                    upscaledLightmap[x, y] = GetUpscaleWallBrightness(secondUpscaledLightmap, lightmapSize, x, y);
                }
            }

            return upscaledLightmap;
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
                                    if (CalculatedLightmap[x, y] - 3 > brightness)
                                    {
                                        brightness = CalculatedLightmap[x, y] - 3;
                                    }
                                }
                                else // diagonal neighbour
                                {
                                    if (CalculatedLightmap[x, y] - 4 > brightness)
                                    {
                                        brightness = CalculatedLightmap[x, y] - 4;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return brightness;
        }


        public int GetUpscaleBrightness(int[,] lightmap, MyVector2Int lightmapSize, int tileX, int tileY)
        {
            int brightness = lightmap[tileX, tileY];
            int neighbours = 1;

            if (brightness != -1) // not wall
            {
                for (int x = tileX - 1; x <= tileX + 1; x++)
                {
                    for (int y = tileY - 1; y <= tileY + 1; y++)
                    {
                        if (!(x == tileX && y == tileY)) // not self
                        {
                            if (lightmap[x, y] > 0)
                            {
                                brightness += lightmap[x, y];
                                neighbours += 1;
                            }
                        }
                    }
                }
                return brightness / neighbours;
            }
            else
            {
                return -1;
            }
        }

        public int GetUpscaleWallBrightness(int[,] lightmap, MyVector2Int lightmapSize, int tileX, int tileY)
        {
            int brightness = lightmap[tileX, tileY];

            if (brightness == -1) // wall
            {
                for (int x = tileX - 1; x <= tileX + 1; x++)
                {
                    for (int y = tileY - 1; y <= tileY + 1; y++)
                    {
                        if (!(x == tileX && y == tileY)) // not self
                        {
                            if (lightmap[x, y] > brightness)
                            {
                                brightness = lightmap[x, y];
                            }
                        }
                    }
                }
            }
            return brightness;
        }


        public int[,] FinishLightMap()
        {
            int[,] newLightMap = new int[LightMapSize.X, LightMapSize.Y];

            for (int x = 0; x < LightMapSize.X; x++)
            {
                for (int y = 0; y < LightMapSize.Y; y++)
                {
                    newLightMap[x, y] = GetWallBrightness(x, y);
                }
            }
            return newLightMap;
        }


        public int GetWallBrightness(int tileX, int tileY)
        {
            int brightness = CalculatedLightmap[tileX, tileY];

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
                                    if (CalculatedLightmap[x, y] - 3 > brightness)
                                    {
                                        brightness = CalculatedLightmap[x, y] - 3;
                                    }
                                }
                                else // diagonal neighbour
                                {
                                    if (CalculatedLightmap[x, y] - 4 > brightness)
                                    {
                                        brightness = CalculatedLightmap[x, y] - 4;
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
