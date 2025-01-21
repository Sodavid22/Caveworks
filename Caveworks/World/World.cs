using System.Numerics;

namespace Caveworks
{
    public class World
    {
        public int WorldSize { get; set; }
        public Chunk[,] Chunks {  get; set; }
        public Camera Camera { get; set; }


        public World(int worldSize)
        {
            WorldSize = worldSize;
            Chunks = new Chunk[(int)worldSize, (int)worldSize];

            for (int x = 0; x < worldSize; x++)
            {
                for (int y = 0; y < worldSize; y++)
                {
                    Chunks[x, y] = new Chunk(this, new Vector2(x, y));
                }
            }

            Camera = new Camera(this, new Vector2(worldSize/2, worldSize/2), 32);
        }


        public void Update()
        {
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
            Camera.DrawWorld();
        }
    }
}
