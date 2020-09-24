using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;


// Add WindowsBase + PresentationCore
// https://www.spriters-resource.com/search/?q=mario
// https://themushroomkingdom.net/media/smb/wav
// https://themushroomkingdom.net/mp3.shtml

namespace SpaceInvaders.GameObjects.View.Sounds
{
    class SongManager
    {
        private SongManager() { }
        public static readonly SongManager instance = new SongManager();


        private readonly List<MediaPlayer> playlistSongs = new List<MediaPlayer>();
        private int index = 0;

        public void Load()
        {
            LoadPlayList(
                new List<string> { "background_music_1.wav" }
            );

        }

        private void LoadPlayList(List<string> urls)
        {

            foreach (string url in urls)
            {

                playlistSongs.Add(
                    CreateSong(
                        url,
                        (object o, EventArgs e) => {
                            index = (index + 1) % playlistSongs.Count;
                            playlistSongs[index].Play();
                        }
                    )
                );
            }

            if (playlistSongs.Count != 0)
                playlistSongs[0].Play();
        }

        public void AddVolatileSong(string url)
        {
            CreateSong(
                url,
                (object sender, EventArgs e) => { }
            ).Play();
        }

        private MediaPlayer CreateSong(string url, EventHandler callback)
        {

            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri(Path.Combine(Environment.CurrentDirectory, $@"..\..\Resources\{url}")));
            player.MediaEnded += callback;

            return player;
        }
    }

}
