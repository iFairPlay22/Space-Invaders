using SpaceInvaders.GameObjects.View.Display.Images;
using System.Drawing;

namespace SpaceInvaders.GameObjects.View.Display.Animations
{
    class Animation : Drawable
    {
        private readonly int lines, columns;
        private Vecteur2D Indexes;

        public Animation(Bitmap image, int lines, int columns) : 
            base(
                image,
                image.Width / (int)GameException.RequirePositive(columns),
                image.Height / (int)GameException.RequirePositive(lines)
            )
        {
            this.lines = lines;
            this.columns = columns;
            Indexes = new Vecteur2D(0, 0);
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
                    (int) Indexes.X * Width,
                    (int) Indexes.Y * Height,
                    Width,
                    Height
                ), 
                GraphicsUnit.Pixel
            );


            Next();

        }

        private void Next()
        {
            int x = (int) Indexes.X;
            int y = (int) Indexes.Y;

            if (Indexes.X + 1 < columns)
            {
                x++;
            } else if (Indexes.Y + 1 < lines)
            {
                x = 0;
                y++;
            } else
            {
                x = 0;
                y = 0;
            }
            
            Indexes = new Vecteur2D(x, y);
        }
    }
}
