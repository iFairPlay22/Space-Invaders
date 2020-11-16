using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.GameObjects.View.Display.Images;
using System;
using System.Drawing;

namespace SpaceInvaders.GameObjects.Background
{
    /// <summary>
    /// Represents a background image 
    /// This image has the same dimentions as the gameInstance
    /// </summary>
    abstract class BackgroundImage : DrawableObject
    {

        /// <summary>
        /// Increment the indexes (line, column) to draw the following image
        /// </summary>
        /// <param name="gameInstance">the gameInstance</param>
        /// <param name="image">the image to draw</param>
        protected BackgroundImage(Game gameInstance, Bitmap image) :
            base(
                Team.NEUTRAL,
                new Vector2D(0, 0),
                new Frame(
                    new Bitmap(image,
                        new Size(
                            GameException.RequireNonNull(gameInstance).GameSize.Width,
                            gameInstance.GameSize.Height
                        )
                    )
                )
            )
        {}

        /// <summary>
        /// An image can be in collision
        /// </summary>
        /// <returns>Can a background image be in collision ?</returns>
        public override bool CanCollision(ProjectileObject projectile)
        {
            return false;
        }

        /// <summary>
        /// An image can be in collision
        /// </summary>
        public override void OnCollision(ProjectileObject projectile)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Nothing to update
        /// </summary>
        public override void Update(Game gameInstance, double deltaT)
        {
           
        }
    }
}
