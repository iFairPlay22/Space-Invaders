using System.Drawing;

namespace SpaceInvaders.GameObjects.View.Display.Images
{
    /// <summary>
    /// Represents a single image to draw
    /// </summary>
    class Frame : Drawable
    {
        /// <summary>
        /// Create a Frame
        /// </summary>
        /// <param name="image">the image that we have to draw</param>
        public Frame(Bitmap image) : base(image, image.Width, image.Height)
        {}

        /// <summary>
        /// Draw the current image into the graphics
        /// </summary>
        /// <param name="graphics">the graphics</param>
        /// <param name="destination">top left position represented in pixels</param>
        public override void Draw(Graphics graphics, Vector2D destination)
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
