
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
        public GameBackground() :
            base(Properties.Resources.game_background)
        { }
    }
}
