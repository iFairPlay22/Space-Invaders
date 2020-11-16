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
        private readonly Game Game;

        #region time management
        /// <summary>
        /// Game watch
        /// </summary>
        private readonly Stopwatch Watch = new Stopwatch();

        /// <summary>
        /// Last update time
        /// </summary>
        private long LastTime = 0;
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
            Game = Game.CreateGame(this.ClientSize);
            Watch.Start();
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

            Game.Draw(g);

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
            long nt = Watch.ElapsedMilliseconds;
            // compute ellapsed time since last call to update
            double deltaT = (nt - LastTime);

            for (; deltaT >= maxDelta; deltaT -= maxDelta)
                Game.Update(maxDelta / 1000.0);

            Game.Update(deltaT / 1000.0);

            // remember the time of this update
            LastTime = nt;

            Invalidate();

        }

        /// <summary>
        /// Key down event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            Game.KeyPressed.Add(e.KeyCode);
        }

        /// <summary>
        /// Key up event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            Game.KeyPressed.Remove(e.KeyCode);
        }

        #endregion

        /// <summary>
        /// Load the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_Load(object sender, EventArgs e)
        {
            Game.Load();
        }
    }
}
