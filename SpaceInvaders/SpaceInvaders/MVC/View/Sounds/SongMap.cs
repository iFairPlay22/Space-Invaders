using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;

namespace SpaceInvaders.GameObjects.View.Sounds
{

    /// <summary>
    /// Represents a dictionnary that manages the songs with MediaPlayer
    /// </summary>
    class SongMap
    {
        /// <summary>
        /// Create a singleton : get the unique instance
        /// </summary>
        public static readonly SongMap Instance = new SongMap();

        /// <summary>
        /// Create a singleton : private construtor
        /// </summary>
        private SongMap() { }

        /// <summary>
        /// Each filename is linked with its MediaPlayer 
        /// Specified to SFX
        /// </summary>
        private readonly Dictionary<string, MediaPlayer> SFXDict = new Dictionary<string, MediaPlayer>();

        /// <summary>
        /// Each filename is linked with its MediaPlayer 
        /// Specified to songs
        /// </summary>
        private readonly List<MediaPlayer> Songs = new List<MediaPlayer>();

        /// <summary>
        /// Load all the songs (background music and sound effects)
        /// </summary>
        public void Load()
        {
            // SFX

            List<string> sfx = new List<string>{
                "sfx_pause.wav", "sfx_user_dead.wav", "sfx_ennemy_be_attacked.wav", 
                "sfx_ennemy_dead.wav", "sfx_user_be_attacked.wav",
                "sfx_fire_1.wav", "sfx_fire_2.wav"
            };

            LoadSFX(sfx);

            // Playlist of songs

            List<string> songs = new List<string>();
            for (int i = 1; i <= 5; i++)
                songs.Add($"background_{i}.wav");

            LoadPlaylist(songs);
        }

        /// <summary>
        /// Load few sound effect
        /// </summary>
        /// <param name="urls">paths of the SFX to load</param>
        private void LoadSFX(List<string> urls)
        {
            for (int i = 0; i < urls.Count; i++)
                SFXDict.Add(urls[i], CreateMediaPlayer(urls[i]));
        }

        /// <summary>
        /// Load a playlist
        /// </summary>
        /// <param name="urls">paths of the sounds to load</param>
        private void LoadPlaylist(List<string> urls)
        {
            for (int i = 0; i < urls.Count; i++)
                Songs.Add(CreateMediaPlayer(urls[i]));

            int length = urls.Count;

            for (int i = 0; i < length; i++)
                Songs[i].MediaEnded += (object o, EventArgs e) => Songs[(i + 1) % length].Play();
            
        }

        /// <summary>
        /// Create a media player linked to the song
        /// /// <summary>
        /// <param name="url">the path of the sound</param>
        /// <returns>The created MediaPlayer instance</returns>
        private MediaPlayer CreateMediaPlayer(string url)
        {
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri(Path.Combine(Environment.CurrentDirectory, $@"..\..\Resources\songs\{url}")));
            mediaPlayer.MediaEnded += (object o, EventArgs e) => mediaPlayer.Stop();

            return mediaPlayer;
        }
        
        /// <summary>
        /// Play a sound
        /// </summary>
        public void PlaySFX(string url)
        {
            GameException.RequireNonNull(SFXDict)[url].Play();
        }

        /// <summary>
        /// Stop a SFX
        /// </summary>
        public void StopSFX(string url)
        {
            GameException.RequireNonNull(SFXDict)[url].Stop();
        }

        /// <summary>
        /// Play the playlist
        /// </summary>
        public void PlayPlaylist()
        {
            StopPlaylist();

            if (GameException.RequireNonNull(Songs).Count != 0)
                Songs[0].Play();
        }

        /// <summary>
        /// Stop a the playlist
        /// </summary>
        public void StopPlaylist()
        {
            foreach (MediaPlayer songs in GameException.RequireNonNull(Songs))
                songs.Stop();
        }
    }
}

