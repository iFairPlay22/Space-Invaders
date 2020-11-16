
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
        public DefeatBackground() :
            base(Properties.Resources.loose_background)
        { }
    }
}
