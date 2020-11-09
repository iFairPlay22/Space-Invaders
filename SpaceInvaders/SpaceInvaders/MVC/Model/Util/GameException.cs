using System;

namespace SpaceInvaders
{
    /// <summary>
    ///   Container of static functions that can raise exceptions
    /// </summary>
    class GameException
    {

        /// <summary>
        ///   Raise an ArgumentOutOfRangeException is the argument is 0
        /// </summary>
        /// <param name="b">a double</param>
        /// <returns> the argument </returns>
        public static double RequireNonZero(double b)
        {
            if (b == 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            return b;
        }
        
        /// <summary>
        ///   Raise an ArgumentOutOfRangeException is the argument is negative
        /// </summary>
        /// <param name="b">a double</param>
        /// <returns> the argument </returns>
        public static double RequirePositive(double b)
        {
            if (b < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            return b;
        }

        /// <summary>
        ///   Raise an ArgumentNullException is the argument is null
        /// </summary>
        /// <param name="obg">an object of any type</param>
        /// <returns> the argument </returns>
        public static T RequireNonNull<T>(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException();
            return obj;
        }
    }
}
