using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace BoneLib
{
    public static class Audio
    {
        private static bool HasFoundMixers => MusicMixer != null && SFXMixer != null && GunshotMixer != null && MasterMixer != null;

        public static AudioMixerGroup MasterMixer { get; private set; }
        public static AudioMixerGroup MusicMixer { get; private set; }
        public static AudioMixerGroup SFXMixer { get; private set; }
        public static AudioMixerGroup GunshotMixer { get; private set; }


        /// <summary>
        /// Finds the music and sfx audio mixers.
        /// </summary>
        internal static void GetAudioMixers()
        {
            if (HasFoundMixers)
                return;

            AudioMixerGroup[] mixers = Resources.FindObjectsOfTypeAll<AudioMixerGroup>();
            MasterMixer = mixers.FirstOrDefault(x => x.name == "Master");
            MusicMixer = mixers.FirstOrDefault(x => x.name == "Music");
            SFXMixer = mixers.FirstOrDefault(x => x.name == "SFX");
            GunshotMixer = mixers.FirstOrDefault(x => x.name == "GunShot");
        }
    }
}
