using SpaceInvaders.GameObjects.Shooters;
using SpaceInvaders.GameObjects.View.Display.Images;
using SpaceInvaders.GameObjects.View.Sounds;

namespace SpaceInvaders.GameObjects.Projectiles
{
    /// <summary>
    /// Represents a projectile
    /// </summary>
    abstract class ProjectileObject : MovingObject
    {
        #region Fields

        /// <summary>
        /// True if projectile go in the top direction
        /// False for bottom
        /// </summary>
        private readonly bool top;

        /// <summary>
        /// Projecitle sounds
        /// </summary>
        private static readonly SoundHandler PROJECTILE_SOUNDS = new SoundHandler(
            onActionSound: null,
            onCollisionSound: null,
            onDeathSound: null
        );

        #endregion

        #region Constructor
        /// <summary>
        /// Create shooter object
        /// </summary>
        /// <param name="team">the team</param>
        /// <param name="coords">current position in pixels</param>
        /// <param name="image">image to draw</param>
        /// <param name="projectileSpeed">moving speed</param>
        /// <param name="life">life of the projectoile</param>
        public ProjectileObject(Team team, Vector2D coords, Drawable image, double projectileSpeed, int life) : 
            base(team, coords, image, PROJECTILE_SOUNDS, life, projectileSpeed, 0)
        {
            top = team == Team.PLAYER;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Manage move and collisions
        /// </summary>
        public override void Update(Game gameInstance, double deltaT)
        {
            if (CanMove(gameInstance, deltaT, null, top))
                Move(gameInstance, deltaT, null, top);
            else
                Destroy();

            foreach (GameObject gameObject in Game.game.gameObjects)
                if (gameObject != this && gameObject.CanCollision(this))
                    gameObject.OnCollision(this);
                
        }

        /// <summary>
        /// Destroy the both projectiles
        /// </summary>
        public override void OnCollision(ProjectileObject projectile)
        {
            Destroy();
            projectile.Destroy();
        }

        #endregion
    }
}
