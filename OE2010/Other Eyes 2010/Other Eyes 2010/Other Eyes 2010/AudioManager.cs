using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Other_Eyes_2010
{
    class AudioManager
    {
        #region Attributes
        public Dictionary<string,SoundEffect> soundEffects;
        public List<Song> backgroundSongs;
        public ContentManager content;
        public int songIndex = 0;
        public static const float MUSIC_VOLUME = 0.5f;
        public static const float SFX_VOLUME = 0.7f;
        #endregion

        #region Constructor
        public AudioManager(ContentManager c)
        {
            content = c;

            soundEffects = new Dictionary<string, SoundEffect>();
            backgroundSongs = new List<Song>();

            MediaPlayer.Volume = MUSIC_VOLUME;
            
            
            LoadContent();
        }
        #endregion

        #region Functions

        public void Update()
        {
            if (MediaPlayer.State == MediaState.Stopped)
                PlayNextSong();
        }
        public void LoadContent()
        {
            backgroundSongs.Add(content.Load<Song>("heaven_descends"));
            backgroundSongs.Add(content.Load<Song>("ringing_of_bells"));
        }
        
        public void PlayNextSong()
        {
            songIndex++;
            if (songIndex >= backgroundSongs.Count)
                songIndex = 0;

            if (MediaPlayer.State == MediaState.Playing)
                MediaPlayer.Stop();

            MediaPlayer.Play(backgroundSongs[songIndex]);
        }

        public void PlaySoundEffect(string name)
        {
            SoundEffectInstance effect;
            effect = soundEffects[name].CreateInstance();

            effect.Volume = SFX_VOLUME;
            if (effect.State != SoundState.Playing)
                effect.Play();
        }
        #endregion
    }
}
