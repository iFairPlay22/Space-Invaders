using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.GameObjects.Ships;
using SpaceInvaders.GameObjects.View.Display.Images;
using SpaceInvaders.GameObjects.View.Sounds;
using SpaceInvaders.Util;
using System;
using System.Timers;

namespace SpaceInvaders.GameObjects.Shooters
{
    /// <summary>
    /// Represents an ennemy ship
    /// </summary>
    abstract class EnnemyObject : MovingShooterObject
    {
        #region Fields

        /// <summary>
        /// Timer
        /// </summary>
        private readonly Timer timer;

        /// <summary>
        /// Percentage to shoot by second between 0 and 100
        /// </summary>
        private readonly int shootPercentage;

        /// <summary>
        /// True if it's time to try a shoot
        /// False else
        /// </summary>
        private bool timeToShoot = true;

        /// <summary>
        /// The ennemies are going to the bottom in direction of 
        /// the destinationCoords, and then in right/left direction
        /// </summary>
        private readonly Vecteur2D destinationCoords;

        /// <summary>
        /// Ennemy sounds
        /// </summary>
        private static readonly SoundHandler ENNEMY_SOUNDS = new SoundHandler(
            onActionSound: "volatile_fire_2.wav",
            onCollisionSound: "volatile_ennemy_be_attacked.wav",
            onDeathSound: "volatile_ennemy_dead.wav"
        );

        /// <summary>
        /// Each ennemy group have a specific missile image
        /// </summary>
        private readonly Drawable missileImage;

        #endregion

        #region Constructor
        /// <summary>
        /// Create shooter object
        /// </summary>
        /// <param name="src">initial position of the ennemy</param>
        /// <param name="dst">destination to reach before horizontal movement</param>
        /// <param name="drawable">image to draw</param>
        /// <param name="missileImage">projectile image to draw</param>
        /// <param name="speed">move speed in pixels</param>
        /// <param name="speedDecalage">move acceleration in pixels when the direction changes</param>
        /// <param name="shootPercentage">percentage to shoot by second between 0 and 100</param>
        /// <param name="life">life of the imageObject</param>
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
        /// First step : the ennemmies are going to the bottom to reach 
        /// the destinationCoords
        /// Second step : horizontal movement
        /// </summary>
        /// <returns>Are we in the second step ?</returns>
        public bool IsArrivedToDestination()
        {
            return destinationCoords.Y < coords.Y;
        }

        /// <summary>
        /// An emmemy can't go to the top direction
        /// </summary>
        public override bool CanMove(Game gameInstance, double deltaT, bool? right, bool? top)
        {
            if (top.HasValue && top.Value == true)
                throw new InvalidOperationException();

            return base.CanMove(gameInstance, deltaT, right, top);
        }

        /// <summary>
        /// First step : move to the bottom directiob
        /// </summary>
        public void MoveDown(Game gameInstance, double deltaT)
        {
            double decalage = (IsArrivedToDestination() ? 500 : 100) * deltaT;
            if (coords.Y < GameException.RequireNonNull(gameInstance).gameSize.Height)
                coords += new Vecteur2D(0, decalage);
        }

        /// <summary>
        /// An ennemy can't shoot in the first step
        /// </summary>
        protected override bool CanShoot()
        {
            return base.CanShoot() && IsArrivedToDestination();
        }

        /// <summary>
        /// Shoot a projectile
        /// </summary>
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

        /// <summary>
        /// Shoot if possible
        /// </summary>
        public override void Update(Game gameInstance, double deltaT)
        {
            if (CanShoot()) 
                Shoot();
        }

        #endregion
    }
}
