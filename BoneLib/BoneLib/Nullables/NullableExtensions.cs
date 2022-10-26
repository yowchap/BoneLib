using Cysharp.Threading.Tasks;
using Il2CppSystem;
using SLZ;
using SLZ.AI;
using SLZ.Marrow.Data;
using SLZ.Marrow.Pool;
using SLZ.Utilities;
using SLZ.Zones;
using UnhollowerBaseLib;
using UnityEngine;
using UnityEngine.Audio;

namespace BoneLib.Nullables
{
    public static class NullableMethodExtensions
    {
        public static void Attenuate(this AudioPlayer inst, float? volume, float? pitch, float? minDistance)
        {
            inst.Attenuate(
                new BoxedNullable<float>(volume),
                new BoxedNullable<float>(pitch),
                new BoxedNullable<float>(minDistance));
        }

        public static void Play(this AudioPlayer inst, AudioClip clip, AudioMixerGroup mixerGroup, float? volume, bool? isLooping, float? pitch, float? minDistance)
        {
            inst.Play(clip, mixerGroup,
                new BoxedNullable<float>(volume),
                new BoxedNullable<bool>(isLooping),
                new BoxedNullable<float>(pitch),
                new BoxedNullable<float>(minDistance));
        }

        public static void Play(this AudioPlayer inst, AudioClip[] clips, AudioMixerGroup mixerGroup, float? volume, bool? isLooping, float? pitch, float? minDistance)
        {
            Il2CppReferenceArray<AudioClip> clipsArr = new Il2CppReferenceArray<AudioClip>(clips);
            inst.Play(clipsArr, mixerGroup,
                new BoxedNullable<float>(volume),
                new BoxedNullable<bool>(isLooping),
                new BoxedNullable<float>(pitch),
                new BoxedNullable<float>(minDistance));
        }

        public static void AudioPlayer_PlayAtPoint(AudioClip clip, Vector3 position, AudioMixerGroup mixerGroup, float? volume, bool? isLooping, float? pitch, float? minDistance)
        {
            AudioPlayer.PlayAtPoint(clip, position, mixerGroup,
                new BoxedNullable<float>(volume),
                new BoxedNullable<bool>(isLooping),
                new BoxedNullable<float>(pitch),
                new BoxedNullable<float>(minDistance));
        }

        public static void AudioPlayer_PlayAtPoint(AudioClip[] clips, Vector3 position, AudioMixerGroup mixerGroup, float? volume, bool? isLooping, float? pitch, float? minDistance)
        {
            Il2CppReferenceArray<AudioClip> clipsArr = new Il2CppReferenceArray<AudioClip>(clips);
            AudioPlayer.PlayAtPoint(clipsArr, position, mixerGroup,
                new BoxedNullable<float>(volume),
                new BoxedNullable<bool>(isLooping),
                new BoxedNullable<float>(pitch),
                new BoxedNullable<float>(minDistance));
        }

        public static void SetTrigger(this AITrigger inst, float? radius, float? fov, TriggerManager.TriggerTypes? type)
        {
            inst.SetTrigger(
                new BoxedNullable<float>(radius),
                new BoxedNullable<float>(fov),
            new BoxedNullable<TriggerManager.TriggerTypes>(type));
        }

        public static UniTask<AssetPoolee> Spawn(this AssetPool inst, Vector3 position, Quaternion rotation, Vector3? scale = null, bool? autoEnable = null)
        {
            return inst.Spawn(position, rotation,
                new BoxedNullable<Vector3>(scale),
                new BoxedNullable<bool>(autoEnable));
        }

        public static UniTask<AssetPoolee> PoolManager_SpawnAsync(Spawnable spawnable,
                                                                  Vector3 position = default,
                                                                  Quaternion rotation = default,
                                                                  Vector3? scale = null,
                                                                  bool ignorePolicy = false,
                                                                  int? groupID = null,
                                                                  Action<GameObject> spawnCallback = null,
                                                                  Action<GameObject> despawnCallback = null)
        {
            return AssetSpawner.SpawnAsync(spawnable, position, rotation, new BoxedNullable<Vector3>(scale), ignorePolicy, new BoxedNullable<int>(groupID), spawnCallback, despawnCallback);
        }

        public static void PoolManager_Spawn(Spawnable spawnable,
                                             Vector3 position = default,
                                             Quaternion rotation = default,
                                             Vector3? scale = null,
                                             bool ignorePolicy = false,
                                             int? groupID = null,
                                             Action<GameObject> spawnCallback = null,
                                             Action<GameObject> despawnCallback = null)
        {
            AssetSpawner.Spawn(spawnable, position, rotation, new BoxedNullable<Vector3>(scale), ignorePolicy, new BoxedNullable<int>(groupID), spawnCallback, despawnCallback);
        }

        public static void SetRigidbody(this SpawnFragment inst, int idx, Vector3? velocity, Vector3? angularVelocity, float? mass, Vector3? worldCenter, float? explosiveForce)
        {
            inst.SetRigidbody(idx,
                new BoxedNullable<Vector3>(velocity),
                new BoxedNullable<Vector3>(angularVelocity),
                new BoxedNullable<float>(mass),
                new BoxedNullable<Vector3>(worldCenter),
                new BoxedNullable<float>(explosiveForce));
        }

        public static void Despawn(this ZoneTracker inst, bool? playVFX)
        {
            inst.Despawn(new BoxedNullable<bool>(playVFX));
        }
    }
}