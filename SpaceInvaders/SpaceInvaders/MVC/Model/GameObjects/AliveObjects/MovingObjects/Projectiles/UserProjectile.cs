using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.GameObjects.View.Display.Images;

namespace SpaceInvaders.GameObjects.Projectile
{
    /// <summary>
    /// Create a user projectile
    /// </summary>
    class UserProjectile : ProjectileObject
    {
        #region Fields

        /// <summary>
        /// Projectile speed in pixels
        /// </summary>
        private static readonly double USER_PROJECTILE_SPEED = 200;

        /// <summary>
        /// Projectile life
        /// </summary>
        private static readonly int USER_PROJECTILE_LIFE = 2;

        #endregion

        #region Constructor
        /// <summary>
        /// Create an ennemy projectile
        /// </summary>
        /// <param name="coords">initial position projectile</param>
        public UserProjectile(Vector2D coords) : 
            base(
                Team.PLAYER, 
                coords, 
                new Frame(Properties.Resources.missile0), 
                USER_PROJECTILE_SPEED, 
                USER_PROJECTILE_LIFE
            ) 
            { }

        #endregion
    }
}
