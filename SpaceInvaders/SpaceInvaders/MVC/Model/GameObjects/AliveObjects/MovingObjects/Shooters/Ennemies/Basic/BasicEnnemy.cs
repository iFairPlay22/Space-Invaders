using SpaceInvaders.GameObjects.View.Display.Images;

namespace SpaceInvaders.GameObjects.Shooters.Ennemies
{
    /// <summary>
    /// Represent a ship1 ennemy
    /// </summary>
    abstract class BasicEnnemy : EnnemyObject
    {
        #region Fields

        /// <summary>
        /// Moving speed
        /// </summary>
        private static readonly double ENNEMY_SPEED = 50;

        /// <summary>
        /// Speed decalage when direction changes
        /// </summary>
        private static readonly double ENNEMY_SPEED_DECALAGE = 10;

        /// <summary>
        /// Percentage to shoot
        /// </summary>
        private static readonly int SHOOT_SUCCES_PERCENTAGE = 50;

        /// <summary>
        /// Ennemy life
        /// </summary>
        private static readonly int ENNEMY_LIFE = 1;

        #endregion

        #region Constructor

        /// <summary>
        /// Create an ennemy1 object
        /// </summary>
        /// <param name="src">initial position of the ennemy</param>
        /// <param name="dst">destination to reach before horizontal movement</param>
        public BasicEnnemy(Vector2D src, Vector2D dst, Drawable objectImage, Drawable missileImage) :
            base(
                src,
                dst,
                objectImage,
                missileImage,
                ENNEMY_SPEED,
                ENNEMY_SPEED_DECALAGE,
                SHOOT_SUCCES_PERCENTAGE,
                ENNEMY_LIFE
            )
        { }

        #endregion
    }
}