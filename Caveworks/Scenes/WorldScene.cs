using Microsoft.Xna.Framework;

namespace Caveworks
{
    public class WorldScene : IScene
    {
        public WorldScene() 
        {

        } 

        public void Update(GameTime gameTime)
        {
            Globals.World.Update(gameTime);
        }


        public void Draw(GameTime gameTime)
        {
            Globals.World.Draw();
        }
    }
}
