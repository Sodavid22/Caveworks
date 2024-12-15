using Microsoft.Xna.Framework;

namespace Caveworks
{
    public static class FpsCounter
    {
        private static bool active = false;

        private static double[] frameTimes = new double[100]; // in milliseconds
        private static int currentFrame = 0;
        private static int sampleSize = 100; // from how many frames is the avearage calculated

        private static double timeSinceLastUpdate = 0;
        private static int updateDelay = 200; // how often is the avearage updated in milliseconds
        private static double Fps = 0; // final FPS

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
