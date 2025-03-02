using Microsoft.Xna.Framework.Audio;


namespace Caveworks
{
    public class MySoundEffect
    {
        readonly SoundEffect soundEffect;
        readonly float soundVolume;


        public MySoundEffect(SoundEffect soundEffect, float volume)
        {
            this.soundEffect = soundEffect;
            this.soundVolume = volume;
        }


        public void Play(float volume)
        {
            SoundEffectInstance soundEffectInstance = soundEffect.CreateInstance();
            soundEffectInstance.Volume = soundVolume * Globals.GlobalVolume * volume;
            soundEffectInstance.Play();
        }
    }
}
