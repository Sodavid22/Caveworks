using Microsoft.Xna.Framework;

namespace Caveworks
{
    public class Chunk
    {
        const int chunkSize = 32;
        public World World { get; set; }
        public Vector2 Coordinates { get; set; }
        public Tile[,] Tiles { get; set; }


        public Chunk(World world, Vector2 coordinates)
        {
            World = world;
            Coordinates = coordinates;
            Tiles = new Tile[chunkSize, chunkSize];

            for (int x = 0; x < chunkSize; x++)
            {
                for (int y = 0; y < chunkSize; y++)
                {
                    Tiles[x, y] = new Tile(this, new Vector2(coordinates.X * chunkSize + x, coordinates.Y * chunkSize + y));
                }
            }
        }

        public void Update()
        {
            foreach (Tile tile in Tiles)
            {
                tile.Update();
            }
        }


        public void Draw(Camera camera)
        {
            foreach (Tile tile in Tiles)
            {
                tile.Draw(camera);
            }
        }
    }
}
