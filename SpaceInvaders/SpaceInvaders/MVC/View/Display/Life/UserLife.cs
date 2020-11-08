using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.GameObjects.View.Display.Animations;
using SpaceInvaders.GameObjects.View.Display.Images;
using SpaceInvaders.Properties;
using System.Drawing;

namespace SpaceInvaders.GameObjects.View.Display.Life
{

    /// <summary>
    /// Draw hearts (user life)
    /// </summary>
    class UserLife : GameObject
    {
        /// <summary>
        /// User game object
        /// </summary>
        private readonly User user;

        /// <summary>
        /// The animation to draw
        /// </summary>
        private readonly static Drawable DRAWABLE = new Animation(Resources.ship0, 1, 2);

        /// <summary>
        /// Margin between the screen limits in pixels
        /// </summary>
        private readonly static int MARGIN = 15;

        /// <summary>
        /// Draw the game objects in the graphics
        /// </summary>
        /// <param name="user">the user instance</param>
        public UserLife(User user) :
            base(Team.NEUTRAL, new Vector2D(MARGIN, Game.game.gameSize.Height - DRAWABLE.Height - MARGIN))
        {
            this.user = user;
        }

        /// <summary>
        /// Draw the game objects in the graphics
        /// </summary>
        /// <param name="gameInstance">gameInstance</param>
        /// <param name="graphics">graphics</param>
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            for (int i = 0; i < user.Life; i++)
            {
                DRAWABLE.Draw(
                    graphics,
                    new Vector2D(
                        coords.X + i * DRAWABLE.Width,
                        coords.Y
                    )
                );
            }
        }

        /// <summary>
        /// Can't have collision with a projectile
        /// </summary>
        /// <param name="gameInstance">gameInstance</param>
        public override bool CanCollision(ProjectileObject projectile)
        {
            return false;
        }

        /// <summary>
        /// Do nothing 
        /// </summary>
        public override void OnCollision(ProjectileObject projectile) {}

        /// <summary>
        /// Do nothing
        /// </summary>
        public override void Update(Game gameInstance, double deltaT) {}

        /// <summary>
        /// Can be destroy when the user is dead
        /// </summary>
        /// <returns>Is the user alive ?</returns>
        public override bool IsAlive()
        {
            return user.IsAlive();
        }
    }
}
