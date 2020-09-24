using SpaceInvaders.GameObjects.Alive;
using SpaceInvaders.GameObjects.Ships;
using SpaceInvaders.GameObjects.Shooters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

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

        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="v">Vecteur</param>
        public ProjectileObject(Team team, Vecteur2D v, Bitmap image, double projectileSpeed, int life) : 
            base(team, v, image, projectileSpeed, 0, life)
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
