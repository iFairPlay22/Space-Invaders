using System.Drawing;

namespace SpaceInvaders.GameObjects.View.Display.Images
{
    abstract class Drawable
    {

        public readonly Bitmap Image;

        public readonly int Width;

        public readonly int Height;

        protected Drawable(Bitmap image, int width, int height)
        {
            Image = GameException.RequireNonNull(image);
            Width = (int) GameException.RequirePositive(width);
            Height = (int) GameException.RequirePositive(height);
        }

        public abstract void Draw(Graphics graphics, Vecteur2D destination);

    }
}
