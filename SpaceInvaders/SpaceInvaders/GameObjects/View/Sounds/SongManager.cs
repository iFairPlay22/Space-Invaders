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


        private List<MediaPlayer> playlistSongs;
        private int index = 0;

        private readonly HashSet<MediaPlayer> volatileSongs = new HashSet<MediaPlayer>();

        public void CreatePlayList(List<string> urls)
        {
            if (playlistSongs != null)
                playlistSongs.ForEach((MediaPlayer m) => m.Stop());
            
            index = 0;
            playlistSongs = new List<MediaPlayer>();

            foreach (string url in urls)
            {
                MediaPlayer player = new MediaPlayer();
                player.Open(new Uri(Path.Combine(Environment.CurrentDirectory, $@"..\..\Resources\{url}")));
                player.MediaEnded += (object o, EventArgs e) => {
                    playlistSongs[index].Stop();
                    index = (index + 1) % playlistSongs.Count;
                    playlistSongs[index].Play();
                };
                playlistSongs.Add(player);
            }

            if (playlistSongs.Count != 0)
                playlistSongs[0].Play();
        }

        public void AddVolatileSong(string url)
        {
            MediaPlayer player = new MediaPlayer();
            
            // Callbacks
            player.MediaOpened += (object o, EventArgs e) => {
                volatileSongs.Add(player);
                player.Play();
            };
            player.MediaEnded += (object o, EventArgs e) => {
                volatileSongs.Remove(player);
            };

            // Action
            player.Open(new Uri(Path.Combine(Environment.CurrentDirectory, $@"..\..\Resources\{url}")));
        }
    }

}
