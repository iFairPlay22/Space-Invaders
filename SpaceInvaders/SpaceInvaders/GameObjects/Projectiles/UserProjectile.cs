using SpaceInvaders.GameObjects.Projectiles;
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

        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="v">Vecteur</param>
        public UserProjectile(Vecteur2D v) : 
            base(Team.PLAYER, v, Properties.Resources.shoot1, USER_PROJECTILE_SPEED) {
            this.coords += new Vecteur2D(0, -GameException.RequireNonNull(ImageDimentions).y);
        }

        #endregion
    }
}
