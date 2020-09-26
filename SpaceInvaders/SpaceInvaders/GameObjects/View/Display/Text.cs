using SpaceInvaders.GameObjects.Projectiles;
using System;
using System.Drawing;

namespace SpaceInvaders.GameObjects.View.Display
{
    class Text : GameObject
    {
        public delegate string StringBuilder();
        private readonly StringBuilder TextBuilder;

        /// <summary>
        /// A shared simple font
        /// </summary>
        private static Font defaultFont = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel);

        /// <summary>
        /// A shared black brush
        /// </summary>
        private static Brush blackBrush = new SolidBrush(Color.Black);

        public Text(Vecteur2D vecteur, StringBuilder textBuilder) : base(Team.NEUTRAL, vecteur)
        {
            TextBuilder = GameException.RequireNonNull(textBuilder);
        }

        public override bool CanCollision(ProjectileObject projectile)
        {
            return false;
        }

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            graphics.DrawString(TextBuilder(), defaultFont, blackBrush, new PointF((int) coords.X, (int) coords.Y));
        }

        public override bool IsAlive()
        {
            return true;
        }

        public override void OnCollision(ProjectileObject projectile)
        {
            throw new NotImplementedException();
        }

        public override void Update(Game gameInstance, double deltaT)
        {

        }
    }
}
