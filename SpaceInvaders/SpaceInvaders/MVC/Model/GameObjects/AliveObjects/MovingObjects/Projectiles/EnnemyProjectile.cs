using SpaceInvaders.GameObjects.View.Display.Images;

namespace SpaceInvaders.GameObjects.Projectiles
{
    /// <summary>
    /// Create an ennemy projectile
    /// </summary>
    class EnnemyProjectile : ProjectileObject
    {
        #region Fields

        /// <summary>
        /// Projectile speed in pixels
        /// </summary>
        private static readonly double ENNEMY_PROJECTILE_SPEED = 100;

        /// <summary>
        /// Projectile life
        /// </summary>
        private static readonly int ENNEMY_PROJECTILE_LIFE = 1;

        #endregion

        #region Constructor
        /// <summary>
        /// Create an ennemy projectile
        /// </summary>
        /// <param name="coords">initial position projectile</param>
        /// <param name="image">image to draw</param>
        public EnnemyProjectile(Vector2D coords, Drawable image) :
            base(Team.ENNEMY, coords, image, ENNEMY_PROJECTILE_SPEED, ENNEMY_PROJECTILE_LIFE)
        { }

        #endregion

    }
}