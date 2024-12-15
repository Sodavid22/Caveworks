using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Caveworks
{
    public interface IScene
    {
        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(GameTime gameTime) { }
    }
}
