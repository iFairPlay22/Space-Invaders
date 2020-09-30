using System;
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
    }
}
