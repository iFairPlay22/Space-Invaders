
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
        public VictoryBackground() :
            base(Properties.Resources.win_background)
        { }
    }
}
