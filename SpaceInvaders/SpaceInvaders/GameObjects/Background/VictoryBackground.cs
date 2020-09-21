using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.GameObjects.Background
{
    class VictoryBackground : BackgroundImage
    {
        public VictoryBackground(Game gameInstance) :
            base(gameInstance, Properties.Resources.win_background)
        { }
    }
}
