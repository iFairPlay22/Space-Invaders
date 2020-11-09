using System;

namespace SpaceInvaders.Util
{
    /// <summary>
    ///   Represents a class that manages random numbers
    /// </summary>
    class RandomNumbers
    {

        /// <summary>
        ///   A random instance to generate random numbers
        /// </summary>
        private static readonly Random random = new Random();

        /// <summary>
        ///   Generate a random number between min and max
        /// </summary>
        /// <param name="min">the min value</param>
        /// <param name="max">the max value</param>
        /// <returns> A random number between min and max </returns>
        public static int Randint(int min, int max)
        {
            lock (random)
            {
                return random.Next(min, max);
            }
        }
    }
}
