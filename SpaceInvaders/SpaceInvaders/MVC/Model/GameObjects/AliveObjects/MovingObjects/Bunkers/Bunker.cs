using SpaceInvaders.GameObjects.Alive;
using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.GameObjects.View.Display.Images;
using SpaceInvaders.GameObjects.View.Sounds;
using SpaceInvaders.Properties;
using System.Drawing;

namespace SpaceInvaders.GameObjects.Model.GameObjects.AliveObjects.Bunkers
{
    /// <summary>
    /// Represents a Bunker game object
    /// </summary>
    class Bunker : AliveObject
    {

        /// <summary>
        /// Bunker have no sounds
        /// </summary>
        private static readonly SoundHandler BUNKER_SOUNDS = new SoundHandler(
            onActionSound: null,
            onCollisionSound: null,
            onDeathSound: null
        );

        /// <summary>
        /// Create a Bunker (neutral element)
        /// </summary>
        public Bunker(Vector2D coords) : 
            base(Team.NEUTRAL, coords, new Frame(Resources.bunker), BUNKER_SOUNDS, 0, true)
        { }

        /// <summary>
        /// Nothing to do
        /// </summary>
        public override void Update(Game gameInstance, double deltaT)
        { }

        /// <summary>
        /// Update the pixels and destroy the projectile
        /// </summary>
        public override void OnCollision(Projectiles.ProjectileObject projectile)
        {
            base.OnCollision(projectile);
            projectile.Destroy();
        }

        /// <summary>
        /// A bunker is always alive
        /// </summary>
        public override bool IsAlive()
        {
            return true;
        }
    }
}
