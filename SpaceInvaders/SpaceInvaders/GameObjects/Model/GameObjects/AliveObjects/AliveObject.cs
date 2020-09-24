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

        public AliveObject(Team team, Vecteur2D coords, Bitmap image, double speed, double speedDecalage, int life) :
            base(team, coords, image, speed, speedDecalage)
        {
            this.life = (int)GameException.RequirePositive(life);
        }

        public override void OnCollision(ProjectileObject projectile)
        {
            int damages = Math.Min(life, projectile.life);
            life -= damages;
            projectile.life -= damages;

            if (life == 0) 
                SongManager.instance.AddVolatileSong("volatile_ennemy_dead.wav");
        }

        public override bool IsAlive()
        {
            return 0 < life;
        }
    }
}
