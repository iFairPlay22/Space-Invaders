using SpaceInvaders.GameObjects.View.Display.Animations;
using SpaceInvaders.GameObjects.View.Display.Images;

namespace SpaceInvaders.GameObjects.Shooters.Ennemies
{
    /// <summary>
    /// Represent a ship1 ennemy
    /// </summary>
    class Ennemy1 : BasicEnnemy
    {

        #region Constructor

        /// <summary>
        /// Create an ennemy1 object
        /// </summary>
        /// <param name="src">initial position of the ennemy</param>
        /// <param name="dst">destination to reach before horizontal movement</param>
        public Ennemy1(Vector2D src, Vector2D dst) :
            base(
                src, 
                dst, 
                new Animation(
                    Properties.Resources.ship1,
                    1, 2
                ),
                new Frame(Properties.Resources.missile1)
            )
        {}

        #endregion
    }
}