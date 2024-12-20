﻿using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveworks
{
    public class MySoundEffect
    {
        private SoundEffect soundEffect;
        private float soundVolume;

        public MySoundEffect(SoundEffect soundEffect, float volume)
        {
            this.soundEffect = soundEffect;
            this.soundVolume = volume;
        }

        public void play(float volume)
        {
            SoundEffectInstance soundEffectInstance = soundEffect.CreateInstance();
            soundEffectInstance.Volume = soundVolume * Globals.GetGlobalVolume() * volume;
            soundEffectInstance.Play();
        }
    }
}
