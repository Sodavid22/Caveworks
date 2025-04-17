using Microsoft.Xna.Framework.Audio;


namespace Caveworks
{
    public class MySoundEffectInstance
    {
        readonly SoundEffectInstance soundEffectInstance;
        readonly float baseVolume;
        readonly float volume;


        public MySoundEffectInstance(SoundEffect soundEffect, float baseVolume)
        {
            soundEffectInstance = soundEffect.CreateInstance();
            soundEffectInstance.IsLooped = true;
            this.baseVolume = baseVolume;
            soundEffectInstance.Volume = baseVolume * 1;
        }


        public void Play()
        {
            soundEffectInstance.Play();
        }

        public void Stop()
        {
            soundEffectInstance.Pause();
        }

        public void SetVolume(float volume)
        {
            soundEffectInstance.Volume = baseVolume * volume;
        }


        public SoundState GetState()
        {
            return soundEffectInstance.State;
        }
    }
}
