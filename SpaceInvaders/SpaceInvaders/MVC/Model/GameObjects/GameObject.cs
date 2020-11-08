using SpaceInvaders.GameObjects;
using SpaceInvaders.GameObjects.Projectiles;
using System.Drawing;

namespace SpaceInvaders
{
    /// <summary>
    /// This is the generic abstact base class for any entity in the game
    /// </summary>
    abstract class GameObject
    {

        public readonly Team team;

        /// <summary>
        /// Pixels coordinates of the game object
        /// </summary>
        protected Vector2D coords;

        /// <summary>
        /// Create a game object
        /// </summary>
        /// <param name="team">team of the game object</param>
        /// <param name="coords">pixels coordinates of the game object</param>
        public GameObject(Team team, Vector2D coords) {
            this.team = GameException.RequireNonNull(team);
            this.coords = GameException.RequireNonNull(coords);
        }

        /// <summary>
        /// Update the state of a game objet
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="deltaT">time ellapsed in seconds since last call to Update</param>
        public abstract void Update(Game gameInstance, double deltaT);

        /// <summary>
        /// Render the game object
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="graphics">graphic object where to perform rendering</param>
        public abstract void Draw(Game gameInstance, Graphics graphics);

        /// <summary>
        /// Determines if object is alive. 
        /// If false, the object will be removed automatically.
        /// </summary>
        /// <returns>Am I alive ?</returns>
        public abstract bool IsAlive();

        /// <summary>
        /// Determines if object can have a collision with a projectile
        /// </summary>
        /// <returns>Can I be in collision with this projectile ?</returns>
        public abstract bool CanCollision(ProjectileObject projectile);

        /// <summary>
        /// Action done when CanCollision return true
        /// </summary>
        public abstract void OnCollision(ProjectileObject projectile);

    }
}
