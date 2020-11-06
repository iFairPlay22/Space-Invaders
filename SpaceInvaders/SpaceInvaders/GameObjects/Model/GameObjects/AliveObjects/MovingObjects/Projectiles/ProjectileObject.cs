using SpaceInvaders.GameObjects.Alive;
using SpaceInvaders.GameObjects.Shooters;
using SpaceInvaders.GameObjects.View.Display.Images;
using SpaceInvaders.GameObjects.View.Sounds;
using System.Drawing;

namespace SpaceInvaders.GameObjects.Projectiles
{
    abstract class ProjectileObject : MovingObject
    {
        #region Fields

        /// <summary>
        /// True if projectile does top
        /// False for bottom
        /// </summary>
        private readonly bool top;

        private static readonly SoundHandler PROJECTILE_SOUNDS = new SoundHandler(
            onActionSound: null,
            onCollisionSound: null,
            onDeathSound: null
        );

        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="v">Vecteur</param>
        public ProjectileObject(Team team, Vecteur2D v, Drawable image, double projectileSpeed, int life) : 
            base(team, v, image, PROJECTILE_SOUNDS, life, projectileSpeed, 0)
        {
            top = team == Team.PLAYER;
        }

        #endregion

        #region Methods

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

        public override void OnCollision(ProjectileObject projectile)
        {
            Destroy();
            projectile.Destroy();
        }

        #endregion
    }
}
