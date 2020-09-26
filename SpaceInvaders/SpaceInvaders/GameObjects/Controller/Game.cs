using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SpaceInvaders.GameObjects;
using SpaceInvaders.GameObjects.Background;
using SpaceInvaders.GameObjects.Shooters;
using SpaceInvaders.GameObjects.View.Display;
using SpaceInvaders.GameObjects.View.Sounds;
using SpaceInvaders.Util;

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
        /// Game state
        /// </summary>
        public GameStateManager gameStateManager;

        #endregion

        #region static fields (helpers)

        /// <summary>
        /// Singleton for easy access
        /// </summary>
        public static Game game { get; private set; }

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
        public void Load() {
            gameStateManager = new GameStateManager(this);
        }

        private void BeforeSwitch()
        {
            gameObjects.Clear();
        }

        public void SwitchToStart()
        {
            BeforeSwitch();

            AddNewGameObject(new StartingBackground(this));

            SongManager.instance.CreatePlayList(
                new List<string> {
                    "background_intro.wav"
                }
            );
        }

        public void SwitchToGame()
        {
            BeforeSwitch();

            AddNewGameObject(new GameBackground(this));

            User user = new User(this);
            AddNewGameObject(user);
           
            AddNewGameObject(new GameObjectText(this, user));

            AddNewGameObject(new ObjectsContainer(this, user));

            SongManager.instance.CreatePlayList(
                new List<string> {
                    "background_game_1.wav",
                    "background_game_2.wav",
                    "background_game_3.wav"
                }
            );
        }

        public void SwitchToEnd(bool win)
        {
            BeforeSwitch();

            if (win) 
                AddNewGameObject(new VictoryBackground(this));
            else
                AddNewGameObject(new DefeatBackground(this));

            SongManager.instance.CreatePlayList(
                new List<string> {
                    win ? "volatile_victory.wav" : "volatile_defeat.wav",
                    "background_end.wav"
                }
            );

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
            // init menu --> game
            if ((gameStateManager.StartMode() || gameStateManager.EndMode()) && keyPressed.Contains(Keys.Space))
            {
                gameStateManager.StartGame();
                keyPressed.Remove(Keys.Space);
            }

            // game --> pause ?
            if (gameStateManager.GameMode())
            {
                if (keyPressed.Contains(Keys.P))
                {
                    gameStateManager.PausedGame();
                    keyPressed.Remove(Keys.P);
                }
                if (gameStateManager.Paused()) return;
            }

            // add new game objects
            gameObjects.UnionWith(pendingNewGameObjects);
            pendingNewGameObjects.Clear();

            // update each game object
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(this, deltaT);
                if (!gameStateManager.GameMode()) break;
            }
            
            // remove dead objects
            gameObjects.RemoveWhere(gameObject => !gameObject.IsAlive());
        }
        #endregion
    }
}
