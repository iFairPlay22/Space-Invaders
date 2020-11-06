using SpaceInvaders.GameObjects.View.Display.Images;
using System.Drawing;

namespace SpaceInvaders.GameObjects.View.Display.Animations
{

    /// <summary>
    /// Represents an animation to draw
    /// </summary>
    class Animation : Drawable
    {

        /// <summary>
        /// The numbers of columns in a single line
        /// </summary>
        private readonly int columns;

        /// <summary>
        /// The numbers of lines in a single row
        /// </summary>
        private readonly int lines;

        /// <summary>
        /// The current position into the base image
        /// </summary>
        private Vecteur2D Indexes;

        /// <summary>
        /// A counter to reduce the animation rythm
        /// </summary>
        private int i = 0;

        /// <summary>
        /// Create an animation from an image that can be divided in few images
        /// </summary>
        /// <param name="image">the graphics</param>
        /// <param name="lines">the numbers of lines in a single row</param>
        /// <param name="columns">the numbers of columns in a single line</param>
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

        /// <summary>
        /// Draw the current image into the graphics
        /// </summary>
        /// <param name="graphics">the graphics</param>
        /// <param name="destination">top left position represented in pixels</param>
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

            if (i++ % 10 == 0)
                Next();

        }

        /// <summary>
        /// Increment the indexes (line, column) to draw the following image
        /// </summary>
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
