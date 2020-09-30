using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.GameObjects.View.Display.Images;
using System;
using System.Drawing;

namespace SpaceInvaders.GameObjects
{
    abstract class ImageObject : GameObject
    {
        #region Fields

        private readonly static Color TRANSPARENT_COLOR = Color.FromArgb(0, 0, 0, 0);
        /// <summary>
        /// Image dimentions
        /// </summary>
        protected Vecteur2D ImageDimentions { get; private set; }

        /// <summary>
        /// Image to draw
        /// </summary>
        private readonly Drawable drawable;

        private delegate bool PixelColorFunction(int x, int y);

        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="coords">Position in pixels</param>
        /// <param name="image">Image to draw</param>

        public ImageObject(Team team, Vecteur2D coords, Drawable drawable) : base(team, coords)
        {
            this.drawable = GameException.RequireNonNull(drawable);
            ImageDimentions = new Vecteur2D(drawable.Width, drawable.Height);
        }
        #endregion

        #region Methods

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

        public override bool CanCollision(ProjectileObject projectile)
        {
            GameException.RequireNonNull(projectile);

            if (team == projectile.team || !projectile.IsAlive()) return false;

            Rectangle intersect = Vecteur2D.Intersect(coords, ImageDimentions, projectile.coords, projectile.ImageDimentions);

            if (intersect.IsEmpty) return false;

            return IteratePixels(projectile, (x, y) => drawable.Image.GetPixel(x, y).A == 255);
        }

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

        public bool IsAbove(ImageObject imageObject)
        {
            return coords.Y < GameException.RequireNonNull(imageObject).coords.Y;
        }

        public override bool IsAlive()
        {
            return true;
        }

        #endregion
    }
}
