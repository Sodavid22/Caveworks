using Microsoft.Xna.Framework;
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
        public float DeltaTime = 0;
        public BaseFloor defaultFloor;

        public bool Paused { get; set; } = false;


        public World(int worldSize)
        {
            WorldSize = worldSize;
            WorldDiameter = worldSize * Chunk.chunkSize;
            defaultFloor = new StoneFloor();

            Camera = new Camera(this, new MyVector2(worldSize / 2, worldSize / 2), 32);

            GenerateWorld();
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

            foreach (Chunk chunk in ChunkList)
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
            Tile tile;

            ChunkList = new Chunk[WorldSize, WorldSize];
            for (int x = 0; x < WorldSize; x++)
            {
                for (int y = 0; y < WorldSize; y++)
                {
                    ChunkList[x, y] = new Chunk(this, new MyVector2Int(x, y));
                }
            }

            for (int x = 0; x < WorldDiameter; x++)
            {
                for (int y = 0; y < WorldDiameter; y++)
                {
                    tile = GlobalCordsToTile(new MyVector2Int(x, y));

                    randomNumber = rnd.Next(0, 100);
                    if (randomNumber < 10 || x == 0 || y == 0 || x == WorldDiameter - 1 || y == WorldDiameter - 1)
                    {
                        tile.Wall = new StoneWall();
                    }
                }
            }

            ChunkList[0, 0].TileList[2, 2].Wall = null;
            ChunkList[0, 0].TileList[2, 2].AddCreature(new Player(ChunkList[0, 0].TileList[2, 2]));
            ChunkList[0, 0].TileList[3, 3].Wall = null;
            ChunkList[0, 0].TileList[3, 3].AddCreature(new Player(ChunkList[0, 0].TileList[3, 3]));

            ChunkList[0, 0].TileList[4, 4].AddItem(new RawIronOre(ChunkList[0, 0].TileList[4, 4], new MyVector2(0.1f, 0.1f), 10));
            ChunkList[0, 0].TileList[4, 4].AddItem(new RawIronOre(ChunkList[0, 0].TileList[4, 4], new MyVector2(0.1f, 0.9f), 10));
            ChunkList[0, 0].TileList[4, 4].AddItem(new RawIronOre(ChunkList[0, 0].TileList[4, 4], new MyVector2(0.9f, 0.1f), 10));
            ChunkList[0, 0].TileList[4, 4].AddItem(new RawIronOre(ChunkList[0, 0].TileList[4, 4], new MyVector2(0.9f, 0.9f), 10));
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
    }
}
