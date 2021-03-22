using Roguelike.Interactions;
using UnityEngine;

namespace Roguelike.Buffs
{
    public class Paralisys : Buff
    {
        //public override BuffType Type => BuffType.Paralysis;

        public override bool IsPermanent => false;

        public override void OnBuff(GameObject user)
        {
            user.GetComponent<LivingEntity>().IsParalysed = true;
        }

        public override void OnDebuff(GameObject user)
        {
            user.GetComponent<LivingEntity>().IsParalysed = false;
        }
    }
}
