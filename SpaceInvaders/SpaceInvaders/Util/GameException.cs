using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class GameException
    {
        public static double RequireNonZero(double b)
        {
            if (b == 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            return b;
        }

        public static double RequirePositive(double b)
        {
            if (b < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            return b;
        }

        public static T RequireNonNull<T>(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException();
            return obj;
        }

        public static void RequireInGame(double x, double y, Game game)
        {
            RequireNonNull(game);
            if (!(0 <= x && x < game.gameSize.Width && 0 <= y && y < game.gameSize.Height))
                throw new ArgumentOutOfRangeException();
        }
    }
}
