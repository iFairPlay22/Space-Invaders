using SpaceInvaders.GameObjects.Projectiles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.GameObjects
{
    abstract class ImageObject : GameObject
    {
        #region Fields

        /// <summary>
        /// Image dimentions
        /// </summary>
        protected Vecteur2D ImageDimentions { get; private set; }

        /// <summary>
        /// Image to draw
        /// </summary>
        private readonly Bitmap image;

        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="coords">Position in pixels</param>
        /// <param name="image">Image to draw</param>
        
        public ImageObject(TeamManager team, Vecteur2D coords, Bitmap image) : base(team, coords)
        {
            this.coords = GameException.RequireNonNull(coords);
            this.image = GameException.RequireNonNull(image);
            this.ImageDimentions = new Vecteur2D(image.Width, image.Height);
        }
        #endregion

        #region Methods

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            graphics.DrawImage(image, (float)coords.x, (float)coords.y, image.Width, image.Height);
        }

        public override bool CanCollision(ProjectileObject projectile)
        {
           return projectile.getExtremities().Any(
               v => 
                    (coords.x <= GameException.RequireNonNull(v).x && v.x < coords.x + ImageDimentions.x) && 
                    (coords.y <= v.y && v.y < coords.y + ImageDimentions.y)
           );
        }

        public override void Update(Game gameInstance, double deltaT)
        {

        }

        public override bool IsAlive()
        {
            return true;
        }

        #endregion
    }
}
