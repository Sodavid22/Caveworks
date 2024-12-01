using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveworks
{
    internal class Sounds // use instances, not originals !!!
    {
        public static SoundEffect buttonClick;
        public static SoundEffectInstance buttonClickInstance;

        public static void AdjustVolume()
        {
            buttonClickInstance = buttonClick.CreateInstance();
            buttonClickInstance.Volume = 0.5f;
        }
    }
}
