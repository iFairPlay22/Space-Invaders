using System;

namespace SpaceInvaders.Util
{
    /// <summary>
    /// Represents the current state of the game
    /// BEFORE_GAME => Menu when lauching the game
    /// IN_GAME     => When the user is playing 
    /// PAUSED,     => The user paused the game
    /// GAME_OVER   => The user has loosed the last game
    /// WIN         => The user has won the last game
    /// </summary>
    enum GameState
    {
        BEFORE_GAME,
        IN_GAME,
        PAUSED,
        GAME_OVER,
        WIN
    }

    /// <summary>
    /// Call game functions when a state is changing
    /// </summary>
    class GameStateManager
    {
        #region Fields

        /// <summary>
        /// Ball speed in pixel/second
        /// </summary>
        private GameState gameState;
        private Game gameInstance;
        #endregion

        #region Constructor
        /// <summary>
        /// Create the GameStateManager
        /// </summary>
        /// <param name="gameInstance">the game</param>
        public GameStateManager(Game gameInstance)
        {
            this.gameInstance = GameException.RequireNonNull(gameInstance);
            this.gameInstance.SwitchToStart();
            Init();
        }

        #endregion

        #region Switch state

        /// <summary>
        /// Programm lauched => BEFORE_GAME
        /// </summary>
        public void Init()
        {
            gameState = GameState.BEFORE_GAME;
        }

        /// <summary>
        /// BEFORE_GAME => IN_GAME
        /// </summary>
        public void StartGame()
        {
            if (!(StartMode() || IsEnd())) throw new InvalidOperationException();

            gameState = GameState.IN_GAME;
            gameInstance.SwitchToGame();
        }

        /// <summary>
        /// IN_GAME => PAUSED
        /// </summary>
        public void PausedGame()
        {
            if (!IsInGame()) throw new InvalidOperationException();
            gameState = gameState == GameState.PAUSED ? GameState.IN_GAME : GameState.PAUSED;
            gameInstance.Pause();
        }

        /// <summary>
        /// IN_GAME => GAME_OVER or WIN
        /// </summary>
        /// <param name="win">true if the user has won, false else</param>
        public void FinishGame(bool win)
        {
            if (!IsInGame()) throw new InvalidOperationException();

            // win or loose
            gameState = win ? GameState.WIN : GameState.GAME_OVER;
       
            // release key
            gameInstance.SwitchToEnd(win);
        }

        #endregion

        #region Getters

        /// <summary>
        /// The game is not launched
        /// </summary>
        /// <returns> Can we see the menu ? </return>
        public bool StartMode()
        {
            return gameState == GameState.BEFORE_GAME;
        }

        /// <summary>
        /// The user is playing or has paused the game
        /// </summary>
        /// <returns>The user is playing or the game is paused ?</returns>
        public bool IsInGame()
        {
            return gameState == GameState.IN_GAME || IsPaused();
        }

        /// <summary>
        /// Is the game paused by the user ?
        /// </summary>
        /// <returns>Is the game currently paused ?</returns>
        public bool IsPaused()
        {
            return gameState == GameState.PAUSED;
        }

        /// <summary>
        /// The game is finished
        /// </summary>
        /// <returns>The user is playing or paused the game ?</returns>
        public bool IsEnd()
        {
            return gameState == GameState.GAME_OVER || gameState == GameState.WIN;
        }

        #endregion
    }
}
