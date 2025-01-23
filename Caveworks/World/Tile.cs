using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Caveworks
{
    [Serializable]
    public class Tile
    {
        public Chunk Chunk;
        public MyVector2Int Coordinates;
        public BaseFloor Floor;
        public BaseWall Wall;
        public List<BaseCreature> Creatures;

        
        public Tile(Chunk chunk, MyVector2Int coordinates)
        {
            Chunk = chunk;
            Coordinates = coordinates;
            Creatures = new List<BaseCreature>();
        }


        public void Draw(Camera camera)
        {
           if (Wall != null) { Wall.Draw(this, camera); }
           else { Floor.Draw(this, camera); }
        }


        public void AddCreature(BaseCreature creature)
        {
            Creatures.Add(creature);
            this.Chunk.Creatures.Add(creature);
        }
    }
}
