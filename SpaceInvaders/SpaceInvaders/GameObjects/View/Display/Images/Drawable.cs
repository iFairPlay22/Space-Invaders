using System.Drawing;

namespace SpaceInvaders.GameObjects.View.Display.Images
{

    /// <summary>
    /// Represents a Drawable image
    /// It can be an animation or a single image
    /// </summary>
    abstract class Drawable
    {

        /// <summary>
        /// The image that we have to draw currently
        /// </summary>
        public readonly Bitmap Image;

        /// <summary>
        /// The width of the image
        /// </summary>
        public readonly int Width;

        /// <summary>
        /// The height of the image
        /// </summary>
        public readonly int Height;

        /// <summary>
        /// Create a Drawable, ready to be drawed
        /// </summary>
        /// <param name="image">the first image that we have to draw</param>
        /// <param name="width">the width of the image</param>
        /// <param name="height">the height of the image</param>
        protected Drawable(Bitmap image, int width, int height)
        {
            Image = GameException.RequireNonNull(image);
            Width = (int) GameException.RequirePositive(width);
            Height = (int) GameException.RequirePositive(height);
        }

        /// <summary>
        /// Draw the current image into the graphics
        /// </summary>
        /// <param name="graphics">the graphics</param>
        /// <param name="destination">top left position represented in pixels</param>
        public abstract void Draw(Graphics graphics, Vecteur2D destination);

    }
}
