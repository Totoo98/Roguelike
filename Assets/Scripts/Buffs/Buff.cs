using UnityEngine;

namespace Roguelike.Buffs
{
    public abstract class Buff
    {
        public abstract bool IsPermanent { get; }

        /*protected float timeRemaining = 0f;
        public void SetTime(float time) => timeRemaining = time;
        public bool IsOver() => IsPermanent ? false : timeRemaining <= 0;*/

        public virtual void OnBuff(GameObject user) { }
        public virtual void OnDebuff(GameObject user) { }

        /*public void ElapseTime(float time, GameObject user)
        {
            if (IsPermanent)
            {
                EffectOverTime(time, user);
            }
            else
            {
                var effectiveTime = time < timeRemaining ? time : timeRemaining;
                EffectOverTime(effectiveTime, user);
                timeRemaining -= time;
            }
        }*/

        public virtual void EffectOverTime(float time, GameObject user) { }
    }
}
