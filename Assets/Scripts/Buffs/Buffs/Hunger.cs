using Roguelike.Combat;
using UnityEngine;

namespace Roguelike.Buffs
{
    public class Hunger : Buff
    {
        //public override BuffType Type => BuffType.Hunger;

        public override bool IsPermanent => true;

        int starvingDamage = 1;

        float timeStep = 5f;
        float currentStarvingTime = 0f;
        float currentTime = 0f;
        float starvingLimit = 50f;

        public void Heal(float time)
        {
            currentTime = currentTime - time < 0 ? 0 : currentTime - time;
        }

        public override void EffectOverTime(float time, GameObject user)
        {
            bool wasAlreadyStarving = currentTime > starvingLimit;

            currentTime += time;

            if(currentTime > starvingLimit)
            {
                float starvingTime = wasAlreadyStarving ? time : currentTime - starvingLimit;
                currentStarvingTime += starvingTime;
            }

            while(currentStarvingTime > timeStep)
            {
                currentStarvingTime -= timeStep;
                EffectOnTimeStep(user);
            }
        }

        private void EffectOnTimeStep(GameObject user)
        {
            user.GetComponent<Health>().Damage(starvingDamage);
        }
    }
}
