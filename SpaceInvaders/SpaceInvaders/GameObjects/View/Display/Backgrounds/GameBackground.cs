
namespace SpaceInvaders.GameObjects.Background
{
    class GameBackground : BackgroundImage
    {
        public GameBackground(Game gameInstance) :
            base(gameInstance, Properties.Resources.game_background)
        { }
    }
}
