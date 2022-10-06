using SLZ.AI;
using SLZ.Combat;
using SLZ.Props.Weapons;
using System;

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
    }
}

// For backwards compatibility with v1.0.0 having the wrong namespace
namespace Bonelib
{
    public static class Extensions
    {
        [Obsolete("Use BoneLib.Extensions.SetRpm() instead (different namespace). This will be removed in future versions.", true)]
        public static void SetRpm(this Gun gun, float rpm) => BoneLib.Extensions.SetRpm(gun, rpm);

        [Obsolete("Use BoneLib.Extensions.DealDamage() instead (different namespace). This will be removed in future versions.", true)]
        public static void DealDamage(this AIBrain brain, float damage) => BoneLib.Extensions.DealDamage(brain, damage);
    }
}