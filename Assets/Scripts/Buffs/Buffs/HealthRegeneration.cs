using Roguelike.Combat;
using UnityEngine;

namespace Roguelike.Buffs
{
    public class HealthRegeneration : Buff
    {
        //public override BuffType Type => BuffType.HealthRegeneration;

        public override bool IsPermanent => false;

        int healthPerStep = 2;
        float timeStep = 0.5f;
        float currentTime = 0f;

        public override void EffectOverTime(float time, GameObject user)
        {
            currentTime += time;
            int count = 0;
            while(currentTime >= timeStep)
            {
                currentTime -= timeStep;
                Effect(user);
                count++;
            }
        }

        private void Effect(GameObject user)
        {
            user.GetComponent<Health>().Heal(healthPerStep);
        }
    }
}
