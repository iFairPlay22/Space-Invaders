using SpaceInvaders.GameObjects.View.Display.Images;
using System.Drawing;

namespace SpaceInvaders.GameObjects.View.Display.Animations
{
    class Animation : Drawable
    {
        private Vecteur2D Position;

        public Animation(Bitmap image, int lines, int columns) : 
            base(
                image,
                image.Height / (int)GameException.RequirePositive(lines),
                image.Width / (int)GameException.RequirePositive(columns)
            )
        {
            Position = new Vecteur2D(0, 0);
        }

        int i = 0;

        public override void Draw(Graphics graphics, Vecteur2D destination)
        {
            GameException.RequireNonNull(destination);
            graphics.DrawImage(
                Image,
                new Rectangle(
                    (int) destination.X,
                    (int) destination.Y,
                    Width, 
                    Height
                ),
                new Rectangle(
                    (int) Position.X,
                    (int) Position.Y,
                    Width,
                    Height
                ), 
                GraphicsUnit.Pixel
            );

            
            Next();
        }

        private void Next()
        {
            Position = new Vecteur2D(
                (Position.X + Width) % Image.Width,
                (Position.Y + Height) % Image.Height
            );
        }
    }
}
