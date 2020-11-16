using SpaceInvaders.GameObjects.Projectile;
using SpaceInvaders.GameObjects.Ships;
using SpaceInvaders.GameObjects.View.Display.Animations;
using SpaceInvaders.GameObjects.View.Sounds;
using System;
using System.Windows.Forms;

namespace SpaceInvaders.GameObjects
{
    /// <summary>
    /// Represents the user ship
    /// </summary>
    class User : ShooterObject
    {
        #region Fields

        /// <summary>
        /// Moving speed
        /// </summary>
        private static readonly double USER_SPEED = 300;

        /// <summary>
        /// User life
        /// </summary>
        private static readonly int USER_LIFE = 2;

        /// <summary>
        /// User sounds
        /// </summary>
        private static readonly SoundHandler USER_SOUNDS = new SoundHandler(
            onActionSound: "sfx_fire_1.wav",
            onCollisionSound: "sfx_user_be_attacked.wav",
            onDeathSound: "sfx_user_dead.wav"
        );
        #endregion

        #region Constructor
        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="gameInstance">gameInstance</param>
        public User(Game gameInstance) :
            base(
                Team.PLAYER,
                new Vector2D(
                    GameException.RequireNonNull(gameInstance).GameSize.Width / 2,
                    gameInstance.GameSize.Height - gameInstance.GameSize.Height / 4
                ),
                new Animation(Properties.Resources.ship0, 1, 2),
                /*new Frame(Properties.Resources.ship0),*/
                USER_SOUNDS,
                USER_SPEED, 
                0, 
                USER_LIFE
             ) 
                {}

        #endregion

        #region Methods

        /// <summary>
        /// Move user to left or right direction
        /// </summary>
        /// <param name="gameInstance">Game instance</param>
        /// <param name="deltaT">Game deltaT</param>
        /// <param name="right">True if right direction, False else</param>
        /// <returns>True if can move in this direction, false else</returns>
        public override bool CanMove(Game gameInstance, double deltaT, bool? right, bool? top)
        {

            if (top.HasValue)
                throw new InvalidOperationException();

            return base.CanMove(gameInstance, deltaT, right, top);

        }

        /// <summary>
        /// Shoot a projectile
        /// </summary>
        protected override void Shoot()
        {
            base.Shoot();
            Projectile = new UserProjectile(ProjectileCoords());
        }

        /// <summary>
        /// Move and shoot
        /// </summary>
        public override void Update(Game gameInstance, double deltaT)
        {
            // if space is pressed and can shoot, shoot a projectile
            if (gameInstance.KeyPressed.Contains(Keys.Space) && CanShoot())
                Shoot();

            bool right = gameInstance.KeyPressed.Contains(Keys.Right);
            bool left = gameInstance.KeyPressed.Contains(Keys.Left);

            // if right or left arrow is pressed and can move, move
            if ((right || left) && CanMove(gameInstance, deltaT, right, null))
                Move(gameInstance, deltaT, right, null);

        }

        /// <summary>
        /// End the game when the user is dying
        /// </summary>
        /// <returns>Is the user alive ?</returns>
        public override bool IsAlive()
        {
            bool alive = base.IsAlive();
            
            if (!alive) Game.Instance.GameStateManager.FinishGame(false);

            return alive;
        }

        #endregion
    }
}

