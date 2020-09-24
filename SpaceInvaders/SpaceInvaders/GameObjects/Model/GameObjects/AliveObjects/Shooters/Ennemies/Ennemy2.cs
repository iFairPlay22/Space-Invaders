
namespace SpaceInvaders.GameObjects.Shooters.Ennemies
{
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
        /// Simple constructor
        /// </summary>
        /// <param name="coords">Initial coords</param>

        public Ennemy2(Vecteur2D src, Vecteur2D dst) :
            base(src, dst, Properties.Resources.ship4, ENNEMY_SPEED, ENNEMY_SPEED_DECALAGE, SHOOT_SUCCES_PERCENTAGE, ENNEMY_LIFE)
        { }

        #endregion
    }
}