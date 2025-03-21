using System;
using System.Collections.Generic;

namespace Caveworks
{
    [Serializable]
    public class BaseBuilding
    {

        public Tile Tile;
        public MyVector2Int Position;
        public MyVector2Int Rotation;
        public static int Size;
        public Inventory Inventory;


        public BaseBuilding(Tile tile, int size) // TODO fix placing large buildings on map edge
        {
            Tile = tile;
            this.Position = tile.Position;


            Tile.Chunk.Buildings.Add(this);
            for (int x = 0; x <= size - 1; x++) 
            {
                for (int y = 0; y <= size - 1; y++)
                {
                    Globals.World.GlobalCordsToTile(new MyVector2Int(Position.X + x, Position.Y + y)).Building = this;
                }
            }
        }

        public virtual int GetLightLevel() { return 0; }

        public virtual bool HasCollision() { return false; }

        public virtual bool HasUI() { return false; }

        public virtual void OpenUI() { return; }

        public virtual void UpdateUI() { return; }

        public virtual void DrawUI() { return; }

        public virtual void CloseUI() { return; }

        public virtual BaseItem ToItem() { return null; }

        public static void DeleteBuilding(BaseBuilding building)
        {
            building.Tile.Building = null;
            building.Tile.Chunk.Buildings.Remove(building);
        }


        public virtual void Update(float deltaTime) { }


        public virtual void Draw(Camera camera, float deltaTime) { }
    }
}
