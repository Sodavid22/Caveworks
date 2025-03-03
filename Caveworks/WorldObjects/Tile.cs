using System;
using System.Collections.Generic;

namespace Caveworks
{
    [Serializable]
    public class Tile
    {
        public Chunk Chunk;
        public MyVector2Int Position;
        public BaseFloor Floor;
        public BaseWall Wall;
        public List<BaseItem> Items;
        public BaseBuilding Building;


        public Tile(Chunk chunk, MyVector2Int position)
        {
            Chunk = chunk;
            Position = position;
            Items = new List<BaseItem>();
        }


        public void Draw(Camera camera)
        {
            MyVector2 screenCords = camera.WorldToScreenCords(Position.ToMyVector2());
            if (screenCords.X > -camera.Scale && screenCords.Y > -camera.Scale && screenCords.X < GameWindow.Size.X && screenCords.Y < GameWindow.Size.Y)
            {
                if (Wall != null) { Wall.Draw(this, camera); }
                else if (Floor != null) { Floor.Draw(this, camera); }

                foreach (BaseItem item in Items)
                {
                    item.Draw(this, camera);
                }
            }
        }


        public void Delete()
        {
            Chunk.TileList[Position.X - Chunk.Index.X * Chunk.chunkSize, Position.Y - Chunk.Index.Y * Chunk.chunkSize] = null;
        }
    }
}
