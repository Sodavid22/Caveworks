
using System;

namespace Caveworks
{
    [Serializable]
    public class BaseMachine : BaseBuilding
    {
        public BaseMachine(Tile tile, int size, int inventorySize) : base(tile, size)
        {
            this.Inventory = new Inventory(inventorySize, Globals.World.Player);
        }
    }
}
