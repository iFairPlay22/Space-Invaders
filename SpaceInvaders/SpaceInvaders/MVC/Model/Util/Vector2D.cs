using System;
using System.Drawing;

namespace SpaceInvaders
{
    public class Vector2D
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Norme { get; private set; }

        public Vector2D() : this(0, 0) { }

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
            Norme = Math.Sqrt(x * x + y * y);
        }

        public static Vector2D operator +(Vector2D a, Vector2D b)
        {
            return new Vector2D(GameException.RequireNonNull(a).X + GameException.RequireNonNull(b).X, a.Y + b.Y);
        }

        public static Vector2D operator -(Vector2D a, Vector2D b)
        {
            return new Vector2D(GameException.RequireNonNull(a).X - GameException.RequireNonNull(b).X, a.Y - b.Y);

        }

        public static Vector2D operator -(Vector2D a)
        { 
            return new Vector2D(-GameException.RequireNonNull(a).X, -a.Y);

        }

        public static Vector2D operator *(Vector2D a, double b)
        {
            return new Vector2D(GameException.RequireNonNull(a).X * b, a.Y * b);
        }

        public static Vector2D operator *(double a, Vector2D b)
        {
            return GameException.RequireNonNull(b) * a;
        }

        public static Vector2D operator /(Vector2D a, double b)
        {
            
            return new Vector2D(GameException.RequireNonNull(a).X / GameException.RequireNonZero(b), a.Y / b);
        }

        public static Rectangle Intersect(Vector2D v1, Vector2D d1, Vector2D v2, Vector2D d2)
        {
            return Rectangle.Intersect(
                new Rectangle(
                    new Point( (int)v1.X, (int)v1.Y ),
                    new Size( (int)d1.X, (int)d1.Y )
                ),
                new Rectangle(
                    new Point( (int)v2.X, (int)v2.Y ),
                    new Size( (int)d2.X, (int)d2.Y )
                )
            );
        }
    }
}
