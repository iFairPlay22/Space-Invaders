using SpaceInvaders.GameObjects.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.GameObjects.Background
{
    class DefeatBackground : BackgroundImage
    {
        public DefeatBackground(Game gameInstance) :
            base(gameInstance, Properties.Resources.loose_background) { }
    }
}
