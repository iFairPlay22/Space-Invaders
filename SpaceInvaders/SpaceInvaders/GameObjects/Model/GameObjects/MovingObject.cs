using System;
using System.Drawing;

namespace SpaceInvaders.GameObjects.Shooters
{

    abstract class MovingObject : ImageObject
    {

        private double speed;
        private readonly double speedDecalage;

        private static readonly double MAX_SPEED = 250;

        public MovingObject(Team team, Vecteur2D coords, Bitmap image, double speed, double speedDecalage) : 
            base(team, coords, image) 
        {
            this.speed = GameException.RequirePositive(speed);
            this.speedDecalage = GameException.RequirePositive(speedDecalage);
        }

        public virtual void Accelerate()
        {
            if (speed + speedDecalage <= MAX_SPEED)
                speed += speedDecalage;
        }

        public virtual bool CanMove(Game gameInstance, double deltaT, bool? right, bool? top)
        {
            Vecteur2D next = NextCoords(right, top, deltaT);

            if (right.HasValue && !(0 <= next.X && next.X + ImageDimentions.X < gameInstance.gameSize.Width))
                return false;
             
            if (top.HasValue && !(0 <= next.Y && next.Y + ImageDimentions.Y < gameInstance.gameSize.Height))
                return false;

            return true;
        }

        public void Move(Game gameInstance, double deltaT, bool? right, bool? top)
        {
            if (!CanMove(gameInstance, deltaT, right, top)) throw new InvalidOperationException();

            coords = NextCoords(right, top, deltaT);
        }

        private Vecteur2D NextCoords(bool? right, bool? top, double deltaT)
        {
            int dx = 0, dy = 0;
            if (right.HasValue)
                dx = (right.Value ? 1 : -1);

            if (top.HasValue)
                dy = (top.Value ? 1 : -1);

            return new Vecteur2D(coords.X + dx * (speed * deltaT), coords.Y - dy * (speed * deltaT));
        }
    }
}
