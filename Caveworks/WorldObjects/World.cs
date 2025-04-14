using Microsoft.Xna.Framework;
using System;
using System.Threading.Tasks;

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
        public ResearchManager Research;
        public PlayerBody PlayerBody;
        public float DeltaTime = 0;

        public MyVector2 WorldMousePos;
        public Tile MouseTile;
        public Tile LastMouseTile;

        [NonSerialized]
        Task<bool> LightmapTask;


        public bool Paused { get; set; } = false;


        public World(int worldSize)
        {
            WorldSize = worldSize;
            WorldDiameter = worldSize * Chunk.chunkSize;

            GenerateWorld();

            /* PERFORMANCE TEST
            for (int x = 0; x < 100; x++)
            {
                for (int y = 0; y < 100; y++)
                {
                    new SlowBelt(GlobalCordsToTile(new MyVector2Int(x, y)), new MyVector2Int(1, 0));
                    new RawIronOreItem(GlobalCordsToTile(new MyVector2Int(x, y)), new MyVector2(0.5f, 0.5f), 1);
                    new RawIronOreItem(GlobalCordsToTile(new MyVector2Int(x, y)), new MyVector2(0.5f, 0.5f), 1);
                    new RawIronOreItem(GlobalCordsToTile(new MyVector2Int(x, y)), new MyVector2(0.5f, 0.5f), 1);
                }
            }
            */

            Camera = new Camera(this, new MyVector2(worldSize / 2, worldSize / 2), (int)(GameWindow.Size.X / 64));
            Player = new Player(this);
            PlayerBody = new PlayerBody(GlobalCordsToTile(new MyVector2Int(WorldDiameter/2, WorldDiameter/2)));
            Research = new ResearchManager();

            // TESTCODE
            for (int i = 0; i < 5; i++)
            {
                Player.PlayerInventory.TryAddItem(new SlowBeltItem(100));
            }
            Player.PlayerInventory.TryAddItem(new RawIronOreItem(100));
            Player.PlayerInventory.TryAddItem(new RawCopperOreItem(100));
            Player.PlayerInventory.TryAddItem(new RawStoneItem(100));
            Player.PlayerInventory.TryAddItem(new IronChestItem(100));
            Player.PlayerInventory.TryAddItem(new AsseblingMachineItem(100));
            Player.PlayerInventory.TryAddItem(new ElectricLightItem(100));
            Player.PlayerInventory.TryAddItem(new FireplaceItem(100));
            Player.PlayerInventory.TryAddItem(new CrossroadItem(100));
            Player.PlayerInventory.TryAddItem(new SplitterItem(100));
            Player.PlayerInventory.TryAddItem(new StoneFurnaceItem(100));
            Player.PlayerInventory.TryAddItem(new DrillItem(100));
            Player.PlayerInventory.TryAddItem(new ResearchLabItem(100));

            WorldMousePos = GetWorldMousePos();
            LastMouseTile = MouseTile;
            MouseTile = TryGlobalCordsToTile(WorldMousePos.ToMyVector2Int());
        }


        public void Update(GameTime gameTime)
        {
            WorldMousePos = GetWorldMousePos();
            Tile newMouseTile = TryGlobalCordsToTile(WorldMousePos.ToMyVector2Int());
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


            Player.Update(DeltaTime);
            PlayerBody.Update(DeltaTime);
            Camera.Update();
            Research.Update();
            Sounds.PlaceSoundCooldown -= DeltaTime;

            LightmapTask = Task.Run(() => Camera.LightMap.UpdateLightmap(Camera));

            foreach (Chunk chunk in ChunkList)
            { 
                chunk.Update(DeltaTime);
            }
        }


        public void Draw()
        {
            if (Paused) Game.MainSpriteBatch.DrawString(Fonts.LargeFont, "PAUSED", new Vector2(GameWindow.Size.X / 2 - 100, GameWindow.Size.Y / 2), Color.White);

            Camera.DrawWorld();
            Player.Draw();
            PlayerBody.Draw(Camera);
            Research.Draw();

            if (LightmapTask != null)
            {
                LightmapTask.Wait();
                // TEST - unhash before use
                Camera.LightMap.DrawUpscaled(Camera);
            }
        }


        public void GenerateWorld()
        {
            ChunkList = WorldGenerator.GenerateWorld(this, WorldSize, WorldDiameter);
        }


        public Tile GlobalCordsToTile(MyVector2Int globalCords)
        {
            return ChunkList[globalCords.X / 32, globalCords.Y / 32].TileList[globalCords.X % 32, globalCords.Y % 32];
        }


        public Tile TryGlobalCordsToTile(MyVector2Int globalCords)
        {
            if (globalCords.X >= 0 && globalCords.X < WorldDiameter && globalCords.Y >= 0 && globalCords.Y < WorldDiameter)
            {
                return ChunkList[globalCords.X / 32, globalCords.Y / 32].TileList[globalCords.X % 32, globalCords.Y % 32];
            }
            return null;
        }


        public Tile GetTileByRelativePosition(Tile tile, MyVector2Int relativePosition)
        {
            return GlobalCordsToTile(new MyVector2Int(tile.Position.X + relativePosition.X, tile.Position.Y + relativePosition.Y));
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
