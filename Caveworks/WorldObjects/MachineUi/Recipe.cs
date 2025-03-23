using System;

namespace Caveworks
{
    [Serializable]
    public class Recipe
    {
        public BaseItem[] Ingredients;
        public BaseItem Result;
        public int Time;


        public Recipe(BaseItem[] ingredients, BaseItem result, int time)
        {
            Ingredients = ingredients;
            Result = result;
            Time = time;
        }
    }
}
