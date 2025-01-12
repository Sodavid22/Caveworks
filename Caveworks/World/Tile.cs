using Microsoft.Xna.Framework;

namespace Caveworks
{
    public class Tile
    {
        public Chunk Chunk {  get; set; }
        public Vector2 Coordinates { get; set; }
        public FloorBase Floor { get; set; }

        
        public Tile(Chunk chunk, Vector2 coordinates)
        {
            Chunk = chunk;
            Coordinates = new Vector2();
            Floor = new StoneFloor(this);
        }


        public void Update()
        {

        }


        public void Draw(Camera camera)
        {
            Floor.Draw(camera);
        }
    }
}
