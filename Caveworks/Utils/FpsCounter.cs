using Microsoft.Xna.Framework;

namespace Caveworks
{
    public static class FpsCounter
    {
        static bool active = false;

        static double[] frameTimes = new double[100]; // in milliseconds
        static int currentFrame = 0;
        static int sampleSize = 100; // from how many frames is the avearage calculated

        static double timeSinceLastUpdate = 0;
        static int updateDelay = 200; // how often is the avearage updated in milliseconds
        static double Fps = 0; // final FPS

        public static void Update(GameTime gameTime)
        {

            timeSinceLastUpdate += gameTime.ElapsedGameTime.TotalMilliseconds;
            frameTimes[currentFrame] = gameTime.ElapsedGameTime.TotalMilliseconds;
            currentFrame++;

            if (currentFrame == sampleSize)
            {
                currentFrame = 0;
            }

            if (timeSinceLastUpdate > updateDelay)
            {
                timeSinceLastUpdate = 0;
                double totalUpdateTime = 0;
                foreach (var time in frameTimes)
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
