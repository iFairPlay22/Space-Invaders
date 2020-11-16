using System.Collections.Generic;


// Add WindowsBase + PresentationCore

namespace SpaceInvaders.GameObjects.View.Sounds
{

    /// <summary>
    /// Represents the interface between the game (Game + SoundHandlers)
    /// and the MediaPlayers (SongMap)
    /// <summary>
    class SongManager
    {
        /// <summary>
        /// Create a singleton
        /// <summary>
        private SongManager() {}

        /// <summary>
        /// Get the unique SongManager instance
        /// <summary>
        public static readonly SongManager instance = new SongManager();

        /// <summary>
        /// The last playlist used 
        /// <summary>
        private List<string> playlistSongs;

        /// <summary>
        /// Load all the MediaPlayers before playing any songs
        /// <summary>
        public void Load()
        {
            SongMap.instance.Load();
        }

        /// <summary>
        /// Play a playlist
        /// <summary>
        public void PlaySongs(List<string> urls)
        {
            if (playlistSongs != null)
            {
                playlistSongs.ForEach(
                    (string songName) => SongMap.instance.Stop(songName)
                );
            }
            
            playlistSongs = urls;


            if (playlistSongs.Count != 0)
            {
                SongMap.instance.MakePlaylist(playlistSongs);
                SongMap.instance.Play(playlistSongs[0]);
            }
            
        }

        /// <summary>
        /// Play a sound effect
        /// <summary>
        public void PlaySoundEffect(string url)
        { 
            SongMap.instance.Play(url);
        }
    }

}
