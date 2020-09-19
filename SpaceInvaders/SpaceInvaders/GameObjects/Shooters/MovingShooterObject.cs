using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.GameObjects.Shooters;
using System;
using System.Drawing;

namespace SpaceInvaders.GameObjects.Ships
{
    abstract class MovingShooterObject : MovingObject
    {
        #region Fields

        /// <summary>
        /// Projectile
        /// </summary>
        
        private ProjectileObject projectile;
        protected ProjectileObject Projectile {
            private get { 
                return projectile; 
            }
            set {
                projectile = value;
                if (projectile != null) 
                    Game.game.AddNewGameObject(projectile);
            } 
        }

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
        public MovingShooterObject(Team team, Vecteur2D coords, Bitmap image, double speed, double speedDecalage, int life) : 
            base(team, coords, image, speed, speedDecalage)
        {
            this.life = (int) GameException.RequirePositive(life);
        }

        #endregion

        #region Methods

        protected virtual bool CanShoot()
        {
            return Projectile == null || (Projectile != null && !Projectile.IsAlive());
        }

        protected virtual void Shoot()
        {
            if (!CanShoot()) throw new InvalidOperationException();
        }

        public override void OnCollision(ProjectileObject projectile)
        {
            life--;
        }

        public override bool IsAlive()
        {
            return 0 < life;
        }

        protected Vecteur2D ProjectileCoords()
        {
            return new Vecteur2D(coords.x + ImageDimentions.x / 2, coords.y);
        }

        #endregion

    }
}
