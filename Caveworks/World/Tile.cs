using Microsoft.Xna.Framework;
using System;

namespace Caveworks
{
    [Serializable]
    public class Tile
    {
        public Chunk Chunk {  get; set; }
        public MyVector2 Coordinates { get; set; }
        public BaseFloor Floor { get; set; }
        public BaseWall Wall { get; set; }

        
        public Tile(Chunk chunk, MyVector2 coordinates)
        {
            Chunk = chunk;
            Coordinates = new MyVector2(coordinates.X, coordinates.Y);
        }


        public void Update()
        {

        }


        public void Draw(Camera camera)
        {
           if (Wall != null) { Wall.Draw(camera); }
           else { Floor.Draw(camera); }
        }
    }
}
