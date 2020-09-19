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
            graphics.DrawImage(image, (float)coords.x, (float)coords.y, image.Width, image.Height);
        }

        public override bool CanCollision(ProjectileObject projectile)
        {
            if (team == projectile.team) return false;

            Vecteur2D projectileCoords = GameException.RequireNonNull(projectile).coords;
            Vecteur2D projectileDimentions = projectile.ImageDimentions;

            Vecteur2D[] res = {
                projectileCoords,
                new Vecteur2D(projectileCoords.x + projectileDimentions.x, projectileCoords.y),
                new Vecteur2D(projectileCoords.x, projectileCoords.y + projectileDimentions.y),
                new Vecteur2D(projectileCoords.x + projectileDimentions.x, projectileCoords.y + projectileDimentions.y),
            };

            return res.Any(
               v => 
                    (coords.x <= GameException.RequireNonNull(v).x && v.x < coords.x + ImageDimentions.x) && 
                    (coords.y <= v.y && v.y < coords.y + ImageDimentions.y)
           );
        }

        public override bool IsAlive()
        {
            return true;
        }

        #endregion
    }
}
