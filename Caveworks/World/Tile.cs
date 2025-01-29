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
        public List<BaseCreature> Creatures;
        public List<BaseItem> Items;


        public Tile(Chunk chunk, MyVector2Int position)
        {
            Chunk = chunk;
            Position = position;
            Creatures = new List<BaseCreature>();
            Items = new List<BaseItem>();
        }


        public void Draw(Camera camera)
        {
            MyVector2 screenCords = camera.WorldToScreenCords(Position.ToMyVector2());
            if (screenCords.X > -camera.Scale && screenCords.Y > -camera.Scale && screenCords.X < GameWindow.WindowSize.X && screenCords.Y < GameWindow.WindowSize.Y)
            {
                if (Wall != null) { Wall.Draw(this, camera); }
                else if (Floor != null) { Floor.Draw(this, camera); }
                else { Chunk.World.defaultFloor.Draw(this, camera); };

                foreach (BaseItem item in Items)
                {
                    item.Draw(this, camera);
                }
            }
        }


        public void AddCreature(BaseCreature creature)
        {
            Creatures.Add(creature);
            Chunk.Creatures.Add(creature);
        }

        public void AddItem(BaseItem item)
        {
            Items.Add(item);
        }
    }
}
