using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.GameObjects.Shooters;
using SpaceInvaders.GameObjects.View.Sounds;
using System;
using System.Drawing;

namespace SpaceInvaders.GameObjects.Alive
{

    abstract class AliveObject : MovingObject
    {

        /// <summary>
        /// Game object life
        /// </summary>
        private int life;

        protected readonly SoundHandler soundHandler;

        public AliveObject(Team team, Vecteur2D coords, Bitmap image, SoundHandler soundHandler, double speed, double speedDecalage, int life) :
            base(team, coords, image, speed, speedDecalage)
        {
            this.soundHandler = GameException.RequireNonNull(soundHandler);
            this.life = (int)GameException.RequirePositive(life);
        }

        public override void OnCollision(ProjectileObject projectile)
        {
            int damages = Math.Min(life, projectile.life);
            life -= damages;
            projectile.life -= damages;

            CollisionSounds();
            projectile.CollisionSounds();
        }

        private void CollisionSounds()
        {
            if (life != 0)
                soundHandler.OnCollision(); //"volatile_ennemy_dead.wav"
            else
                soundHandler.OnDeath();
        }

        public override bool IsAlive()
        {
            return 0 < life;
        }
    }
}
