using System;
using System.Drawing;
using System.Linq;

namespace SpaceInvaders
{
    public class Vecteur2D
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Norme { get; private set; }

        public Vecteur2D() : this(0, 0) { }

        public Vecteur2D(double x, double y)
        {
            X = x;
            Y = y;
            Norme = Math.Sqrt(x * x + y * y);
        }

        public static Vecteur2D operator +(Vecteur2D a, Vecteur2D b)
        {
            return new Vecteur2D(GameException.RequireNonNull(a).X + GameException.RequireNonNull(b).X, a.Y + b.Y);
        }

        public static Vecteur2D operator -(Vecteur2D a, Vecteur2D b)
        {
            return new Vecteur2D(GameException.RequireNonNull(a).X - GameException.RequireNonNull(b).X, a.Y - b.Y);

        }

        public static Vecteur2D operator -(Vecteur2D a)
        { 
            return new Vecteur2D(-GameException.RequireNonNull(a).X, -a.Y);

        }

        public static Vecteur2D operator *(Vecteur2D a, double b)
        {
            return new Vecteur2D(GameException.RequireNonNull(a).X * b, a.Y * b);
        }

        public static Vecteur2D operator *(double a, Vecteur2D b)
        {
            return GameException.RequireNonNull(b) * a;
        }

        public static Vecteur2D operator /(Vecteur2D a, double b)
        {
            
            return new Vecteur2D(GameException.RequireNonNull(a).X / GameException.RequireNonZero(b), a.Y / b);
        }

        public static Rectangle Intersect(Vecteur2D v1, Vecteur2D d1, Vecteur2D v2, Vecteur2D d2)
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
