using SpaceInvaders.GameObjects.Alive;
using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.GameObjects.View.Sounds;
using SpaceInvaders.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.GameObjects.Model.GameObjects.AliveObjects.Bunkers
{
    class Bunker : AliveObject
    {

        private static readonly SoundHandler BUNKER_SOUNDS = new SoundHandler(
            onActionSound: null,
            onCollisionSound: "volatile_ennemy_be_attacked.wav",
            onDeathSound: null
        );

        public Bunker(Vecteur2D coords) : 
            base(Team.NEUTRAL, coords, Resources.bunker, BUNKER_SOUNDS, 0)
        {
        }

        public override void Update(Game gameInstance, double deltaT)
        {

        }

        public override void OnCollision(ProjectileObject projectile)
        {
            base.OnCollision(projectile);
            projectile.Destroy();
        }

        public override bool IsAlive()
        {
            return true;
        }
    }
}
