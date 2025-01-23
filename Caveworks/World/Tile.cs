using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Caveworks
{
    [Serializable]
    public class Tile
    {
        public Chunk Chunk;
        public MyVector2 ChunkCoordinates;
        public MyVector2 GlobalCoordinates;
        public BaseFloor Floor;
        public BaseWall Wall;
        public List<BaseCreature> Creatures;

        
        public Tile(Chunk chunk, MyVector2 coordinates)
        {
            Chunk = chunk;
            ChunkCoordinates = coordinates;
            GlobalCoordinates = new MyVector2(Chunk.Coordinates.X * Chunk.chunkSize + ChunkCoordinates.X, Chunk.Coordinates.Y * Chunk.chunkSize + ChunkCoordinates.Y);
            Creatures = new List<BaseCreature>();
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
