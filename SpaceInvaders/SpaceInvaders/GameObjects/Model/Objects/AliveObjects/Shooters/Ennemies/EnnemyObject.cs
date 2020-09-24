using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.GameObjects.Ships;
using SpaceInvaders.Util;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Timers;

namespace SpaceInvaders.GameObjects.Shooters
{
    abstract class EnnemyObject : MovingShooterObject
    {
        #region Fields

        /// <summary>
        /// Timer
        /// </summary>
        private readonly Timer timer;

        /// <summary>
        /// Percentage to shoot between 0 and 100
        /// </summary>
        private readonly int shootPercentage;

        /// <summary>
        /// True if it's time to try a shoot
        /// False else
        /// </summary>
        private bool timeToShoot = true;

        private Vecteur2D destinationCoords;

        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="coords">Initial coords</param>
        /// 
        public EnnemyObject(Vecteur2D src, Vecteur2D dst, Bitmap image, double speed, double speedDecalage, double shootTime, int shootPercentage, int life) : 
            base(Team.ENNEMY, GameException.RequireNonNull(src), image, speed, speedDecalage, life) 
            {
                destinationCoords = GameException.RequireNonNull(dst);
                this.shootPercentage = (int) GameException.RequirePositive(shootPercentage);
                timer = new Timer { 
                    Interval = GameException.RequirePositive(shootTime) 
                };
                timer.Elapsed += (object sender, ElapsedEventArgs e) => {
                    timeToShoot = true;
                    timer.Stop();
                };
            }

        #endregion

        #region Methods

        /// <summary>
        /// Move user to left or right position
        /// </summary>
        /// <param name="gameInstance">Game instance</param>
        /// <param name="deltaT">Game deltaT</param>
        /// <param name="right">True if right direction, False else</param>

        public bool IsArrivedToDestination()
        {
            return destinationCoords.y < coords.y;
        }

        public override bool CanMove(Game gameInstance, double deltaT, bool? right, bool? top)
        {
            if (top.HasValue && top.Value == true)
                throw new InvalidOperationException();

            return base.CanMove(gameInstance, deltaT, right, top);
        }

        public void MoveDown(Game gameInstance, double deltaT)
        {
            double decalage = (IsArrivedToDestination() ? 500 : 100) * deltaT;
            if (coords.y < GameException.RequireNonNull(gameInstance).gameSize.Height)
                coords += new Vecteur2D(0, decalage);
        }

        protected override bool CanShoot()
        {
            return base.CanShoot() && timeToShoot && IsArrivedToDestination();
        }

        protected override void Shoot()
        {
            base.Shoot();

            if (RandomNumbers.Randint(0, 100) <= shootPercentage)
                Projectile = new EnnemyProjectile(ProjectileCoords());

            timeToShoot = false;
            timer.Start();
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            if (CanShoot()) 
                Shoot();
        }

        #endregion
    }
}
