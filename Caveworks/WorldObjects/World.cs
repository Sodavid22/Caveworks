﻿using Microsoft.Xna.Framework;
using System;

namespace Caveworks
{
    [Serializable]
    public class World
    {
        public int WorldSize;
        public int WorldDiameter;
        public Chunk[,] ChunkList;
        public Camera Camera;
        public Player Player;
        public PlayerBody PlayerBody;
        public float DeltaTime = 0;

        public MyVector2 WorldMousePos;
        public Tile MouseTile;
        public Tile LastMouseTile;


        public bool Paused { get; set; } = false;


        public World(int worldSize)
        {
            WorldSize = worldSize;
            WorldDiameter = worldSize * Chunk.chunkSize;

            GenerateWorld();

            Camera = new Camera(this, new MyVector2(worldSize / 2, worldSize / 2), 32);
            Player = new Player(this);
            PlayerBody = new PlayerBody(GlobalCordsToTile(new MyVector2Int(WorldDiameter/2, WorldDiameter/2)));

            // TESTCODE
            for (int i = 0; i < 10; i++)
            {
                Player.Inventory.TryAddItem(new SlowBeltItem(100));
            }
            Player.Inventory.TryAddItem(new RawIronOreItem(100));
            Player.Inventory.TryAddItem(new RawIronOreItem(100));
            Player.Inventory.TryAddItem(new IronChestItem(50));

            WorldMousePos = GetWorldMousePos();
            LastMouseTile = MouseTile;
            MouseTile = GlobalCordsToTile(WorldMousePos.ToMyVector2Int());
        }


        public void Update(GameTime gameTime)
        {
            WorldMousePos = GetWorldMousePos();
            Tile newMouseTile = GlobalCordsToTile(WorldMousePos.ToMyVector2Int());
            if (newMouseTile != null)
            {
                LastMouseTile = MouseTile;
                MouseTile = newMouseTile;
            }

            if (MyKeyboard.IsPressed(KeyBindings.PAUE_KEY))
            {
                Sounds.ButtonClick2.Play(1.0f);
                Paused = !Paused;
            }
            if (Paused) return;

            DeltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            if (DeltaTime > 0.02f)
            {
                DeltaTime = 0.02f;
            }

            Camera.Update();
            Player.Update();
            PlayerBody.Update(DeltaTime);
            Sounds.PlaceSoundCooldown -= DeltaTime;

            foreach (Chunk chunk in ChunkList)
            { 
                chunk.Update(DeltaTime);
            }
        }


        public void Draw()
        {
            if (Paused) Game.MainSpriteBatch.DrawString(Fonts.MenuButtonFont, "PAUSED", new Vector2(GameWindow.Size.X / 2 - 100, GameWindow.Size.Y / 2), Color.White);

            Camera.DrawWorld();
            Player.Draw();
            PlayerBody.Draw(Camera);
        }


        public void GenerateWorld()
        {
            ChunkList = WorldGenerator.GenerateWorld(this, WorldSize, WorldDiameter);
        }


        public Tile GlobalCordsToTile(MyVector2Int globalCords)
        {
            if (globalCords.X >= 0 && globalCords.X < WorldDiameter && globalCords.Y >= 0 && globalCords.Y < WorldDiameter)
            {
                return ChunkList[globalCords.X / 32, globalCords.Y / 32].TileList[globalCords.X % 32, globalCords.Y % 32];
            }
            return null;
        }


        public Tile GetTileByRelativePosition(Tile tile, MyVector2Int relativePosition)
        {
            if (tile.Position.X + relativePosition.X >= 0 && tile.Position.Y + relativePosition.Y >= 0)
            {
                if (tile.Position.X + relativePosition.X < WorldSize * Chunk.chunkSize && tile.Position.Y + relativePosition.Y < WorldSize * Chunk.chunkSize)
                {
                    return GlobalCordsToTile(new MyVector2Int(tile.Position.X + relativePosition.X, tile.Position.Y + relativePosition.Y));
                }
            }
            return null;
        }


        private MyVector2 GetWorldMousePos()
        {
            return new MyVector2(Camera.Coordinates.X + (MyKeyboard.GetMousePosition().X - GameWindow.Size.X / 2) / Camera.Scale, Camera.Coordinates.Y + (MyKeyboard.GetMousePosition().Y - GameWindow.Size.Y / 2) / Camera.Scale);
        }


        public float RotationToAngle(MyVector2Int rotation)
        {
            return (float)Math.Atan2(rotation.Y, rotation.X);
        }


        public void RemoveEmptyTiles()
        {
            Tile tile;
            for (int chunk_x = 0; chunk_x < WorldSize; chunk_x++)
            {
                for (int chunk_y = 0; chunk_y < WorldSize; chunk_y++)
                {
                    for (int tile_x = 0; tile_x < Chunk.chunkSize; tile_x++)
                    {
                        for (int tile_y = 0; tile_y < Chunk.chunkSize; tile_y++)
                        {
                            tile = ChunkList[chunk_x, chunk_y].TileList[tile_x, tile_y];

                            if (tile.Floor is StoneFloor && tile.Wall == null && tile.Items.Count == 0 && tile.Building == null)
                            {
                                tile.Delete();
                            }
                        }
                    }
                }
            }
        }

        public void FillEmptyTiles()
        {
            Tile tile;
            for (int chunk_x = 0; chunk_x < WorldSize; chunk_x++)
            {
                for (int chunk_y = 0; chunk_y < WorldSize; chunk_y++)
                {
                    for (int tile_x = 0; tile_x < Chunk.chunkSize; tile_x++)
                    {
                        for (int tile_y = 0; tile_y < Chunk.chunkSize; tile_y++)
                        {
                            if (ChunkList[chunk_x, chunk_y].TileList[tile_x, tile_y] == null)
                            {
                                tile = new Tile(ChunkList[chunk_x, chunk_y], new MyVector2Int(chunk_x * Chunk.chunkSize + tile_x, chunk_y * Chunk.chunkSize + tile_y));
                                ChunkList[chunk_x, chunk_y].TileList[tile_x, tile_y] = tile;
                                new StoneFloor(tile);
                            }
                        }
                    }
                }
            }
        }
    }
}
