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
        public static readonly SongMap instance = new SongMap();

        /// <summary>
        /// Create a singleton : private construtor
        /// </summary>
        private SongMap() { }

        /// <summary>
        /// Each filename is linked with its MediaPlayer
        /// </summary>
        private Dictionary<string, MediaPlayer> Dict;

        /// <summary>
        /// Load all the songs (background music and sound effects)
        /// </summary>
        public void Load()
        {
            string[] songs = {
                "background_1.wav", "background_2.wav", "background_3.wav",
                "background_4.wav", "background_5.wav", "sfx_pause.wav",
                "sfx_user_dead.wav", "sfx_ennemy_be_attacked.wav", 
                "sfx_ennemy_dead.wav", "sfx_user_be_attacked.wav",
                "sfx_fire_1.wav", "sfx_fire_2.wav"
            };

            Dict = new Dictionary<string, MediaPlayer>();

            for (int i = 0; i < songs.Length; i++)
                Dict.Add(songs[i], CreateMediaPlayer(songs[i]));
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
            mediaPlayer.MediaEnded += (object o, EventArgs e) =>
            {
                mediaPlayer.Stop();
            };
            return mediaPlayer;
        }

        /// <summary>
        /// Play a song
        /// </summary>
        public void Play(string url)
        {
            GameException.RequireNonNull(Dict)[url].Play();
        }

        /// <summary>
        /// Stop a song
        /// </summary>
        public void Stop(string url)
        {
            GameException.RequireNonNull(Dict)[url].Stop();
        }

        /// <summary>
        /// Create a playlist
        /// url[0] -> url[1] -> ... -> url[n] -> url[0]
        /// </summary>
        public void MakePlaylist(List<string> urls)
        {
            GameException.RequireNonNull(Dict);
            int length = GameException.RequireNonNull(urls).Count;

            for (int i = 0; i < length; i++)
            {
                string nextUrl  = urls[(i + 1) % length];
                Dict[urls[i]].MediaEnded += (object o, EventArgs e) => Play(nextUrl);
            }

        }
    }
}

