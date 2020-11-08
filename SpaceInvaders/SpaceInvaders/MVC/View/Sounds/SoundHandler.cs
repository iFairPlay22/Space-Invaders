

namespace SpaceInvaders.GameObjects.View.Sounds
{

    /// <summary>
    /// Container of sounds
    /// </summary>
    class SoundHandler
    {
        /// <summary>
        /// Sound to play when a game object do an action (ex: shoot)
        /// </summary>
        private readonly string onActionSound;

        /// <summary>
        /// Sound to play when a game object is in collision with another object (ex: shooted)
        /// </summary>
        private readonly string onCollisionSound;

        /// <summary>
        /// Sound to play when a game object is dying
        /// </summary>
        private readonly string onDeathSound;

        /// <summary>
        /// Create a sound container
        /// </summary>
        /// <param name="onActionSound">sound to play when a game object do an action</param>
        /// <param name="onCollisionSound">sound to play when a game object is in collision with another object</param>
        /// <param name="onDeathSound">sound to play when a game object is dying</param>
        public SoundHandler(string onActionSound, string onCollisionSound, string onDeathSound)
        {
            this.onActionSound = onActionSound;
            this.onCollisionSound = onCollisionSound;
            this.onDeathSound = onDeathSound;
        }

        /// <summary>
        /// OnAction Handler
        /// </summary>
        public void OnAction()
        {
            PlaySong(onActionSound);
        }

        /// <summary>
        /// OnCollision Handler
        /// </summary>
        public void OnCollision()
        {
            PlaySong(onCollisionSound);
        }

        /// <summary>
        /// On death handler
        /// </summary>
        public void OnDeath()
        {
            PlaySong(onDeathSound);
        }

        /// <summary>
        /// Play the song
        /// </summary>
        /// <param name="soundPath">sound to play</param>
        private void PlaySong(string soundPath)
        {
            if (soundPath != null)
            {
                SongManager.instance.PlaySoundEffect(soundPath);
            }
        }
    }
}
