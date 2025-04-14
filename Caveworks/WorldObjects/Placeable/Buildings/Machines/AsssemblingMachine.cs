﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Caveworks
{
    [Serializable]
    public class AsssemblingMachine : BaseMachine
    {
        public AsssemblingMachine(Tile tile):base(tile, 3, 4, new List<Recipe> { RecipeList.IronGear, RecipeList.CopperWire, RecipeList.GreenCircuit, RecipeList.StoneFurnace,
            RecipeList.AssemblingMachine, RecipeList.Drill, RecipeList.ResearchLab, RecipeList.SlowBelt, RecipeList.CrossRoad, RecipeList.Splitter,
            RecipeList.ElectricLight, RecipeList.IronChest
        })
        {

        }


        public override int GetSize() { return 3; }


        public override void Update(float deltaTime)
        {
            Crafter.Update(deltaTime);
        }


        public override BaseItem ToItem()
        {
            return new AsseblingMachineItem(1);
        }


        public override void Draw(Camera camera, float deltaTime)
        {
            MyVector2Int screenCoordinates = camera.WorldToScreenCords(Position);
            Game.WallSpritebatch.Draw(Textures.AssemblingMachine, new Rectangle(screenCoordinates.X, screenCoordinates.Y, camera.Scale * 3, camera.Scale * 3), Color.White);
        }
    }
}
