using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using SpaceInvaders.GameObjects.View.Sounds;

namespace SpaceInvaders
{
    public partial class GameForm : Form
    {

        #region fields
        /// <summary>
        /// Instance of the game
        /// </summary>
        private Game game;

        #region time management
        /// <summary>
        /// Game watch
        /// </summary>
        Stopwatch watch = new Stopwatch();

        /// <summary>
        /// Last update time
        /// </summary>
        long lastTime = 0;
        #endregion
           
        #endregion

        #region constructor
        /// <summary>
        /// Create form, create game
        /// </summary>
        public GameForm()
        {
            SongManager.instance.Load();
            InitializeComponent();
            this.ClientSize = new Size(
                (2 * Screen.PrimaryScreen.Bounds.Width) / 3, 
                (2 * Screen.PrimaryScreen.Bounds.Height) / 3
            );
            game = Game.CreateGame(this.ClientSize);
            watch.Start();
            WorldClock.Start();

        }
        #endregion

        #region events
        /// <summary>
        /// Paint event of the form, see msdn for help => paint game with double buffering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            BufferedGraphics bg = BufferedGraphicsManager.Current.Allocate(e.Graphics, e.ClipRectangle);
            Graphics g = bg.Graphics;
            g.Clear(System.Drawing.Color.White);

            game.Draw(g);

            bg.Render();
            bg.Dispose();

        }

        /// <summary>
        /// Tick event => update game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorldClock_Tick(object sender, EventArgs e)
        {
            // lets do 5 ms update to avoid quantum effects
            int maxDelta = 5;

            // get time with millisecond precision
            long nt = watch.ElapsedMilliseconds;
            // compute ellapsed time since last call to update
            double deltaT = (nt - lastTime);

            for (; deltaT >= maxDelta; deltaT -= maxDelta)
                game.Update(maxDelta / 1000.0);

            game.Update(deltaT / 1000.0);

            // remember the time of this update
            lastTime = nt;

            Invalidate();

        }

        /// <summary>
        /// Key down event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            game.keyPressed.Add(e.KeyCode);
        }

        /// <summary>
        /// Key up event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            game.keyPressed.Remove(e.KeyCode);
        }

        #endregion

        private void GameForm_Load(object sender, EventArgs e)
        {
            game.Load();
        }
    }
}
