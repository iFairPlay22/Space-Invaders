
namespace SpaceInvaders.GameObjects.Background
{

    /// <summary>
    /// Background image used when the has won
    /// </summary>
    class VictoryBackground : BackgroundImage
    {

        /// <summary>
        /// Create the background image
        /// </summary>
        /// <param name="gameInstance">the gameInstance</param>
        public VictoryBackground(Game gameInstance) :
            base(gameInstance, Properties.Resources.win_background)
        { }
    }
}
