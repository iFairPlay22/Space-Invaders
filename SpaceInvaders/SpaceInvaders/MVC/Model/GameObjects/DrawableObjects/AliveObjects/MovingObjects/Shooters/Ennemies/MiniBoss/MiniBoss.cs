using SpaceInvaders.GameObjects.View.Display.Images;
using SpaceInvaders.GameObjects.View.Display.Animations;

namespace SpaceInvaders.GameObjects.Shooters.Ennemies
{
    /// <summary>
    /// Represent a ship1 ennemy
    /// </summary>
    class MiniBoss : EnnemyObject
    {
        #region Fields

        /// <summary>
        /// Moving speed
        /// </summary>
        private static readonly double ENNEMY_SPEED = 25;

        /// <summary>
        /// Speed decalage when direction changes
        /// </summary>
        private static readonly double ENNEMY_SPEED_DECALAGE = 5;

        /// <summary>
        /// Percentage to shoot
        /// </summary>
        private static readonly int SHOOT_SUCCES_PERCENTAGE = 75;

        /// <summary>
        /// Ennemy life
        /// </summary>
        private static readonly int ENNEMY_LIFE = 10;

        #endregion

        #region Constructor

        /// <summary>
        /// Create an ennemy1 object
        /// </summary>
        /// <param name="src">initial position of the ennemy</param>
        /// <param name="dst">destination to reach before horizontal movement</param>
        public MiniBoss(Vector2D src, Vector2D dst) :
            base(
                src,
                dst,
                new Animation(
                    Properties.Resources.boss1,
                    1, 17
                ),
                new Animation(
                    Properties.Resources.boss1_missile,
                    17, 1
                ),
                ENNEMY_SPEED,
                ENNEMY_SPEED_DECALAGE,
                SHOOT_SUCCES_PERCENTAGE,
                ENNEMY_LIFE
            )
        { }

        #endregion
    }
}