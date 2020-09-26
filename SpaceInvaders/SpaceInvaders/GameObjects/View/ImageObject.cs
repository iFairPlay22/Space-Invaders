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
            if (team == projectile.team || !projectile.IsAlive()) return false;

            Vecteur2D projectileCoords = GameException.RequireNonNull(projectile).coords;
            Vecteur2D projectileDimentions = projectile.ImageDimentions;

            Vecteur2D[] res = {
                projectileCoords,
                new Vecteur2D(projectileCoords.X + projectileDimentions.X, projectileCoords.Y),
                new Vecteur2D(projectileCoords.X, projectileCoords.Y + projectileDimentions.Y),
                new Vecteur2D(projectileCoords.X + projectileDimentions.X, projectileCoords.Y + projectileDimentions.Y),
            };

            return res.Any(
               v => 
                    (coords.X <= GameException.RequireNonNull(v).X && v.X < coords.X + ImageDimentions.X) && 
                    (coords.Y <= v.Y && v.Y < coords.Y + ImageDimentions.Y)
           );
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
