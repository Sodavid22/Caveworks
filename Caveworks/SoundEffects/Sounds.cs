using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Caveworks.SoundEffects
{
    public class Sounds // use instances, not originals !!!
    {
        public static MySoundEffect buttonClick;
        public static MySoundEffect buttonDecline;
        public static MySoundEffect buttonClick2;

        public static void Load(ContentManager contentManager)
        {
            Sounds.buttonClick = new MySoundEffect(contentManager.Load<SoundEffect>("SoundEffects/UI/modern-ui-hover"), 0.5f);
            Sounds.buttonDecline = new MySoundEffect(contentManager.Load<SoundEffect>("SoundEffects/UI/modern-ui-decline"), 0.5f);
            Sounds.buttonClick2 = new MySoundEffect(contentManager.Load<SoundEffect>("SoundEffects/UI/ui-button-click"), 0.5f);
        }
    }
}
