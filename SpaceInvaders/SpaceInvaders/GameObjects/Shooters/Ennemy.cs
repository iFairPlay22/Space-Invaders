using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.GameObjects.Ships;
using System;
using System.Timers;

namespace SpaceInvaders.GameObjects.Shooters
{
    class Ennemy : MovingShooterObject
    {
        #region Fields

        /// <summary>
        /// Ball speed in pixel/second
        /// </summary>
        private static readonly double ENNEMY_SPEED = 50;

        /// <summary>
        /// Ball speed in pixel/second
        /// </summary>
        private static readonly double ENNEMY_SPEED_DECALAGE = 10;

        /// <summary>
        /// Time between 2 shoots in milliseconds
        /// </summary>
        private static readonly int SHOOT_TIME = 10000;

        /// <summary>
        /// Timer
        /// </summary>
        private readonly Timer timer;

        /// <summary>
        /// True if can shoot
        /// False else
        /// </summary>
        private bool canShoot = true;

        /// <summary>
        /// Ennemy life
        /// </summary>
        private static readonly int ENNEMY_LIFE = 1;

        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="coords">Initial coords</param>
        /// 
        public Ennemy(Vecteur2D coords) : 
            base(new TeamManager(Team.ENNEMY), coords, Properties.Resources.ship2, ENNEMY_SPEED, ENNEMY_SPEED_DECALAGE, ENNEMY_LIFE) 
            {
                timer = new Timer { Interval = SHOOT_TIME };
                timer.Elapsed += (object sender, ElapsedEventArgs e) => {
                    canShoot = true;
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

        public override bool CanMove(Game gameInstance, double deltaT, bool? right, bool? top)
        {

            if (top.HasValue && top.Value == true)
                throw new InvalidOperationException();

            return base.CanMove(gameInstance, deltaT, right, top);
        }

        public override bool CanShoot()
        {
            base.CanShoot();
            return canShoot;
        }

        public override void Shoot()
        {
            base.Shoot();

            canShoot = false;
            timer.Start();

            Game.game.AddNewGameObject(new EnnemyProjectile(ProjectileCoords()));
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            if (CanShoot())
                Shoot();
        }

        #endregion
    }
}
