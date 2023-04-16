using Il2CppCysharp.Threading.Tasks;
using Il2CppSystem;
using Il2CppSLZ;
using Il2CppSLZ.AI;
using Il2CppSLZ.Marrow.Data;
using Il2CppSLZ.Marrow.Pool;
using Il2CppSLZ.Props;
using Il2CppSLZ.Utilities;
using Il2CppSLZ.Zones;
using UnityEngine;
using UnityEngine.Audio;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace BoneLib.Nullables
{
    /// <summary>
    /// Wraps BONELAB methods that use nullable types, so you can easily use them on the mono side.
    /// </summary>
    public static class NullableMethodExtensions
    {
        public static void Attenuate(this AudioPlayer inst, float? volume = null, float? pitch = null, float? minDistance = null)
        {
            inst.Attenuate(
                new BoxedNullable<float>(volume),
                new BoxedNullable<float>(pitch),
                new BoxedNullable<float>(minDistance));
        }

        public static void Play(this AudioPlayer inst, AudioClip clip, AudioMixerGroup mixerGroup = null, float? volume = null, bool? isLooping = null, float? pitch = null, float? minDistance = null)
        {
            inst.Play(clip, mixerGroup,
                new BoxedNullable<float>(volume),
                new BoxedNullable<bool>(isLooping),
                new BoxedNullable<float>(pitch),
                new BoxedNullable<float>(minDistance));
        }

        public static void Play(this AudioPlayer inst, AudioClip[] clips, AudioMixerGroup mixerGroup = null, float? volume = null, bool? isLooping = null, float? pitch = null, float? minDistance = null)
        {
            Il2CppReferenceArray<AudioClip> clipsArr = new Il2CppReferenceArray<AudioClip>(clips);
            inst.Play(clipsArr, mixerGroup,
                new BoxedNullable<float>(volume),
                new BoxedNullable<bool>(isLooping),
                new BoxedNullable<float>(pitch),
                new BoxedNullable<float>(minDistance));
        }

        public static void AudioPlayer_PlayAtPoint(AudioClip clip, Vector3 position, AudioMixerGroup mixerGroup = null, float? volume = null, bool? isLooping = null, float? pitch = null, float? minDistance = null)
        {
            AudioPlayer.PlayAtPoint(clip, position, mixerGroup,
                new BoxedNullable<float>(volume),
                new BoxedNullable<bool>(isLooping),
                new BoxedNullable<float>(pitch),
                new BoxedNullable<float>(minDistance));
        }

        public static void AudioPlayer_PlayAtPoint(AudioClip[] clips, Vector3 position, AudioMixerGroup mixerGroup = null, float? volume = null, bool? isLooping = null, float? pitch = null, float? minDistance = null)
        {
            Il2CppReferenceArray<AudioClip> clipsArr = new Il2CppReferenceArray<AudioClip>(clips);
            AudioPlayer.PlayAtPoint(clipsArr, position, mixerGroup,
                new BoxedNullable<float>(volume),
                new BoxedNullable<bool>(isLooping),
                new BoxedNullable<float>(pitch),
                new BoxedNullable<float>(minDistance));
        }

        public static void SetTrigger(this AITrigger inst, float? radius = null, float? fov = null, TriggerManager.TriggerTypes? type = null)
        {
            inst.SetTrigger(
                new BoxedNullable<float>(radius),
                new BoxedNullable<float>(fov),
            new BoxedNullable<TriggerManager.TriggerTypes>(type));
        }

        public static UniTask<AssetPoolee> Spawn(this AssetPool inst, Vector3 position = default, Quaternion rotation = default, Vector3? scale = null, bool? autoEnable = null)
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

        public static void SetRigidbody(this SpawnFragment inst, int idx, Vector3? velocity = null, Vector3? angularVelocity = null, float? mass = null, Vector3? worldCenter = null, float? explosiveForce = null)
        {
            inst.SetRigidbody(idx,
                new BoxedNullable<Vector3>(velocity),
                new BoxedNullable<Vector3>(angularVelocity),
                new BoxedNullable<float>(mass),
                new BoxedNullable<Vector3>(worldCenter),
                new BoxedNullable<float>(explosiveForce));
        }

        public static void Despawn(this ZoneTracker inst, bool? playVFX = null)
        {
            inst.Despawn(new BoxedNullable<bool>(playVFX));
        }
    }
}