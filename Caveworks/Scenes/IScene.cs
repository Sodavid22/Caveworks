using Microsoft.Xna.Framework;


namespace Caveworks
{
    public interface IScene
    {
        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(GameTime gameTime) { }
    }
}
