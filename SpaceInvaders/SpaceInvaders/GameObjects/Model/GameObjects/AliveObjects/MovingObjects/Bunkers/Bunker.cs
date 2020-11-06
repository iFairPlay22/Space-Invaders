using SpaceInvaders.GameObjects.Alive;
using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.GameObjects.View.Display.Images;
using SpaceInvaders.GameObjects.View.Sounds;
using SpaceInvaders.Properties;

namespace SpaceInvaders.GameObjects.Model.GameObjects.AliveObjects.Bunkers
{
    class Bunker : AliveObject
    {

        private static readonly SoundHandler BUNKER_SOUNDS = new SoundHandler(
            onActionSound: null,
            onCollisionSound: null,
            onDeathSound: null
        );

        public Bunker(Vecteur2D coords) : 
            base(Team.NEUTRAL, coords, new Frame(Resources.bunker), BUNKER_SOUNDS, 0, true)
        {}

        public override void Update(Game gameInstance, double deltaT)
        {

        }

        public override void OnCollision(Projectiles.ProjectileObject projectile)
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
