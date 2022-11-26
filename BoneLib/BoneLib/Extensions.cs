using SLZ.AI;
using SLZ.Combat;
using SLZ.Props.Weapons;

using SLZ.Marrow.Pool;
using SLZ.Marrow.Warehouse;

using System.Collections.Generic;

using UnityEngine;

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

        public static T GetRandom<T>(this List<T> list) where T : class
        {
            int random = UnityEngine.Random.Range(0, list.Count);
            return list[random];
        }
    }
}