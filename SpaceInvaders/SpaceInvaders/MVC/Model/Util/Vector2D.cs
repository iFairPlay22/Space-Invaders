using System;
using System.Drawing;

namespace SpaceInvaders
{

    /// <summary>
    ///   Represents a 2 dimentions vector (x, y)
    /// </summary>
    public class Vector2D
    {

        /// <summary>
        ///   X value
        /// </summary>
        public double X { get; private set; }

        /// <summary>
        ///   Y value
        /// </summary>
        public double Y { get; private set; }

        /// <summary>
        ///   Norm of the vector
        /// </summary>
        public double Norm { get; private set; }

        /// <summary>
        ///   Create a (0, 0) vector
        /// </summary>
        public Vector2D() : this(0, 0) { }

        /// <summary>
        ///  Create a vector (x, y)
        /// </summary>
        /// <param name="x">the x value</param>
        /// <param name="u">the y value</param>
        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
            Norm = Math.Sqrt(x * x + y * y);
        }

        /// <summary>
        ///   (a.x, a.y) + (b.x, b.y) = (a.x + b.x, a.y + b.y)
        /// </summary>
        /// <param name="a">a vector</param>
        /// <param name="b">a vector</param>
        /// <returns> A new vector </returns>
        public static Vector2D operator +(Vector2D a, Vector2D b)
        {
            GameException.RequireNonNull(a);
            GameException.RequireNonNull(b);
            return new Vector2D(a.X + b.X, a.Y + b.Y);
        }

        /// <summary>
        ///   (a.x, a.y) - (b.x, b.y) = (a.x - b.x, a.y - b.y)
        /// </summary>
        /// <param name="a">a vector</param>
        /// <param name="b">a vector</param>
        /// <returns> A new vector </returns>
        public static Vector2D operator -(Vector2D a, Vector2D b)
        {
            GameException.RequireNonNull(a);
            GameException.RequireNonNull(b);
            return new Vector2D(a.X - b.X, a.Y - b.Y);

        }

        /// <summary>
        ///   - (a.x, a.y) = (-a.x, -a.y)
        /// </summary>
        /// <param name="a">a vector</param>
        /// <returns> A new vector </returns>
        public static Vector2D operator -(Vector2D a)
        {
            GameException.RequireNonNull(a);
            return new Vector2D(-a.X, -a.Y);

        }

        /// <summary>
        ///   (a.x, a.y) * (b.x, b.y) = (a.x * b.x, a.y * b.y)
        /// </summary>
        /// <param name="a">a vector</param>
        /// <param name="b">a vector</param>
        /// <returns> A new vector </returns>
        public static Vector2D operator *(Vector2D a, double b)
        {
            GameException.RequireNonNull(a);
            return new Vector2D(a.X * b, a.Y * b);
        }

        /// <summary>
        ///   a * (b.x, b.y) = (a * b.x, a * b.y)
        /// </summary>
        /// <param name="a">a double</param>
        /// <param name="b">a vector</param>
        /// <returns> A new vector </returns>
        public static Vector2D operator *(double a, Vector2D b)
        {
            return GameException.RequireNonNull(b) * a;
        }

        /// <summary>
        ///   (a.x, a.y) / (b.x, b.y) = (a.x / b.x, a.y / b.y)
        /// </summary>
        /// <param name="a">a vector</param>
        /// <param name="b">a vector</param>
        /// <returns> A new vector </returns>
        public static Vector2D operator /(Vector2D a, double b)
        {
            GameException.RequireNonNull(a);
            GameException.RequireNonZero(b);
            return new Vector2D(a.X / b, a.Y / b);
        }

        /// <summary>
        ///   Is there an intersectio, between the two squares ?
        /// </summary>
        /// <param name="position1">the top left position (x, y) of the first object</param>
        /// <param name="size1">the vector (width, height) of the first object</param>
        /// <param name="position2">the top left position (x, y) of the second object</param>
        /// <param name="size2">the vector (width, height) of the second object</param>
        /// <returns> Is there an intersectio, between the two squares ? </returns>
        public static Rectangle Intersect(Vector2D position1, Vector2D size1, Vector2D position2, Vector2D size2)
        {
            return Rectangle.Intersect(
                new Rectangle(
                    new Point( (int)position1.X, (int)position1.Y ),
                    new Size( (int)size1.X, (int)size1.Y )
                ),
                new Rectangle(
                    new Point( (int)position2.X, (int)position2.Y ),
                    new Size( (int)size2.X, (int)size2.Y )
                )
            );
        }
    }
}
