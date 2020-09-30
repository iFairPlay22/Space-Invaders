using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.GameObjects.View.Sounds;
using System;
using System.Drawing;

namespace SpaceInvaders.GameObjects.Alive
{

    abstract class AliveObject : ImageObject
    {

        /// <summary>
        /// Game object life
        /// </summary>
        private int life;

        protected readonly SoundHandler soundHandler;

        public AliveObject(Team team, Vecteur2D coords, Bitmap image, SoundHandler soundHandler, int life) :
            base(team, coords, image)
        {
            this.soundHandler = GameException.RequireNonNull(soundHandler);
            this.life = (int)GameException.RequirePositive(life);
        }

        public override void OnCollision(ProjectileObject projectile)
        {
            base.OnCollision(projectile);

            int damages = Math.Min(life, projectile.life);
            life -= damages;
            projectile.life -= damages;

            CollisionSounds();
            projectile.CollisionSounds();
        }

        private void CollisionSounds()
        {
            if (life != 0)
                soundHandler.OnCollision();
            else
                soundHandler.OnDeath();
        }

        public override bool IsAlive()
        {
            return 0 < life;
        }

        public void Destroy()
        {
            life = 0;
            soundHandler.OnDeath();
        }

        public override string ToString()
        {
            return $"Vie : {life}";
        }
    }
}
