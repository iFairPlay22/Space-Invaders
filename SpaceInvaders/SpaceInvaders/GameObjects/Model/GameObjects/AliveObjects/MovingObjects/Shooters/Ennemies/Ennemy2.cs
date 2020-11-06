
using SpaceInvaders.GameObjects.View.Display.Animations;
using SpaceInvaders.GameObjects.View.Display.Images;

namespace SpaceInvaders.GameObjects.Shooters.Ennemies
{

    /// <summary>
    /// Represent a ship1 ennemy
    /// </summary>
    class Ennemy2 : EnnemyObject
    {
        #region Fields

        /// <summary>
        /// Ball speed in pixel/second
        /// </summary>
        private static readonly double ENNEMY_SPEED = 75;

        /// <summary>
        /// Ball speed in pixel/second
        /// </summary>
        private static readonly double ENNEMY_SPEED_DECALAGE = 5;

        /// <summary>
        /// Percentage
        /// </summary>
        private static readonly int SHOOT_SUCCES_PERCENTAGE = 75;

        /// <summary>
        /// Ennemy life
        /// </summary>
        private static readonly int ENNEMY_LIFE = 1;

        #endregion

        #region Constructor

        /// <summary>
        /// Create shooter object
        /// </summary>
        /// <param name="src">initial position of the ennemy</param>
        /// <param name="dst">destination to reach before horizontal movement</param>
        public Ennemy2(Vecteur2D src, Vecteur2D dst) :
            base(src, dst, new Animation(Properties.Resources.ship2, 1, 2), new Frame(Properties.Resources.missile2), ENNEMY_SPEED, ENNEMY_SPEED_DECALAGE, SHOOT_SUCCES_PERCENTAGE, ENNEMY_LIFE)
        { }

        #endregion
    }
}