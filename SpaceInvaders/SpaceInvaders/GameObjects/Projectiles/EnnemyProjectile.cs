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

        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="v">Vecteur</param>
        public EnnemyProjectile(Vecteur2D v) :
            base(new TeamManager(GameObjects.Team.ENNEMY), v, SpaceInvaders.Properties.Resources.shoot1, ENNEMY_PROJECTILE_SPEED, false)
        {
            this.coords += new Vecteur2D(0, GameException.RequireNonNull(ImageDimentions).y);
        }

        #endregion

    }
}