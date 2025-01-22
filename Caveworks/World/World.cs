using Microsoft.Xna.Framework;
using System;

namespace Caveworks
{
    [Serializable]
    public class World
    {
        public int WorldSize { get; set; }
        public Chunk[,] Chunks {  get; set; }
        public Camera Camera { get; set; }
        public float DeltaTime {  get; set; } = 0;

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

            DeltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            Camera.Update();
            /* 
            foreach (Chunk chunk in Chunks)
            { 
                chunk.Update();
            }
            */
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
                    Chunks[x, y] = new Chunk(this, new MyVector2(x, y));
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
                            Chunks[chunk_x, chunk_y].Tiles[tile_x, tile_y].Floor = new StoneFloor(Chunks[chunk_x, chunk_y].Tiles[tile_x, tile_y]);

                            randomNumber = rnd.Next(100);

                            if (randomNumber < 10)
                            {
                                Chunks[chunk_x, chunk_y].Tiles[tile_x, tile_y].Wall = new StoneWall(Chunks[chunk_x, chunk_y].Tiles[tile_x, tile_y]);
                            }
                            
                        }
                    }
                }
            }
        }
    }
}
