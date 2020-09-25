using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.GameObjects.View.Sounds
{
    class SoundHandler
    {
        private readonly string onActionSound;
        private readonly string onCollisionSound;
        private readonly string onDeathSound;

        public SoundHandler(string onActionSound, string onCollisionSound, string onDeathSound)
        {
            this.onActionSound = onActionSound;
            this.onCollisionSound = onCollisionSound;
            this.onDeathSound = onDeathSound;
        }

        public void OnAction()
        {
            PlaySong(onActionSound);
        }

        public void OnCollision()
        {
            PlaySong(onCollisionSound);
        }

        public void OnDeath()
        {
            PlaySong(onDeathSound);
        }

        private void PlaySong(string soundPath)
        {
            if (soundPath != null)
                SongManager.instance.AddVolatileSong(soundPath);
        }
    }
}
