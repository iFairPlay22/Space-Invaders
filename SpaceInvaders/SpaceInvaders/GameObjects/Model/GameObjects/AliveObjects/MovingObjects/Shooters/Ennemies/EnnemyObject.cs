using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.GameObjects.Ships;
using SpaceInvaders.GameObjects.View.Display.Animations;
using SpaceInvaders.GameObjects.View.Display.Images;
using SpaceInvaders.GameObjects.View.Sounds;
using SpaceInvaders.Util;
using System;
using System.Drawing;
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

        private static readonly SoundHandler ENNEMY_SOUNDS = new SoundHandler(
            onActionSound: "volatile_fire_2.wav",
            onCollisionSound: "volatile_ennemy_be_attacked.wav",
            onDeathSound: "volatile_ennemy_dead.wav"
        );

        private readonly Drawable missileImage;

        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="coords">Initial coords</param>
        /// 
        public EnnemyObject(Vecteur2D src, Vecteur2D dst, Drawable drawable, Drawable missileImage, double speed, double speedDecalage, int shootPercentage, int life) : 
            base(Team.ENNEMY, GameException.RequireNonNull(src), drawable, ENNEMY_SOUNDS, speed, speedDecalage, life) 
            {
                destinationCoords = GameException.RequireNonNull(dst);
                this.missileImage = missileImage;
                this.shootPercentage = (int) GameException.RequirePositive(shootPercentage);
                timer = new Timer { 
                    Interval =  1000
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
            return destinationCoords.Y < coords.Y;
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
            if (coords.Y < GameException.RequireNonNull(gameInstance).gameSize.Height)
                coords += new Vecteur2D(0, decalage);
        }

        protected override bool CanShoot()
        {
            return base.CanShoot() && IsArrivedToDestination();
        }

        protected override void Shoot()
        {

            if (timeToShoot && RandomNumbers.Randint(0, 100) <= shootPercentage)
            {
                base.Shoot();
                Projectile = new EnnemyProjectile(ProjectileCoords(), missileImage);
            }

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
