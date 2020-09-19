using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SpaceInvaders.GameObjects;
using SpaceInvaders.GameObjects.Shooters;
using SpaceInvaders.GameObjects.Shooters.Ennemies;

namespace SpaceInvaders
{
    class Game
    {

        #region GameObjects management
        
        /// <summary>
        /// Set of all game objects currently in the game
        /// </summary>
        public HashSet<GameObject> gameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Set of new game objects scheduled for addition to the game
        /// </summary>
        private HashSet<GameObject> pendingNewGameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Schedule a new object for addition in the game.
        /// The new object will be added at the beginning of the next update loop
        /// </summary>
        /// <param name="gameObject">object to add</param>
        public void AddNewGameObject(GameObject gameObject)
        {
            pendingNewGameObjects.Add(gameObject);
        }

        #endregion

        #region game technical elements
        /// <summary>
        /// Size of the game area
        /// </summary>
        public Size gameSize;

        /// <summary>
        /// State of the keyboard
        /// </summary>
        public HashSet<Keys> keyPressed = new HashSet<Keys>();

        /// <summary>
        /// True if game is paused
        /// False else
        /// </summary>
        private bool pause = false;

        #endregion

        #region static fields (helpers)

        /// <summary>
        /// Singleton for easy access
        /// </summary>
        public static Game game { get; private set; }

        /// <summary>
        /// A shared black brush
        /// </summary>
        private static Brush blackBrush = new SolidBrush(Color.Black);

        /// <summary>
        /// A shared simple font
        /// </summary>
        private static Font defaultFont = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel);
        #endregion


        #region constructors
        /// <summary>
        /// Singleton constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        /// 
        /// <returns></returns>
        public static Game CreateGame(Size gameSize)
        {
            if (game == null)
                game = new Game(gameSize);
            return game;
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        private Game(Size gameSize)
        {
            this.gameSize = gameSize;
        }

        #endregion

        #region methods

        /// <summary>
        /// Force a given key to be ignored in following updates until the user
        /// explicitily retype it or the system autofires it again.
        /// </summary>
        /// <param name="key">key to ignore</param>
        public void ReleaseKey(Keys key)
        {
            keyPressed.Remove(key);
        }

        /// <summary>
        /// Create game objects when launching the game
        /// </summary>
        public void Load()
        {
            AddNewGameObject(new User(new Vecteur2D(gameSize.Width / 2, gameSize.Height - gameSize.Height / 4)));

            GameObject gameObject = new EnnemyContainer(
                new Ennemy1(new Vecteur2D(gameSize.Width / 4, gameSize.Height / 4)),
                new Ennemy1(new Vecteur2D(gameSize.Width / 4 +100, gameSize.Height / 4)),
                new Ennemy1(new Vecteur2D(gameSize.Width / 4 +200, gameSize.Height / 4)),
                new Ennemy1(new Vecteur2D(gameSize.Width / 4 +300, gameSize.Height / 4)),

                new Ennemy2(new Vecteur2D(gameSize.Width / 4, gameSize.Height / 3)),
                new Ennemy2(new Vecteur2D(gameSize.Width / 4 + 100, gameSize.Height / 3)),
                new Ennemy2(new Vecteur2D(gameSize.Width / 4 + 200, gameSize.Height / 3)),
                new Ennemy2(new Vecteur2D(gameSize.Width / 4 + 300, gameSize.Height / 3))
            );
            AddNewGameObject(gameObject);
        }

        /// <summary>
        /// Draw the whole game
        /// </summary>
        /// <param name="g">Graphics to draw in</param>
        public void Draw(Graphics g)
        {
            foreach (GameObject gameObject in gameObjects)
                gameObject.Draw(this, g);       
        }

        /// <summary>
        /// Update game
        /// </summary>
        public void Update(double deltaT)
        {
            // check if pause
            if (keyPressed.Contains(Keys.P))
            {
                // update pause state
                pause = !pause;
                ReleaseKey(Keys.P);
            }

            // if game is paused
            if (pause) { 
                // don't update the game
                return; 
            }

            // add new game objects
            gameObjects.UnionWith(pendingNewGameObjects);
            pendingNewGameObjects.Clear();
            
            // update each game object
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(this, deltaT);
            }

            // remove dead objects
            gameObjects.RemoveWhere(gameObject => !gameObject.IsAlive());
        }
        #endregion
    }
}
