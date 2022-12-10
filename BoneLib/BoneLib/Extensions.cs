using SLZ.AI;
using SLZ.Combat;
using SLZ.Props.Weapons;
using System;
using System.Linq;

namespace BoneLib
{
    public static class Extensions
    {
        /// <summary>
        /// Set rounds-per-minute.
        /// </summary>
        public static void SetRpm(this Gun gun, float rpm)
        {
            gun.roundsPerMinute = rpm;
            gun.roundsPerSecond = rpm / 60f;
            gun.fireDuration = 60f / rpm;
        }

        public static void DealDamage(this AIBrain brain, float damage)
        {
            var health = brain?.behaviour?.health;
            if (health != null)
            {
                health.TakeDamage(1, new Attack()
                {
                    damage = damage
                });
            }
        }

        public static void InvokeActionSafe(this Action action) => SafeActions.InvokeActionSafe(action);
        public static void InvokeActionSafe<T>(this Action<T> action, T param) => SafeActions.InvokeActionSafe(action, param);
        public static void InvokeActionSafe<T1, T2>(this Action<T1, T2> action, T1 param1, T2 param2) => SafeActions.InvokeActionSafe(action, param1, param2);

        public static T GetRandom<T>(this System.Collections.Generic.List<T> list)
        {
            int random = UnityEngine.Random.Range(0, list.Count);
            return list.ElementAt<T>(random);
        }
    }
}