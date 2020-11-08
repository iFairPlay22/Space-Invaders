﻿using System;

namespace SpaceInvaders.Util
{
    class RandomNumbers
    {
        private static readonly Random random = new Random();

        public static int Randint(int min, int max)
        {
            lock (random)
            {
                return random.Next(min, max);
            }
        }
    }
}