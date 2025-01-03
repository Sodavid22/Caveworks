using Microsoft.Xna.Framework;


namespace Caveworks
{
    public static class FpsCounter
    {
        const int updateDelay = 200; // how often is the displayed number updated
        const int sampleSize = 100; // from how many frames is the avearage calculated

        static double[] frameTimes = new double[100]; // in milliseconds
        static int currentFrame = 0;
        static double timeSinceLastUpdate = 0;
        
        public static bool Active { get; private set; } = false;
        public static double Fps { get; private set; } = 0; // final FPS


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
            if (Active)
            {
                Active = false;
            }
            else
            {
                Active = true;
            }
        }
    }
}
