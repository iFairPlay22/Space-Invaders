using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.GameObjects.View.Display.Images;
using SpaceInvaders.GameObjects.View.Sounds;
using System;

namespace SpaceInvaders.GameObjects.Alive
{

    /// <summary>
    /// Represents a game object that have a life
    /// </summary>
    abstract class AliveObject : DrawableObject
    {

        /// <summary>
        /// Game object life
        /// </summary>
        public int Life { get; private set; }

        /// <summary>
        /// Song container of the game object
        /// </summary>
        protected readonly SoundHandler SoundHandler;

        /// <summary>
        /// Create an alive object
        /// </summary>
        /// <param name="team">the team of the game object</param>
        /// <param name="coords">the position of the game object</param>
        /// <param name="drawable">the image to draw</param>
        /// <param name="soundHandler">the song container</param>
        /// <param name="life">the life of the imageObject</param>
        public AliveObject(Team team, Vector2D coords, Drawable drawable, SoundHandler soundHandler, int life) :
            base(team, coords, drawable)
        {
            Coords -= new Vector2D(ImageDimentions.X / 2, 0);
            SoundHandler = GameException.RequireNonNull(soundHandler);
            Life = (int)GameException.RequirePositive(life);
        }

        /// <summary>
        /// Update user life, pixels and songs
        /// </summary>
        /// <param name="projectile">the projectile</param>
        public override void OnCollision(ProjectileObject projectile)
        {
            int damages = Math.Min(Life, projectile.Life);
            Life -= damages;
            projectile.Life -= damages;

            CollisionSounds();
            projectile.CollisionSounds();
        }

        /// <summary>
        /// Play the correct sound
        /// </summary>
        private void CollisionSounds()
        {
            if (0 < Life)
                SoundHandler.OnCollision();
            else
                SoundHandler.OnDeath();
        }

        /// <summary>
        /// An object is alive if he have at least 1 life
        /// </summary>
        /// <returns>Is the object alive ?</returns>
        public override bool IsAlive()
        {
            return 0 < Life;
        }

        /// <summary>
        /// Kill an alive object and play a deah song
        /// </summary>
        public void Destroy()
        {
            Life = 0;
            SoundHandler.OnDeath();
        }
    }
}
