

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
        private readonly string OnActionSound;

        /// <summary>
        /// Sound to play when a game object is in collision with another object (ex: shooted)
        /// </summary>
        private readonly string OnCollisionSound;

        /// <summary>
        /// Sound to play when a game object is dying
        /// </summary>
        private readonly string OnDeathSound;

        /// <summary>
        /// Create a sound container
        /// </summary>
        /// <param name="onActionSound">sound to play when a game object do an action</param>
        /// <param name="onCollisionSound">sound to play when a game object is in collision with another object</param>
        /// <param name="onDeathSound">sound to play when a game object is dying</param>
        public SoundHandler(string onActionSound, string onCollisionSound, string onDeathSound)
        {
            OnActionSound = onActionSound;
            OnCollisionSound = onCollisionSound;
            OnDeathSound = onDeathSound;
        }

        /// <summary>
        /// OnAction Handler
        /// </summary>
        public void OnAction()
        {
            PlaySong(OnActionSound);
        }

        /// <summary>
        /// OnCollision Handler
        /// </summary>
        public void OnCollision()
        {
            PlaySong(OnCollisionSound);
        }

        /// <summary>
        /// On death handler
        /// </summary>
        public void OnDeath()
        {
            PlaySong(OnDeathSound);
        }

        /// <summary>
        /// Play the song
        /// </summary>
        /// <param name="soundPath">sound to play</param>
        private void PlaySong(string soundPath)
        {
            if (soundPath != null)
            {
                SongMap.Instance.PlaySFX(soundPath);
            }
        }
    }
}
