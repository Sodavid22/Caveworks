using System;
using System.Diagnostics;

namespace Caveworks
{
    [Serializable]
    public class BaseBuilding
    {

        public Tile Tile;
        public MyVector2Int Position;
        public MyVector2Int Rotation;
        public Inventory Inventory;
        public RecipeCrafter Crafter;


        public BaseBuilding(Tile tile, int size) // TODO fix placing large buildings on map edge
        {
            Tile = tile;
            Position = tile.Position;


            Tile.Chunk.Buildings.Add(this);
            for (int x = 0; x < size; x++) 
            {
                for (int y = 0; y < size; y++)
                {
                    Globals.World.GlobalCordsToTile(new MyVector2Int(Position.X + x, Position.Y + y)).Building = this;
                }
            }
        }

        public virtual bool AccteptsItems(BaseBuilding building) { return false; } // does this building accept items

        public virtual int GetLightLevel() { return 0; } // how bright is the building

        public virtual bool HasCollision() { return false; } // can player collide with it

        public virtual bool IsTransportBuilding() { return false; } // is for transporting items

        public virtual int GetSize() { return 1; } // size

        public virtual bool HasUI() { return false; } // can it be interacted with

        public virtual void OpenUI() { return; }

        public virtual void UpdateUI() { return; }

        public virtual void DrawUI() { return; }

        public virtual void CloseUI() { return; }

        public virtual BaseItem ToItem() { return null; }

        public static void DeleteBuilding(BaseBuilding building)
        {
            Tile clearedTile;

            for (int x = building.Tile.Position.X; x < building.Tile.Position.X + building.GetSize(); x++)
            {
                for (int y = building.Tile.Position.Y; y < building.Tile.Position.Y + building.GetSize(); y++)
                {
                    clearedTile = Globals.World.GlobalCordsToTile(new MyVector2Int(x, y));
                    clearedTile.Building = null;
                }
            }
            building.Tile.Chunk.Buildings.Remove(building);
        }


        public virtual void Update(float deltaTime) { }


        public virtual void Draw(Camera camera, float deltaTime) { }
    }
}
