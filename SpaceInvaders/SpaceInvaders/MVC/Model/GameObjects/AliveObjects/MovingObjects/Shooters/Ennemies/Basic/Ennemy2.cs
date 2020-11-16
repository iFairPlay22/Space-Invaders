using SpaceInvaders.GameObjects.View.Display.Animations;
using SpaceInvaders.GameObjects.View.Display.Images;


namespace SpaceInvaders.GameObjects.Shooters.Ennemies
{

    /// <summary>
    /// Represent a ship2 ennemy
    /// </summary>
    class Ennemy2 : BasicEnnemy
    {

        #region Constructor

        /// <summary>
        /// Create an ennemy2 object
        /// </summary>
        /// <param name="src">initial position of the ennemy</param>
        /// <param name="dst">destination to reach before horizontal movement</param>
        public Ennemy2(Vector2D src, Vector2D dst) :
            base(
                src,
                dst,
                new Animation(
                    Properties.Resources.ship2,
                    1, 2
                ),
                new Frame(Properties.Resources.missile2)
            )
        { }

        #endregion
    }
}