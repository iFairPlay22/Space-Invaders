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
        public StartingBackground() :
            base(Properties.Resources.start_background)
        { }
    }
}
