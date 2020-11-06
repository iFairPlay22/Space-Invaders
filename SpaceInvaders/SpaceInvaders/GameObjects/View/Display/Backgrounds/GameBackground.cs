
namespace SpaceInvaders.GameObjects.Background
{    
    
    /// <summary>
    /// Background image used when the user is playing
    /// </summary>
    class GameBackground : BackgroundImage
    {

        /// <summary>
        /// Create the background image
        /// </summary>
        /// <param name="gameInstance">the gameInstance</param>
        public GameBackground(Game gameInstance) :
            base(gameInstance, Properties.Resources.game_background)
        { }
    }
}
