using UnityEngine;
using UnityEngine.Audio;

using Il2CppSLZ.Marrow.Audio;

namespace BoneLib
{
    public static class Audio
    {
        public static AudioMixerGroup Ambience => Audio3dManager.ambience;
        public static AudioMixerGroup BulletImpact => Audio3dManager.bulletImpact;
        public static AudioMixerGroup DiegeticMusic => Audio3dManager.diegeticMusic;
        public static AudioMixerGroup Footsteps => Audio3dManager.footsteps;
        public static AudioMixerGroup Gunshot => Audio3dManager.gunShot;
        public static AudioMixerGroup HardInteraction => Audio3dManager.hardInteraction;
        public static AudioMixerGroup InHead => Audio3dManager.inHead;
        public static AudioMixerGroup Impact => Audio3dManager.impact;
        public static AudioMixerGroup NonDiegeticMusic => Audio3dManager.nonDiegeticMusic;
        public static AudioMixerGroup NPCVocals => Audio3dManager.npcVocals;
        public static AudioMixerGroup Shell => Audio3dManager.shells;
        public static AudioMixerGroup SoftInteraction => Audio3dManager.softInteraction;
        public static AudioMixerGroup UI => Audio3dManager.ui;

        public static void PlayOneShot(AudioClip clip, Vector3 position, AudioMixerGroup mixerGroup, float volume = 1f, float pitch = 1f) => Audio3dManager.PlayAtPoint(clip, position, mixerGroup, volume, pitch);
        public static void SpawnAudioPlayer(Vector3 position = default, System.Action<GameObject> callback = null) => Audio3dManager.SpawnAudioPlayer(position, callback);
    }
}
