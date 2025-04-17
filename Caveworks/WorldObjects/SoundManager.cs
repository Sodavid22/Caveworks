using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using SharpDX.Direct3D9;

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
        public void Update()
        {
            float BurningSoundDistance;
            float NearestBurningSound = -1;
            Tile tile;

            for (int x = World.PlayerBody.Tile.Position.X - SoundRange; x < World.PlayerBody.Tile.Position.X + SoundRange; x++)
            {
                for (int y = World.PlayerBody.Tile.Position.Y - SoundRange; y < World.PlayerBody.Tile.Position.X + SoundRange; y++)
                {
                    tile = World.GlobalCordsToTile(new MyVector2Int(x, y));
                    if (tile.Building != null)
                    {
                        if (tile.Building.GetSoundType() == 1)
                        {
                            BurningSoundDistance = MathF.Sqrt((x - World.PlayerBody.Tile.Position.X) * (x - World.PlayerBody.Tile.Position.X) + (y - World.PlayerBody.Tile.Position.Y) * (y - World.PlayerBody.Tile.Position.Y));
                            if (BurningSoundDistance < NearestBurningSound || NearestBurningSound == -1)
                            {
                                NearestBurningSound = BurningSoundDistance;
                            }
                        }
                    }
                }
            }

            if (Sounds.Burning.GetState() == SoundState.Stopped || Sounds.Burning.GetState() == SoundState.Paused)
            {
                Sounds.Burning.Play();
            }

            if (NearestBurningSound > 0)
            {
                float volume = (SoundRange - 1 - NearestBurningSound) / (SoundRange - 1);
                if (volume < 0)
                {
                    volume = 0;
                }
                Sounds.Burning.SetVolume(volume);
            }
            else
            {
                Sounds.Burning.SetVolume(0);
            }
        }


        public void StopSound()
        {
            Sounds.Burning.SetVolume(0);
        }
    }
}
