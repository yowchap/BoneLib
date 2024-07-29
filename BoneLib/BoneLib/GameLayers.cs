namespace BoneLib
{
    /// <summary>
    /// <see cref="UnityEngine.LayerMask"/> values for BoneLab.
    /// </summary>
    /// <remarks>
    /// Can be used with <see href="https://en.wikipedia.org/wiki/Mask_(computing)">bitwise</see> 
    /// <see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators">operators</see>.
    /// </remarks>
    public enum GameLayers
    {
        DEFAULT = 1 << 0,
        TRANSPARENT_FX = 1 << 1,
        IGNORE_RAYCAST = 1 << 2,
        OBSERVER_TRIGGER = 1 << 3,
        WATER = 1 << 4,
        UI = 1 << 5,
        FIXTURE = 1 << 6,
        PLAYER = 1 << 8,
        NO_COLLIDE = 1 << 9,
        DYNAMIC = 1 << 10,
        ENEMY_COLLIDERS = 1 << 12,
        INTERACTABLE = 1 << 15,
        DECAVERSE = 1 << 16,
        DECIVERSE = 1 << 17,
        SOCKET = 1 << 18,
        PLUG = 1 << 19,
        PLAYER_AND_NPC = 1 << 21,
        FEET_ONLY = 1 << 23,
        FEET = 1 << 24,
        NO_FOOTBALL = 1 << 25,
        ENTITY_TRACKER = 1 << 26,
        BEING_TRACKER = 1 << 27,
        OBSERVER_TRACKER = 1 << 28,
        ENTITY_TRIGGER = 1 << 29,
        BEING_TRIGGER = 1 << 30,
        BACKGROUND = 1 << 31,
    }
}
