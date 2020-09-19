using SpaceInvaders.GameObjects.Ships;
using SpaceInvaders.GameObjects.Shooters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.GameObjects.Projectiles
{
    abstract class ProjectileObject : MovingObject
    {
        #region Fields

        /// <summary>
        /// Ball speed in pixel/second
        /// </summary>
        private readonly double projectileSpeed;

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
        public ProjectileObject(TeamManager team, Vecteur2D v, Bitmap image, double projectileSpeed, bool top) : 
            base(team, v, image, projectileSpeed, 0)
        {
            this.projectileSpeed = GameException.RequirePositive(projectileSpeed);
            this.top = top;
        }

        #endregion

        #region Methods

        public override void Update(Game gameInstance, double deltaT)
        {
            coords = NextCoords(null, top, deltaT);
            if (coords.y <= 0)
                alive = false;

            foreach (GameObject gameObject in Game.game.gameObjects)
                if (gameObject != this && gameObject.CanCollision(this))
                {
                    gameObject.OnCollision(this);
                    this.alive = false;
                }
        }

        public override bool IsAlive()
        {
            return alive;
        }

        public override bool CanCollision(ProjectileObject projectile)
        {
            return TeamManager.Team == projectile.TeamManager.getEnnemy() && base.CanCollision(projectile);
        }


        public override void OnCollision(ProjectileObject projectile)
        {
            if (this.TeamManager.getEnnemy() == projectile.TeamManager.Team)
            {
                this.alive = false;
                projectile.alive = false;
            }
        }

        public Vecteur2D[] getExtremities()
        {
            Vecteur2D v = coords;
            Vecteur2D d = ImageDimentions;

            Vecteur2D[] res = {
                v,
                new Vecteur2D(v.x + d.x  , v.y      ),
                new Vecteur2D(v.x        , v.y + d.y),
                new Vecteur2D(v.x + d.x  , v.y + d.y),
            };

            return res;
        }

        #endregion
    }
}
