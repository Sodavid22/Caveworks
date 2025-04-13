﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Caveworks
{
    [Serializable]
    public class ResearchManager
    {
        public List<BaseItem> CurrentItemGoals;
        public List<BaseItem> RemainingItems;
        public List<List<BaseItem>> ResearchGoals = new List<List<BaseItem>>();
        public int CurrentResearch = 0;
        public int ResearchTime;

        static int BoxWidth = 140;
        static int BoxHeight = 40;
        static int Border = 2;


        public ResearchManager()
        {
            ResearchGoals.Add(new List<BaseItem> { new RawStoneItem(4), new RawIronOreItem(2) });
            ResearchGoals.Add(new List<BaseItem> { new RawStoneItem(8), new RawIronOreItem(4) });
            ResearchGoals.Add(new List<BaseItem> { new RawStoneItem(16), new RawIronOreItem(8) });

            CurrentItemGoals = ResearchGoals[CurrentResearch];
            RemainingItems = Cloning.DeepClone(ResearchGoals[CurrentResearch]);
        }


        public void Update()
        {
            bool finished = true;
            foreach (BaseItem item in RemainingItems)
            {
                if (item.Count > 0)
                {
                    finished = false;
                    break;
                }
            }

            if (finished)
            {
                CurrentResearch += 1;
                if (CurrentResearch < ResearchGoals.Count)
                {
                    CurrentItemGoals = ResearchGoals[CurrentResearch];
                    RemainingItems = Cloning.DeepClone(ResearchGoals[CurrentResearch]);
                }

                Globals.World.Player.CloseUi();
                if (CurrentResearch == 1)
                {
                    Globals.World.Player.Crafter.RecipeList.Add(RecipeList.Stonification);
                }
            }
        }


        public void Draw()
        {
            if (CurrentResearch < ResearchGoals.Count)
            {
                for (int i = 0; i < CurrentItemGoals.Count; i++)
                {
                    Game.MainSpriteBatch.Draw(Textures.EmptyTexture, new Rectangle((int)GameWindow.Size.X - BoxWidth - Border, i * (BoxHeight + Border), BoxWidth + Border, BoxHeight + Border), Color.Black);
                    Game.MainSpriteBatch.Draw(Textures.EmptyTexture, new Rectangle((int)GameWindow.Size.X - BoxWidth, i * (BoxHeight + Border), BoxWidth, BoxHeight), Color.FromNonPremultiplied(Globals.InventoryBoxColor));

                    BaseItem item = CurrentItemGoals[i];
                    Game.MainSpriteBatch.Draw(item.GetTexture(), new Rectangle((int)GameWindow.Size.X - BoxWidth + Border, i * (BoxHeight + Border) + Border, BoxHeight - Border * 2, BoxHeight - Border * 2), Color.White);
                    Game.MainSpriteBatch.DrawString(Fonts.MediumFont, (item.Count - RemainingItems[i].Count).ToString() + "/" + item.Count.ToString(), new Vector2((int)GameWindow.Size.X - BoxWidth + BoxHeight + 8, i * (BoxHeight + Border) + 8), Color.White);
                }

            }
        }
    }
}
