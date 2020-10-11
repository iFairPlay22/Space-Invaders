using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.GameObjects.View.Display.Images;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.GameObjects.Background
{
    abstract class BackgroundImage : ImageObject
    {
        protected BackgroundImage(Game gameInstance, Bitmap image) :
            base(
                Team.NEUTRAL,
                new Vecteur2D(0, 0),
                new Frame(
                    new Bitmap(image,
                        new Size(
                            GameException.RequireNonNull(gameInstance).gameSize.Width,
                            gameInstance.gameSize.Height
                        )
                    )
                )
            )
        {}

        public override bool CanCollision(Projectiles.ProjectileObject projectile)
        {
            return false;
        }
        public override void OnCollision(Projectiles.ProjectileObject projectile)
        {
            throw new NotImplementedException();
        }

        public override void Update(Game gameInstance, double deltaT)
        {
           
        }
    }
}
