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
                "background_end.wav", "background_game_1.wav", "background_game_2.wav",
                "background_game_3.wav", "background_intro.wav", "volatile_defeat.wav",
                "volatile_ennemy_be_attacked.wav", "volatile_ennemy_dead.wav", "volatile_fire_1.wav",
                "volatile_fire_2.wav", "volatile_pause.wav", "volatile_projectile_die.wav",
                "volatile_user_be_attacked.wav", "volatile_victory.wav"
            };

            Dict = new Dictionary<string, MediaPlayer>();

            for (int i = 0; i < songs.Length; i++)
                Dict.Add(songs[i], CreateMediaPlayer(songs[i]));
        }

        /// <summary>
        /// Create a media player linked to the song
        /// /// <summary>
        /// <param name="url">the path of the sound</param>
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
            int length = GameException.RequireNonNull(urls).Count;

            for (int i = 0; i < length; i++)
                GameException.RequireNonNull(Dict)[urls[i]].MediaEnded += (object o, EventArgs e) =>
                    Play(urls[(i + 1) % length]);

        }
    }
}

