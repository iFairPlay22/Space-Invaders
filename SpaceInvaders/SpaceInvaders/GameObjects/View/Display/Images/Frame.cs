using System.Drawing;

namespace SpaceInvaders.GameObjects.View.Display.Images
{
    class Frame : Drawable
    {
        public Frame(Bitmap image) : base(image, image.Width, image.Height)
        {}

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
                )
            );
        }
    }
}
