using SpaceInvaders.GameObjects;
using SpaceInvaders.GameObjects.Projectiles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// This is the generic abstact base class for any entity in the game
    /// </summary>
    abstract class GameObject
    {

        public readonly TeamManager TeamManager;

        /// <summary>
        /// Position coords
        /// </summary>
        public Vecteur2D coords;

        public GameObject(TeamManager team, Vecteur2D coords) {
            this.TeamManager = GameException.RequireNonNull(team);
            this.coords = coords;
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
        /// Determines if object is alive. If false, the object will be removed automatically.
        /// </summary>
        /// <returns>Am I alive ?</returns>
        public abstract bool IsAlive();

        /// <summary>
        /// Determines if object can have a collision
        /// <returns>Am I alive ?</returns>
        public abstract bool CanCollision(ProjectileObject projectile);

        /// <summary>
        /// Determines if object can have a collision
        /// <returns>Am I alive ?</returns>
        public abstract void OnCollision(ProjectileObject projectile);

    }
}
