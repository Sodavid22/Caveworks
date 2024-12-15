using Microsoft.Xna.Framework;

namespace Caveworks
{
    public interface IScene
    {
        public void Update(GameTime gameTime) { }

        public void Draw(GameTime gameTime) { }
    }
}
