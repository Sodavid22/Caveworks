using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caveworks
{
    public class FpsCounter
    {
        static bool active = false;

        static double[] updateTimes = new double[100]; // in milliseconds
        static int frame = 0;
        static int sampleSize = 100; // from how many frames is the avearage calculated

        static double timeSinceLastUpdate = 0;
        static int updateSpeed = 200; // how often is the avearage updated in milliseconds
        static double Fps = 0; // final FPS

        public static void Update(GameTime gameTime)
        {
            updateTimes[frame] = gameTime.ElapsedGameTime.TotalMilliseconds;
            frame++;

            if (frame == sampleSize)
            {
                frame = 0;
            }

            timeSinceLastUpdate += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceLastUpdate > updateSpeed)
            {
                timeSinceLastUpdate = 0;
                double totalUpdateTime = 0;
                foreach (var time in updateTimes)
                {
                    totalUpdateTime += time;
                }
                Fps = 1000 / (totalUpdateTime / sampleSize);
            }
        }

        public static void Toggle()
        {
            if (active)
            {
                active = false;
            }
            else
            {
                active = true;
            }
        }

        public static double GetFps()
        {
            return Fps;
        }

        public static bool IsActive()
        {
            return active;
        }
    }
}
