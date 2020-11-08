
namespace SpaceInvaders.GameObjects.Background
{

    /// <summary>
    /// Background image used when the has loosed
    /// </summary>
    class DefeatBackground : BackgroundImage
    {

        /// <summary>
        /// Create the background image
        /// </summary>
        /// <param name="gameInstance">the gameInstance</param>
        public DefeatBackground(Game gameInstance) :
            base(gameInstance, Properties.Resources.loose_background)
        { }
    }
}
