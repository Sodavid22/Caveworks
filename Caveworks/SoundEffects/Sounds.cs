using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Caveworks
{
    public class Sounds // use instances, not originals !!!
    {
        public static MySoundEffect ButtonClick;
        public static MySoundEffect ButtonDecline;
        public static MySoundEffect ButtonClick2;

        public static void Load(ContentManager contentManager)
        {
            ButtonClick = new MySoundEffect(contentManager.Load<SoundEffect>("SoundEffects/UI/modern-ui-hover"), 0.5f);
            ButtonDecline = new MySoundEffect(contentManager.Load<SoundEffect>("SoundEffects/UI/modern-ui-decline"), 0.5f);
            ButtonClick2 = new MySoundEffect(contentManager.Load<SoundEffect>("SoundEffects/UI/ui-button-click"), 0.5f);
        }
    }
}
