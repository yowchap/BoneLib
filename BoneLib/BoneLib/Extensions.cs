using SLZ.AI;
using SLZ.Combat;
using SLZ.Props.Weapons;

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