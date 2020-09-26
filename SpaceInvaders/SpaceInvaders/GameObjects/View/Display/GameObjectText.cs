using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.GameObjects.View.Display
{
    class GameObjectText : Text
    {
        public GameObjectText(Game gameInstance, GameObject gameObject) 
            : base(new Vecteur2D(10, gameInstance.gameSize.Height - 35), () => gameObject.ToString())
        {}
    }
}
