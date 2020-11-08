using System.Collections.Generic;


// Add WindowsBase + PresentationCore
// https://www.spriters-resource.com/search/?q=mario
// https://themushroomkingdom.net/media/smb/wav
// https://themushroomkingdom.net/mp3.shtml

namespace SpaceInvaders.GameObjects.View.Sounds
{
    class SongManager
    {
        private SongManager() {}
        public static readonly SongManager instance = new SongManager();

        private List<string> playlistSongs;

        public void Load()
        {
            SongMap.instance.Load();
        }

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

        public void PlaySoundEffect(string url)
        { // SoundEffect SFX
            SongMap.instance.Play(url);
        }
    }

}
