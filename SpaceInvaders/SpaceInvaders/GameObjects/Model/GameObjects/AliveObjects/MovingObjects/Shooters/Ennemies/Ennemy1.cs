using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.GameObjects.Shooters.Ennemies
{
    class Ennemy1 : EnnemyObject
    {
        #region Fields

        /// <summary>
        /// Ball speed in pixel/second
        /// </summary>
        private static readonly double ENNEMY_SPEED = 50;

        /// <summary>
        /// Ball speed in pixel/second
        /// </summary>
        private static readonly double ENNEMY_SPEED_DECALAGE = 10;

        /// <summary>
        /// Percentage
        /// </summary>
        private static readonly int SHOOT_SUCCES_PERCENTAGE = 50;

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
        
        public Ennemy1(Vecteur2D src, Vecteur2D dst) :
            base(src, dst, Properties.Resources.ship1, ENNEMY_SPEED, ENNEMY_SPEED_DECALAGE, SHOOT_SUCCES_PERCENTAGE, ENNEMY_LIFE)
        {}

        #endregion
    }
}