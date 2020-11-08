
namespace SpaceInvaders.GameObjects.Background
{

    /// <summary>
    /// Background image used when displaying the game starting menu
    /// </summary>
    class StartingBackground : BackgroundImage
    {

        /// <summary>
        /// Create the background image
        /// </summary>
        /// <param name="gameInstance">the gameInstance</param>
        public StartingBackground(Game gameInstance) :
            base(gameInstance, Properties.Resources.start_background)
        { }
    }
}
