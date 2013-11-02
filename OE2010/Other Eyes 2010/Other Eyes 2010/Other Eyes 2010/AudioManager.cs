#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
#endregion

namespace Other_Eyes_2010
{
    class AudioManager
    {
        #region Attributes
        private Dictionary<string,SoundEffect> soundEffects;
        private List<Song> backgroundSongs;
        private ContentManager content;
        private int songIndex = 0;
        private static const float MUSIC_VOLUME = 0.5f;
        private static const float SFX_VOLUME = 0.7f;
        #endregion

        #region Properties
        public List<string> SoundEffects
        {
            get { return soundEffects.Keys.ToList(); }
        }
        #endregion

        #region Constructor


        /// <summary>
        /// Creates an instance of the audio manager class.
        /// </summary>
        /// <param name="c">The game's content manager</param>
        public AudioManager(ContentManager c)
        {
            content = c;

            soundEffects = new Dictionary<string, SoundEffect>();
            backgroundSongs = new List<Song>();

            MediaPlayer.Volume = MUSIC_VOLUME;
            
            
            LoadContent();
        }
        #endregion

        #region Public Functions

        /// <summary>
        /// Pauses the music.
        /// </summary>
        public void Pause()
        {
            if (MediaPlayer.State != MediaState.Paused)
                MediaPlayer.Pause();
        }

        /// <summary>
        /// Resumes the music
        /// </summary>
        public void Resume()
        {
            if (MediaPlayer.State != MediaState.Playing)
                MediaPlayer.Resume();
        }

        /// <summary>
        /// Basic update. Checks to see if the current song is done, and if so, plays the next one.
        /// </summary>
        public void Update()
        {
            if (MediaPlayer.State == MediaState.Stopped)
                PlayNextSong();
        }

        /// <summary>
        /// Gets an effect and plays it.
        /// </summary>
        /// <param name="name">The name of the effect to play</param>
        public void PlaySoundEffect(string name)
        {
            SoundEffectInstance effect;
            effect = soundEffects[name].CreateInstance();

            effect.Volume = SFX_VOLUME;
            if (effect.State != SoundState.Playing)
                effect.Play();
        }


        /// <summary>
        /// Stops the current song, if necessary, and plays the next one in the list.
        /// </summary>
        public void PlayNextSong()
        {
            songIndex++;
            if (songIndex >= backgroundSongs.Count)
                songIndex = 0;

            if (MediaPlayer.State == MediaState.Playing)
                MediaPlayer.Stop();

            MediaPlayer.Play(backgroundSongs[songIndex]);
        }
        #endregion

        #region Private Functions

        /// <summary>
        /// Loads audio content into respective containers.
        /// </summary>
        private void LoadContent()
        {
            backgroundSongs.Add(content.Load<Song>("heaven_descends"));
            backgroundSongs.Add(content.Load<Song>("ringing_of_bells"));
        }
        #endregion


    }
}
