using SpaceInvaders.GameObjects.Projectile;
using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.GameObjects.Shooters;
using System;
using System.Drawing;
using System.Timers;

namespace SpaceInvaders.GameObjects.Ships
{
    abstract class MovingShooterObject : MovingObject
    {
        #region Fields

        /// <summary>
        /// Game object life
        /// </summary>
        private int life;

        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="coords">Position in pixels</param>
        /// <param name="image">Image to draw</param>
        public MovingShooterObject(TeamManager team, Vecteur2D coords, Bitmap image, double speed, double speedDecalage, int life) : 
            base(team, coords, image, speed, speedDecalage)
        {
            this.life = (int) GameException.RequirePositive(life);
        }
        
        #endregion

        #region Methods

        public virtual bool CanShoot()
        {
            return true;
        }

        public virtual void Shoot()
        {
            if (!CanShoot()) throw new InvalidOperationException();
        }

        public override bool IsAlive()
        {
            return 0 < life;
        }

        protected Vecteur2D ProjectileCoords()
        {
            return new Vecteur2D(coords.x + ImageDimentions.x / 2, coords.y);
        }

        public override bool CanCollision(ProjectileObject projectile)
        {
            return TeamManager.Team == projectile.TeamManager.getEnnemy() && base.CanCollision(projectile);
        }


        public override void OnCollision(ProjectileObject projectile)
        {
            life--;
        }

        #endregion

    }
}
