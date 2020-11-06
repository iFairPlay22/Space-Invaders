using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Media;

namespace SpaceInvaders.GameObjects.View.Sounds
{
    class SongMap
    {
        public static readonly SongMap instance = new SongMap();

        private Dictionary<string, MediaPlayer> Dict;

        private SongMap() { }

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

        public void Play(string url)
        {
            GameException.RequireNonNull(Dict)[url].Play();
        }
        public void Stop(string url)
        {
            GameException.RequireNonNull(Dict)[url].Stop();
        }

        public void MakePlaylist(List<string> urls)
        {
            int length = GameException.RequireNonNull(urls).Count;

            for (int i = 0; i < length; i++)
                GameException.RequireNonNull(Dict)[urls[i]].MediaEnded += (object o, EventArgs e) =>
                    Play(urls[(i + 1) % length]);

        }
    }
}

