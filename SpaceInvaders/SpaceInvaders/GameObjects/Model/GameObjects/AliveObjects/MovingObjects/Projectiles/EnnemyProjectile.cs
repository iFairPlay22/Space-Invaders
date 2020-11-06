using SpaceInvaders.GameObjects.View.Display.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.GameObjects.Projectiles
{
    class EnnemyProjectile : ProjectileObject
    {
        #region Fields

        /// <summary>
        /// Ball speed in pixel/second
        /// </summary>
        private static readonly double ENNEMY_PROJECTILE_SPEED = 100;

        /// <summary>
        /// Projectile life
        /// </summary>
        private static readonly int ENNEMY_PROJECTILE_LIFE = 1;

        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="v">Vecteur</param>
        public EnnemyProjectile(Vecteur2D v, Drawable image) :
            base(Team.ENNEMY, v, image, ENNEMY_PROJECTILE_SPEED, ENNEMY_PROJECTILE_LIFE)
        {
            this.coords += new Vecteur2D(0, GameException.RequireNonNull(ImageDimentions).Y);
        }

        #endregion

    }
}