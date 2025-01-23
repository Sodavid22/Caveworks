using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Caveworks
{
    [Serializable]
    public class World
    {
        public int WorldSize;
        public Chunk[,] Chunks;
        public Camera Camera;
        public float DeltaTime = 0;

        public bool Paused { get; set; } = false;


        public World(int worldSize)
        {
            WorldSize = worldSize;

            GenerateWorld();

            Camera = new Camera(this, new MyVector2(worldSize/2, worldSize/2), 32);
        }


        public void Update(GameTime gameTime)
        {
            if (MyKeyboard.IsPressed(KeyBindings.PAUE_KEY))
            {
                Sounds.ButtonClick2.play(1.0f);
                Paused = !Paused;
            }
            if (Paused) return;

            DeltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;

            Camera.Update();

            foreach (Chunk chunk in Chunks)
            { 
                chunk.Update(DeltaTime);
            }
        }


        public void Draw()
        {
            if (Paused) Game.MainSpriteBatch.DrawString(Fonts.MenuButtonFont, "PAUSED", new Vector2(GameWindow.WindowSize.X / 2 - 100, GameWindow.WindowSize.Y / 2), Color.White);

            Camera.DrawWorld();
        }


        public void GenerateWorld()
        {
            Random rnd = new Random();
            int randomNumber;

            Chunks = new Chunk[WorldSize, WorldSize];
            for (int x = 0; x < WorldSize; x++)
            {
                for (int y = 0; y < WorldSize; y++)
                {
                    Chunks[x, y] = new Chunk(this, new MyVector2Int(x, y));
                }
            }

            for (int chunk_x = 0; chunk_x < WorldSize; chunk_x++)
            {
                for (int chunk_y = 0; chunk_y < WorldSize; chunk_y++)
                {
                    for (int tile_x = 0; tile_x < Chunk.chunkSize; tile_x++)
                    {
                        for (int tile_y = 0; tile_y < Chunk.chunkSize; tile_y++)
                        {
                            Chunks[chunk_x, chunk_y].Tiles[tile_x, tile_y].Floor = new StoneFloor();

                            randomNumber = rnd.Next(100);

                            if (randomNumber < 10)
                            {
                                Chunks[chunk_x, chunk_y].Tiles[tile_x, tile_y].Wall = new StoneWall();
                            }
                            
                        }
                    }
                }
            }
            Chunks[0, 0].Tiles[2, 2].Wall = null;
            Chunks[0, 0].Tiles[2, 2].AddCreature(new Player(Chunks[0, 0].Tiles[2, 2]));

        }


        public Tile GlobalCordsToTile(MyVector2Int globalCords)
        {
            return Chunks[globalCords.X / 32, globalCords.X / 32].Tiles[globalCords.X % 32, globalCords.Y % 32];
        }

        public Tile GetTileByRelativePosition(Tile tile, MyVector2Int relativePosition)
        {
            if (tile.GlobalCoords.X + relativePosition.X > 0 && tile.GlobalCoords.Y + relativePosition.Y > 0)
            {
                if (tile.GlobalCoords.X + relativePosition.X < WorldSize * Chunk.chunkSize && tile.GlobalCoords.Y + relativePosition.Y < WorldSize * Chunk.chunkSize)
                {
                    return GlobalCordsToTile(new MyVector2Int(tile.GlobalCoords.X + relativePosition.X, tile.GlobalCoords.Y + relativePosition.Y));
                }
            }
            return null;
        }
    }
}
