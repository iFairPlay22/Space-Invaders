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
        private int Life;

        /// <summary>
        /// Song container of the game object
        /// </summary>
        protected readonly SoundHandler SoundHandler;

        /// <summary>
        /// True if the common pixels with a projecitle must be destroyed
        /// (replaced by transparent pixels), false else
        /// </summary>
        private readonly bool DestroyPixelsOnCollision;

        /// <summary>
        /// Create an alive object
        /// </summary>
        /// <param name="team">the team of the game object</param>
        /// <param name="coords">the position of the game object</param>
        /// <param name="drawable">the image to draw</param>
        /// <param name="soundHandler">the song container</param>
        /// <param name="life">the life of the imageObject</param>
        /// <param name="destroyPixelsOnCollision">the comportment of the object on collision</param>
        public AliveObject(Team team, Vecteur2D coords, Drawable drawable, SoundHandler soundHandler, int life, bool destroyPixelsOnCollision) :
            base(team, coords, drawable)
        {
            this.SoundHandler = GameException.RequireNonNull(soundHandler);
            this.Life = (int)GameException.RequirePositive(life);
            this.DestroyPixelsOnCollision = destroyPixelsOnCollision;
        }

        /// <summary>
        /// Update user life, pixels and songs
        /// </summary>
        /// <param name="projectile">the projectile</param>
        public override void OnCollision(ProjectileObject projectile)
        {
            if (DestroyPixelsOnCollision)
                base.OnCollision(projectile);

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
            if (Life != 0)
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

        /// <summary>
        /// Return the life of the game object
        /// </summary>
        /// <returns>the life of the game object</returns>
        public override string ToString()
        {
            return $"Vie : {Life}";
        }
    }
}
