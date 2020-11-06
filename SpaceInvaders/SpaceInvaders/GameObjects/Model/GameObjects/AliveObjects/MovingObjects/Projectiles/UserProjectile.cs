using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.GameObjects.View.Display.Images;
using System;

namespace SpaceInvaders.GameObjects.Projectile
{
    class UserProjectile : ProjectileObject
    {
        #region Fields

        /// <summary>
        /// Ball speed in pixel/second
        /// </summary>
        private static readonly double USER_PROJECTILE_SPEED = 200;

        /// <summary>
        /// Projectile life
        /// </summary>
        private static readonly int USER_PROJECTILE_LIFE = 2;

        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="v">Vecteur</param>
        public UserProjectile(Vecteur2D v) : 
            base(Team.PLAYER, v, new Frame(Properties.Resources.missile0), USER_PROJECTILE_SPEED, USER_PROJECTILE_LIFE) {
            coords += new Vecteur2D(0, -GameException.RequireNonNull(ImageDimentions).Y);
        }

        #endregion
    }
}
