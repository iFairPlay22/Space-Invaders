using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.GameObjects.View.Display
{
    /// <summary>
    /// Draw the ToSring() text of a GameObject
    /// </summary>
    class GameObjectText : Text
    {

        /// <summary>
        /// Create a text object
        /// </summary>
        /// <param name="gameInstance">the gameInstance</param>
        /// <param name="gameObject">the gameObject for draw ToString </param>
        public GameObjectText(Game gameInstance, GameObject gameObject) 
            : base(new Vecteur2D(10, gameInstance.gameSize.Height - 35), () => gameObject.ToString())
        {}
    }
}
