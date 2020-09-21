using System;
using System.Windows.Forms;

namespace SpaceInvaders.Util
{
    enum GameState
    {
        BEFORE_GAME,
        IN_GAME,
        PAUSED,
        GAME_OVER,
        WIN
    }

    class GameStateManager
    {
        #region Fields

        /// <summary>
        /// Ball speed in pixel/second
        /// </summary>
        private GameState gameState = GameState.BEFORE_GAME;
        private Game gameInstance;
        #endregion

        #region Constructor
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="coords">Initial coords</param>

        public GameStateManager(Game gameInstance)
        {
            this.gameInstance = GameException.RequireNonNull(gameInstance);
            this.gameInstance.SwitchToStart();
        }

        public bool StartMode()
        {
            return gameState == GameState.BEFORE_GAME;
        }

        public void StartGame()
        {
            if (!StartMode()) throw new InvalidOperationException();

            gameState = GameState.IN_GAME;
            gameInstance.SwitchToGame();
        }

        public bool Paused()
        {
            return gameState == GameState.PAUSED;
        }

        public void PausedGame()
        {
            if (!GameMode()) throw new InvalidOperationException();
            gameState = gameState == GameState.PAUSED ? GameState.IN_GAME : GameState.PAUSED;
        }

        public bool GameMode()
        {
            return gameState == GameState.IN_GAME || Paused();
        }

        public void FinishGame(bool win)
        {
            if (!GameMode()) throw new InvalidOperationException();

            // paused or continue
            gameState = gameState == GameState.PAUSED ? GameState.IN_GAME : GameState.PAUSED;
       
            // release key
            gameInstance.keyPressed.Remove(Keys.P);
            gameInstance.SwitchToEnd(win);
        }

        public bool EndMode()
        {
            return gameState == GameState.GAME_OVER || gameState == GameState.WIN;
        }

        #endregion
    }
}
