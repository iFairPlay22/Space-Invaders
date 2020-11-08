using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.GameObjects.Shooters;
using SpaceInvaders.GameObjects.View.Sounds;
using System;

namespace SpaceInvaders.GameObjects.Ships
{
    /// <summary>
    /// Represents a game object that can shoot projectiles
    /// </summary>
    abstract class ShooterObject : MovingObject
    {
        #region Fields

        /// <summary>
        /// The current projectile
        /// </summary>
        private ProjectileObject projectile;

        /// <summary>
        /// The current projectile
        /// </summary>
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

        #endregion

        #region Constructor
        /// <summary>
        /// Create shooter object
        /// </summary>
        /// <param name="team">the team of the game object</param>
        /// <param name="coords">the position of the game object</param>
        /// <param name="drawable">the image to draw</param>
        /// <param name="soundHandler">the song container</param>
        /// <param name="life">the life of the imageObject</param>
        /// <param name="speed">move speed in pixels</param>
        /// <param name="speedDecalage">move acceleration in pixels when the direction changes</param>
        public ShooterObject(Team team, Vector2D coords, View.Display.Images.Drawable drawable, SoundHandler soundHandler, double speed, double speedDecalage, int life) : 
            base(team, coords, drawable, soundHandler, life, speed, speedDecalage)
        {}

        #endregion

        #region Methods

        /// <summary>
        /// A shooter object can shoot an object if the precedent projecitle is not alive
        /// </summary>
        protected virtual bool CanShoot()
        {
            return Projectile == null || (Projectile != null && !Projectile.IsAlive());
        }

        /// <summary>
        /// Play a sound when shooting
        /// </summary>
        protected virtual void Shoot()
        {
            if (!CanShoot()) throw new InvalidOperationException();

            SoundHandler.OnAction();
        }

        /// <summary>
        /// Get the started projectile coordinates in pixels
        /// </summary>
        protected Vector2D ProjectileCoords()
        {
            return new Vector2D(coords.X + ImageDimentions.X / 2, coords.Y);
        }

        #endregion

    }
}
