using SpaceInvaders.GameObjects.Alive;
using SpaceInvaders.GameObjects.View.Sounds;
using System.Drawing;

namespace SpaceInvaders.GameObjects.Projectiles
{
    abstract class ProjectileObject : AliveObject
    {
        #region Fields

        /// <summary>
        /// True if object have to be print, 
        /// False else
        /// </summary>
        private bool alive = true;

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
        public ProjectileObject(Team team, Vecteur2D v, Bitmap image, double projectileSpeed, int life) : 
            base(team, v, image, PROJECTILE_SOUNDS, projectileSpeed, 0, life)
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
                alive = false;

            foreach (GameObject gameObject in Game.game.gameObjects)
                if (gameObject != this && gameObject.CanCollision(this))
                    gameObject.OnCollision(this);
                
        }

        public override bool IsAlive()
        {
            return base.IsAlive() && alive;
        }

        public override void OnCollision(ProjectileObject projectile)
        {
            if (team != projectile.team)
            {
                alive = false;
                projectile.alive = false;
            }
        }

        #endregion
    }
}
