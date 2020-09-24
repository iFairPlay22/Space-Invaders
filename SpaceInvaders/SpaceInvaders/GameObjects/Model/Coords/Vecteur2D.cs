using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    public class Vecteur2D
    {
        public double x { get; private set; }
        public double y { get; private set; }
        public double Norme { get; private set; }

        public Vecteur2D() : this(0, 0) { }

        public Vecteur2D(double x, double y)
        {
            this.x = x;
            this.y = y;
            this.Norme = Math.Sqrt(x * x + y * y);
        }

        public static Vecteur2D operator +(Vecteur2D a, Vecteur2D b)
        {
            return new Vecteur2D(GameException.RequireNonNull(a).x + GameException.RequireNonNull(b).x, a.y + b.y);
  
        }

        public static Vecteur2D operator -(Vecteur2D a, Vecteur2D b)
        {
            return new Vecteur2D(GameException.RequireNonNull(a).x - GameException.RequireNonNull(b).x, a.y - b.y);

        }

        public static Vecteur2D operator -(Vecteur2D a)
        { 
            return new Vecteur2D(-GameException.RequireNonNull(a).x, -a.y);

        }

        public static Vecteur2D operator *(Vecteur2D a, double b)
        {
            return new Vecteur2D(GameException.RequireNonNull(a).x * b, a.y * b);
        }

        public static Vecteur2D operator *(double a, Vecteur2D b)
        {
            return b * a;
        }

        public static Vecteur2D operator /(Vecteur2D a, double b)
        {
            
            return new Vecteur2D(GameException.RequireNonNull(a).x / GameException.RequireNonZero(b), a.y / b);
        }
    }
}
