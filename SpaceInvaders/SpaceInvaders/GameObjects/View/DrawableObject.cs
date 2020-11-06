using SpaceInvaders.GameObjects.Projectiles;
using System;
using System.Drawing;

namespace SpaceInvaders.GameObjects
{
    /// <summary>
    /// Represents a game object that can be drawed
    /// </summary>
    abstract class DrawableObject : GameObject
    {
        #region Fields

        /// <summary>
        /// Save an instance of transparent color
        /// </summary>
        private readonly static Color TRANSPARENT_COLOR = Color.FromArgb(0, 0, 0, 0);

        /// <summary>
        /// Image dimentions
        /// </summary>
        protected Vecteur2D ImageDimentions { get; private set; }

        /// <summary>
        /// Image to draw
        /// </summary>
        private readonly View.Display.Images.Drawable drawable;

        /// <summary>
        /// Function that make an action in the (x, y) pixel of the image
        /// </summary>
        /// <param name="x">x pixel</param>
        /// <param name="y">y pixel</param>
        private delegate bool PixelColorFunction(int x, int y);

        #endregion

        #region Constructor
        /// <summary>
        /// Allow to a game object to be draw
        /// </summary>
        /// <param name="team">team of the game object</param>
        /// <param name="coords">coordinates of the gameObject</param>
        /// <param name="drawable">image to draw</param>
        public DrawableObject(Team team, Vecteur2D coords, View.Display.Images.Drawable drawable) : base(team, coords)
        {
            this.drawable = GameException.RequireNonNull(drawable);
            ImageDimentions = new Vecteur2D(drawable.Width, drawable.Height);
        }
        #endregion

        #region Methods

        /// <summary>
        /// Draw the game objects in tge graphics
        /// </summary>
        /// <param name="gameInstance">gameInstance</param>
        /// <param name="graphics">graphics</param>
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            drawable.Draw(
                graphics, 
                new Vecteur2D(
                    (int) coords.X, 
                    (int) coords.Y
                )
            );
        }

        /// <summary>
        /// An gameObjet can is in collision with a projectile if 
        /// the projectile meet a non transparent pixel of the DrawableObject 
        /// </summary>
        /// <param name="projectile">projectile</param>
        /// <returns>Is the projectile in a non transparent pixel of the DrawableObject?</returns>
        public override bool CanCollision(ProjectileObject projectile)
        {
            GameException.RequireNonNull(projectile);

            if (team == projectile.team || !projectile.IsAlive()) return false;

            Rectangle intersect = Vecteur2D.Intersect(coords, ImageDimentions, projectile.coords, projectile.ImageDimentions);

            if (intersect.IsEmpty) return false;

            return IteratePixels(projectile, (x, y) => drawable.Image.GetPixel(x, y).A == 255);
        }

        /// <summary>
        /// By default, set transparent the common pixels
        /// </summary>
        public override void OnCollision(ProjectileObject projectile)
        {
            if (!CanCollision(projectile))
                throw new InvalidOperationException();

            IteratePixels(
                projectile,
                (x, y) => {
                    if (drawable.Image.GetPixel(x, y).A == 255)
                        drawable.Image.SetPixel(x, y, TRANSPARENT_COLOR);
                    return false;
                }
            );
        }

        /// <summary>
        /// Apply the PixelColorFunction in the common pixel
        /// </summary>
        /// <param name="projectile">the projectile</param>
        /// <param name="function">the action(s) to apply to the common pixel</param>
        /// <returns>True if function(x, t) return true</returns>
        private bool IteratePixels(ProjectileObject projectile, PixelColorFunction function)
        {
            Rectangle intersect = Vecteur2D.Intersect(coords, ImageDimentions, projectile.coords, projectile.ImageDimentions);

            int startX = (int)(intersect.X - coords.X);
            int startY = (int)(intersect.Y - coords.Y);

            for (int x = startX; x < startX + intersect.Width; x++)
                for (int y = startY; y < startY + intersect.Height; y++)
                    if (function(x, y))
                        return true;

            return false;
        }

        /// <summary>
        /// Return true if the gameObject is above of imageObject 
        /// </summary>
        /// <param name="imageObject">the imageObject to compare</param>
        /// <returns>True if the gameObject is above of imageObject </returns>
        public bool IsAbove(DrawableObject imageObject)
        {
            return coords.Y < GameException.RequireNonNull(imageObject).coords.Y;
        }

        /// <summary>
        /// By default, a drawable object is always alive
        /// <returns>True</returns>
        public override bool IsAlive()
        {
            return true;
        }

        #endregion
    }
}
