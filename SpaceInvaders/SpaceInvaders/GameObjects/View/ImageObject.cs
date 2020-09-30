using SpaceInvaders.GameObjects.Projectiles;
using System.Drawing;
using System.Linq;

namespace SpaceInvaders.GameObjects
{
    abstract class ImageObject : GameObject
    {
        #region Fields

        /// <summary>
        /// Image dimentions
        /// </summary>
        protected Vecteur2D ImageDimentions { get; private set; }

        /// <summary>
        /// Image to draw
        /// </summary>
        private readonly Bitmap image;

        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="coords">Position in pixels</param>
        /// <param name="image">Image to draw</param>
        
        public ImageObject(Team team, Vecteur2D coords, Bitmap image) : base(team, coords)
        {
            this.image = GameException.RequireNonNull(image);
            ImageDimentions = new Vecteur2D(image.Width, image.Height);
        }
        #endregion

        #region Methods

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            graphics.DrawImage(image, (float)coords.X, (float)coords.Y, image.Width, image.Height);
        }

        public override bool CanCollision(ProjectileObject projectile)
        {
            GameException.RequireNonNull(projectile);

            if (team == projectile.team || !projectile.IsAlive()) return false;

            Rectangle intersect = Rectangle.Intersect(
                new Rectangle(
                    new Point(
                        (int) coords.X,
                        (int) coords.Y
                    ), 
                    new Size(
                        (int) ImageDimentions.X,
                        (int) ImageDimentions.Y
                    )
                ),
                new Rectangle(
                    new Point(
                        (int) projectile.coords.X,
                        (int) projectile.coords.Y
                    ),
                    new Size(
                        (int) projectile.ImageDimentions.X,
                        (int) projectile.ImageDimentions.Y
                    )
                )
            );

            if (intersect.IsEmpty) return false;

            for (int x = 0; x < intersect.Width; x++)
                for (int y = 0; y < intersect.Height; y++)
                    if (image.GetPixel((int) ImageDimentions.X - x - 1, (int) ImageDimentions.Y - y - 1).A == 255)
                        return true;

            return false;
        }

        private bool PixelCollision(ProjectileObject projectile)
        {
            return true;
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
