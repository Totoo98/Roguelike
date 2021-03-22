using Roguelike.Turn;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Buffs
{
    public class BuffEffector : MonoBehaviour
    {
        [SerializeField] bool hasHunger = false;

        [SerializeField] List<Tuple<Buff, float>> buffs = new List<Tuple<Buff, float>>();

        private void Awake()
        {
            GetComponent<TurnEntity>().OnTimeSpent += ElapseTime;
            if(hasHunger)
            {
                BuffForThisAmountOfTime(new Hunger());
            }
        }

        private void OnDestroy()
        {
            GetComponent<TurnEntity>().OnTimeSpent -= ElapseTime;
        }

        private void ElapseTime(float time)
        {
            for(int i = buffs.Count - 1; i >= 0; i--)
            {
                if (buffs[i].Item1.IsPermanent)
                {
                    buffs[i].Item1.EffectOverTime(time, gameObject);
                }
                else
                {
                    var effectiveTime = time < buffs[i].Item2 ? time : buffs[i].Item2;
                    buffs[i].Item1.EffectOverTime(effectiveTime, gameObject);
                    buffs[i] = new Tuple<Buff, float>(buffs[i].Item1, buffs[i].Item2 - time);
                }
            }

            for (int i = buffs.Count - 1; i >= 0; i--)
            {
                if(!buffs[i].Item1.IsPermanent && buffs[i].Item2 <= 0)
                {
                    buffs[i].Item1.OnDebuff(gameObject);
                    buffs.RemoveAt(i);
                }
            }
        }

        public void BuffForThisAmountOfTime(Buff buff, float time = 0)
        {
            buff.OnBuff(gameObject);

            for (int i = buffs.Count - 1; i >= 0; i--)
            {
                if (buffs[i].Item1.GetType() == buff.GetType())
                {
                    buffs[i] = new Tuple<Buff, float>(buffs[i].Item1, buffs[i].Item2 + time);
                    return;
                }
            }

            buffs.Add(new Tuple<Buff, float>(buff, time));
        }

        public void ForceRemoveBuff(Buff buff)
        {
            for (int i = buffs.Count - 1; i >= 0; i--)
            {
                if (buffs[i].Item1.GetType() == buff.GetType())
                {
                    buffs[i].Item1.OnDebuff(gameObject);
                    buffs.RemoveAt(i);
                }
            }
        }
    }
}
