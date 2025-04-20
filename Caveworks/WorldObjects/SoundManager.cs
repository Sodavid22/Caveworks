using System;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;

namespace Caveworks
{
    [Serializable]
    public class SoundManager
    {
        World World;
        const int SoundRange = 16;

        public SoundManager(World world)
        {
            World = world;
        }

        // 1 - burning
        // 2 - machine
        // 3 - advanced machine
        // 4 - drill

        public void Update()
        {
            float burningSoundDistance = GetNearestSound(1);
            float machineSoundDistance = GetNearestSound(2);
            float drillSoundDistance = GetNearestSound(4);

            ActivateSound(Sounds.Burning);
            ActivateSound(Sounds.Machine);
            ActivateSound(Sounds.Drill);

            SetVolume(Sounds.Burning, burningSoundDistance);
            SetVolume(Sounds.Machine, machineSoundDistance);
            SetVolume(Sounds.Drill, drillSoundDistance);
        }


        public void StopSound()
        {
            Sounds.Burning.SetVolume(0);
            Sounds.Machine.SetVolume(0);
            Sounds.Drill.SetVolume(0);
        }


        private float GetNearestSound(int sound)
        {
            float nearestSoundDistance = -1;
            Tile tile;
            for (int x = World.PlayerBody.Tile.Position.X - SoundRange; x < World.PlayerBody.Tile.Position.X + SoundRange; x++)
            {
                for (int y = World.PlayerBody.Tile.Position.Y - SoundRange; y < World.PlayerBody.Tile.Position.Y + SoundRange; y++)
                {
                    if (x > 0 && y > 0 && x < World.WorldDiameter && y < World.WorldDiameter)
                    {
                        tile = World.GlobalCordsToTile(new MyVector2Int(x, y));
                        if (tile.Building != null)
                        {
                            if (tile.Building.GetSoundType() == sound)
                            {
                                float soundDistance = MathF.Sqrt((x - World.PlayerBody.Tile.Position.X) * (x - World.PlayerBody.Tile.Position.X) + (y - World.PlayerBody.Tile.Position.Y) * (y - World.PlayerBody.Tile.Position.Y));
                                if (soundDistance < nearestSoundDistance || nearestSoundDistance == -1)
                                {
                                    nearestSoundDistance = soundDistance;
                                }
                            }
                        }
                    }
                }
            }
            return nearestSoundDistance;
        }


        private void ActivateSound(MySoundEffectInstance sound)
        {
            if (sound.GetState() == SoundState.Stopped || sound.GetState() == SoundState.Paused)
            {
                sound.Play();
            }
        }

        private void SetVolume(MySoundEffectInstance sound, float distance)
        {
            if (distance > 0)
            {
                float volume = (SoundRange - 1 - distance) / (SoundRange - 1);
                if (volume < 0)
                {
                    volume = 0;
                }
                sound.SetVolume(volume);
            }
            else
            {
                sound.SetVolume(0);
            }
        }
    }
}
