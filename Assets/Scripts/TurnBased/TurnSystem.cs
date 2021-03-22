using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Turn
{
    public class TurnSystem : MonoBehaviour
    {
        [SerializeField] TurnEntityList entities = null;

        [SerializeField] TurnEntity current = null;
        [SerializeField] float currentTime = 0f;

        [SerializeField] int maxTurnPerFrame = 100;

        //public event Action OnAnyEndOfTurn;


        private void Update()
        {
            var haveLoopedThisFrame = new HashSet<TurnEntity>();
            int maxTurnPerFrame = this.maxTurnPerFrame;
            while(maxTurnPerFrame-- > 0)
            {
                if (current == null) StartNextEntityTurn();
                if (current == null) break;
                if (!current.ReadyForNextTurn || haveLoopedThisFrame.Contains(current))
                {
                    break;
                }

                current.HandleTurn();
                if(current == null)
                {
                    print("it's fucking null");
                }

                bool hasTurnFinished = current.TimeOfNextTurn != currentTime;

                haveLoopedThisFrame.Add(current);

                if (hasTurnFinished)
                {
                    //OnAnyEndOfTurn?.Invoke();
                    current = null;
                }
                else
                {
                    break;
                }
            }

            haveLoopedThisFrame.Clear();

            if(maxTurnPerFrame == 0) print("maxAllowedPerFrame (" + this.maxTurnPerFrame + ") exceeded");
        }

        private void StartNextEntityTurn()
        {
            if (entities.List.Count == 0) return;

            float nextTime = Mathf.Infinity;

            foreach(var entity in entities.List)
            {
                if(entity.TimeOfNextTurn < nextTime)
                {
                    nextTime = entity.TimeOfNextTurn;
                    current = entity;
                }
            }

            ElapseTimeTo(nextTime);
        }

        private void ElapseTimeTo(float nextTime)
        {
            float timeElapsed = nextTime - currentTime;

            foreach(var entity in entities.List)
            {
                entity.ElapseTime(timeElapsed);
            }

            currentTime = nextTime;
        }
    }
}

