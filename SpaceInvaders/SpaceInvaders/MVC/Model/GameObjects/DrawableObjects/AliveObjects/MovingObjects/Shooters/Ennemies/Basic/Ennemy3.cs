using SpaceInvaders.GameObjects.View.Display.Animations;
using SpaceInvaders.GameObjects.View.Display.Images;


namespace SpaceInvaders.GameObjects.Shooters.Ennemies
{

    /// <summary>
    /// Represent a ship3 ennemy
    /// </summary>
    class Ennemy3 : BasicEnnemy
    {

        #region Constructor

        /// <summary>
        /// Create an ennemy3 object
        /// </summary>
        /// <param name="src">initial position of the ennemy</param>
        /// <param name="dst">destination to reach before horizontal movement</param>
        public Ennemy3(Vector2D src, Vector2D dst) :
            base(
                src,
                dst,
                new Animation(
                    Properties.Resources.ship3,
                    1, 2
                ),
                new Frame(Properties.Resources.missile3)
            )
        { }

        #endregion
    }
}