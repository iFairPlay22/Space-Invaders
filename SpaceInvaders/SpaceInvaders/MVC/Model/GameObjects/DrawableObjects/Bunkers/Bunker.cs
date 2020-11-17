using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.GameObjects.View.Display.Images;
using SpaceInvaders.Properties;

namespace SpaceInvaders.GameObjects.Model.GameObjects.AliveObjects.Bunkers
{
    /// <summary>
    /// Represents a Bunker game object
    /// </summary>
    class Bunker : DrawableObject
    {

        /// <summary>
        /// Create a Bunker (neutral element)
        /// </summary>
        public Bunker(Vector2D coords) : 
            base(Team.NEUTRAL, coords, new Frame(Resources.bunker))
        { }

        /// <summary>
        /// Nothing to do
        /// </summary>
        public override void Update(Game gameInstance, double deltaT)
        { }

        /// <summary>
        /// Update the pixels and destroy the projectile
        /// </summary>
        public override void OnCollision(ProjectileObject projectile)
        {
            base.OnCollision(projectile);
            projectile.Destroy();
        }
    }
}
