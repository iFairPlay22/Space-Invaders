using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SpaceInvaders.GameObjects;
using SpaceInvaders.GameObjects.Background;
using SpaceInvaders.GameObjects.Model.GameObjects.AliveObjects.Bunkers;
using SpaceInvaders.GameObjects.Shooters;
using SpaceInvaders.GameObjects.View.Display.Life;
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
        public readonly HashSet<GameObject> GameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Set of new game objects scheduled for addition to the game
        /// </summary>
        private readonly HashSet<GameObject> PendingNewGameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Schedule a new object for addition in the game.
        /// The new object will be added at the beginning of the next update loop
        /// </summary>
        /// <param name="gameObject">object to add</param>
        public void AddNewGameObject(GameObject gameObject)
        {
            PendingNewGameObjects.Add(gameObject);
        }

        #endregion

        #region game technical elements
        /// <summary>
        /// Size of the game area
        /// </summary>
        public Size GameSize;

        /// <summary>
        /// State of the keyboard
        /// </summary>
        public HashSet<Keys> KeyPressed = new HashSet<Keys>();

        /// <summary>
        /// State of the game
        /// </summary>
        public GameStateManager GameStateManager;

        #endregion

        #region static fields (helpers)

        /// <summary>
        /// Singleton for easy access
        /// </summary>
        public static Game Instance { get; private set; }

        #endregion


        #region constructors
        /// <summary>
        /// Singleton constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        /// <returns></returns>
        public static Game CreateGame(Size gameSize)
        {
            if (Instance == null)
                Instance = new Game(gameSize);
            return Instance;
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        private Game(Size gameSize)
        {
            this.GameSize = gameSize;
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
            KeyPressed.Remove(key);
        }

        /// <summary>
        /// Launch the game
        /// </summary>
        public void Load()
        {
            GameStateManager = new GameStateManager();
        }

        /// <summary>
        /// Function called before each SwitchTo...()
        /// </summary>
        private void BeforeSwitch()
        {
            GameObjects.Clear();
        }

        /// <summary>
        /// When the game menu appears
        /// </summary>
        public void SwitchToStart()
        {
            BeforeSwitch();

            AddNewGameObject(new StartingBackground());

            SongManager.instance.PlaySongs(
                new List<string> {
                    "background_1.wav",
                    "background_2.wav",
                    "background_3.wav",
                    "background_4.wav",
                    "background_5.wav"
                }
            );
        }

        /// <summary>
        /// When a new game is launched
        /// </summary>
        public void SwitchToGame()
        {
            BeforeSwitch();

            AddNewGameObject(new GameBackground());

            ObjectsContainer objectsContainer = new ObjectsContainer();

            AddNewGameObject(objectsContainer);

            AddNewGameObject(new UserLife(objectsContainer.User));
        }

        /// <summary>
        /// When the game is paused
        /// </summary>
        public void Pause()
        {
            SongManager.instance.PlaySoundEffect("sfx_pause.wav");
        }

        /// <summary>
        /// When the game is finished (victory or defeat)
        /// </summary>
        /// <param name="win">Did the user win ?</param>
        public void SwitchToEnd(bool win)
        {
            BeforeSwitch();

            if (win) 
                AddNewGameObject(new VictoryBackground());
            else
                AddNewGameObject(new DefeatBackground());

        }

        /// <summary>
        /// Draw the whole game
        /// </summary>
        /// <param name="g">Graphics to draw in</param>
        public void Draw(Graphics g)
        {
            foreach (GameObject gameObject in GameObjects)
                gameObject.Draw(this, g);       
        }

        /// <summary>
        /// Update game state, and then game logic
        /// </summary>
        /// <param name="deltaT">deltaT</param>
        public void Update(double deltaT)
        {
            // init menu --> game
            if ((GameStateManager.StartMode() || GameStateManager.IsEnd()) && KeyPressed.Contains(Keys.Space))
            {
                GameStateManager.StartGame();
                ReleaseKey(Keys.Space);
            }

            // game --> pause ?
            if (GameStateManager.IsInGame())
            {
                if (KeyPressed.Contains(Keys.P))
                {
                    GameStateManager.PausedGame();
                    ReleaseKey(Keys.P);
                }
                if (GameStateManager.IsPaused()) return;
            }

            UpdateGameLogic(deltaT);
            
        }

        /// <summary>
        /// Update game logic
        /// - Add game objects if necessary ;
        /// - Update game objects ;
        /// - Remove dead game objects ;
        /// </summary>
        /// <param name="deltaT">deltaT</param>
        private void UpdateGameLogic(double deltaT)
        {
            // add new game objects
            GameObjects.UnionWith(PendingNewGameObjects);
            PendingNewGameObjects.Clear();

            // update each game object
            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.Update(this, deltaT);
                if (!GameStateManager.IsInGame()) break;
            }

            // remove dead objects
            GameObjects.RemoveWhere(gameObject => !gameObject.IsAlive());
        }
        #endregion
    }
}
