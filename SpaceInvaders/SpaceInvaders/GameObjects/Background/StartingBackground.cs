using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.GameObjects.Background
{
    class StartingBackground : BackgroundImage
    {
        public StartingBackground(Game gameInstance) :
            base(gameInstance, Properties.Resources.start_background)
        { }
    }
}
