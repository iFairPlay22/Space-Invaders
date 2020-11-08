using SpaceInvaders.GameObjects.Alive;
using SpaceInvaders.GameObjects.View.Display.Images;
using SpaceInvaders.GameObjects.View.Sounds;
using System;

namespace SpaceInvaders.GameObjects.Shooters
{
    /// <summary>
    /// Represents an alive object that can move
    /// </summary>
    abstract class MovingObject : AliveObject
    {
        /// <summary>
        /// Move speed in pixels
        /// </summary>
        private double speed;

        /// <summary>
        /// Move acceleration in pixels when the direction changes
        /// </summary>
        private readonly double speedDecalage;

        /// <summary>
        /// Max speed of any alive objects in pixels
        /// </summary>
        private static readonly double MAX_SPEED = 250;

        /// <summary>
        /// Create an alive object
        /// </summary>
        /// <param name="team">the team of the game object</param>
        /// <param name="coords">the position of the game object</param>
        /// <param name="drawable">the image to draw</param>
        /// <param name="soundHandler">the song container</param>
        /// <param name="life">the life of the imageObject</param>
        /// <param name="speed">move speed in pixels</param>
        /// <param name="speedDecalage">move acceleration in pixels when the direction changes</param>
        public MovingObject(Team team, Vector2D coords, Drawable drawable, SoundHandler soundHandler, int life, double speed, double speedDecalage) : 
            base(team, coords, drawable, soundHandler, life, false) 
        {
            this.speed = GameException.RequirePositive(speed);
            this.speedDecalage = GameException.RequirePositive(speedDecalage);
        }

        /// <summary>
        /// Accelerate when the direction changes
        /// </summary>
        public virtual void Accelerate()
        {
            if (speed + speedDecalage <= MAX_SPEED)
                speed += speedDecalage;
        }

        /// <summary>
        /// True is the object can go to the specified direction,
        /// False else
        /// </summary>
        /// <param name="gameInstance">game instance</param>
        /// <param name="deltaT">deltaT</param>
        /// <param name="right">true to move to the right direction</param>
        /// <param name="top">true to move to the top direction</param>
        /// <returns>Can the object move to the specified direction ?</returns>
        public virtual bool CanMove(Game gameInstance, double deltaT, bool? right, bool? top)
        {
            Vector2D next = NextCoords(right, top, deltaT);

            if (right.HasValue && !(0 <= next.X && next.X + ImageDimentions.X < gameInstance.gameSize.Width))
                return false;
             
            if (top.HasValue && !(0 <= next.Y && next.Y + ImageDimentions.Y < gameInstance.gameSize.Height))
                return false;

            return true;
        }

        /// <summary>
        /// Move to the specified direction
        /// </summary>
        /// <param name="gameInstance">game instance</param>
        /// <param name="deltaT">deltaT</param>
        /// <param name="right">true to move to the right direction</param>
        /// <param name="top">true to move to the top direction</param>
        public void Move(Game gameInstance, double deltaT, bool? right, bool? top)
        {
            if (!CanMove(gameInstance, deltaT, right, top)) throw new InvalidOperationException();

            coords = NextCoords(right, top, deltaT);
        }

        /// <summary>
        /// Get the next position of an object in pixels 
        /// </summary>
        private Vector2D NextCoords(bool? right, bool? top, double deltaT)
        {
            int dx = 0, dy = 0;
            if (right.HasValue)
                dx = (right.Value ? 1 : -1);

            if (top.HasValue)
                dy = (top.Value ? 1 : -1);

            return new Vector2D(coords.X + dx * (speed * deltaT), coords.Y - dy * (speed * deltaT));
        }
    }
}
