using SLZ.AI;
using SLZ.Props.Weapons;
using SLZ.Combat;

namespace Bonelib
{
    public static class Extensions
    {
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
