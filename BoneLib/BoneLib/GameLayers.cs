namespace BoneLib
{
    /// <summary>
    /// <see cref="UnityEngine.LayerMask"/> values for BoneLab.
    /// </summary>
    /// <remarks>
    /// Can be used with <see href="https://en.wikipedia.org/wiki/Mask_(computing)">bitwise</see> 
    /// <see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators">operators</see>.
    /// </remarks>
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public enum GameLayers
    {
        DEFAULT = 1 << 0,
        TRANSPARENT_FX = 1 << 1,
        IGNORE_RAYCAST = 1 << 2,
        WATER = 1 << 4,
        UI = 1 << 5,
        PLAYER = 1 << 8,
        NO_COLLIDE = 1 << 9,
        DYNAMIC = 1 << 10,
        STEREO_RENDER_IGNORE = 1 << 11,
        ENEMY_COLLIDERS = 1 << 12,
        STATIC = 1 << 13,
        SPAWNGUN_UI = 1 << 14,
        INTERACTABLE = 1 << 15,
        HAND = 1 << 16,
        HAND_ONLY = 1 << 17,
        SOCKET = 1 << 18,
        PLUG = 1 << 19,
        INTERACTABLE_ONLY = 1 << 20,
        PLAYER_AND_NPC = 1 << 21,
        NO_SELF_COLLIDE = 1 << 22,
        FEET_ONLY = 1 << 23,
        FEET = 1 << 24,
        NO_FOOTBALL = 1 << 25,
        TRACKER = 1 << 26,
        TRIGGER = 1 << 27,
        BACKGROUND = 1 << 31,
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member