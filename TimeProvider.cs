using System.Diagnostics;

namespace AudioPlayer
{
    public class TimeProvider
    {
        private readonly Stopwatch stopwatch;
        private long lastFrameTime;

        public TimeProvider()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            lastFrameTime = stopwatch.ElapsedTicks;
        }

        /// <summary>
        /// Gets the time elapsed since the last frame in seconds
        /// </summary>
        /// <returns>Delta time in seconds</returns>
        public double GetDeltaTime()
        {
            long currentFrameTime = stopwatch.ElapsedTicks;
            double deltaTime = (currentFrameTime - lastFrameTime) / (double)Stopwatch.Frequency;
            lastFrameTime = currentFrameTime;
            return deltaTime;
        }

        /// <summary>
        /// Gets the total time elapsed since the TimeProvider was created
        /// </summary>
        /// <returns>Total time in seconds</returns>
        public double GetTotalTime()
        {
            return stopwatch.ElapsedTicks / (double)Stopwatch.Frequency;
        }
    }
}  