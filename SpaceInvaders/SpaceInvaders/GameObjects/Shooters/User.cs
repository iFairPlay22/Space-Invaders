

using SpaceInvaders.GameObjects.Projectile;
using SpaceInvaders.GameObjects.Ships;
using System;
using System.Windows.Forms;

namespace SpaceInvaders.GameObjects
{ 
    class User : MovingShooterObject
    {
        #region Fields

        /// <summary>
        /// Ball speed in pixel/second
        /// </summary>
        private static readonly double USER_SPEED = 300;

        /// <summary>
        /// Ball speed in pixel/second
        /// </summary>
        private static readonly double ENNEMY_SPEED_DECALAGE = 0;

        /// <summary>
        /// Ennemy life
        /// </summary>
        private static readonly int USER_LIFE = 2;

        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="coords">Initial coordsx</param>
        public User(Game gameInstance) :
            base(
                Team.PLAYER,
                new Vecteur2D(
                    GameException.RequireNonNull(gameInstance).gameSize.Width / 2,
                    gameInstance.gameSize.Height - gameInstance.gameSize.Height / 4
                ), 
                Properties.Resources.ship3, 
                USER_SPEED, 
                ENNEMY_SPEED_DECALAGE, 
                USER_LIFE
             ) 
                {}

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

            if (top.HasValue)
                throw new InvalidOperationException();

            return base.CanMove(gameInstance, deltaT, right, top);

        }

        protected override void Shoot()
        {
            base.Shoot();

            Projectile = new UserProjectile(ProjectileCoords());
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            // if space is pressed
            if (gameInstance.keyPressed.Contains(Keys.Space) && CanShoot())
            {
                // shoot a projectile
                Shoot();
            }

            bool right = gameInstance.keyPressed.Contains(Keys.Right);
            bool left = gameInstance.keyPressed.Contains(Keys.Left);

            // if right or left arrow is pressed and can move
            if ((right || left) && CanMove(gameInstance, deltaT, right, null))
            {
                // move player to right or left direction
                Move(gameInstance, deltaT, right, null);
            }
        }

        public override bool IsAlive()
        {
            bool alive = base.IsAlive();
            
            if (!alive) Game.game.gameStateManager.FinishGame(false);

            return alive;
        }

        #endregion
    }
}

