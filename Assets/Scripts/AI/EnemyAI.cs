using Roguelike.Interactions;
using UnityEngine;
using Roguelike.CellMapping;
using Roguelike.Movement;
using Roguelike.Combat;

namespace Roguelike.AI
{
    public class EnemyAI : Interactor
    {
        [SerializeField] protected CellMap CellMap = null;
        [SerializeField] protected AIState state = new Sleeping();

        CellElement cellElement;

        private void ChangeState(AIState nextState)
        {
            state.OnExit(this);
            state = nextState;
            state.OnEnter(this);
        }

        protected override void Awake()
        {
            base.Awake();
            cellElement = GetComponent<CellElement>();
        }

        public override void GetInteraction()
        {
            state.Update(this);
        }



        public abstract class AIState
        {
            public virtual void OnEnter(EnemyAI interactor) { }
            public abstract void Update(EnemyAI interactor);
            public virtual void OnExit(EnemyAI interactor) { }
        }

        public class Sleeping : AIState
        {
            float awakeRange = 5f;

            public override void Update(EnemyAI interactor)
            {
                Collider2D collider = Physics2D.OverlapCircle(interactor.cellElement.Cell.Coords, awakeRange, (int)LayerBit.Player);
                if(collider != null)
                {
                    interactor.ChangeState(new Hunting(collider.GetComponent<Health>()));
                }
                interactor.turnEntity.SpendTime(1f);
            }
        }

        public class Wandering : AIState
        {
            int timeToSleep = 10;
            int countUntilSleep = 0;
            float huntRange = 5f;

            public override void Update(EnemyAI interactor)
            {
                interactor.cellElement.TryToMoveToANewCell(interactor.cellElement.CellMap.GetRandomAdjacentWalkableCell(interactor.cellElement.Cell));
                interactor.turnEntity.SpendTime(1f);

                Collider2D collider = Physics2D.OverlapCircle(interactor.cellElement.Cell.Coords, huntRange + 1, (int)LayerBit.Player);
                if (collider == null)
                {
                    timeToSleep++;
                    if(timeToSleep >= countUntilSleep)
                    {
                        interactor.ChangeState(new Sleeping());
                    }
                }
                else
                {
                    interactor.ChangeState(new Hunting(collider.GetComponent<Health>()));
                }
            }
        }

        public class Hunting : AIState
        {
            Health target;

            Walker walker;
            Fighter fighter;

            public Hunting(Health target)
            {
                this.target = target;
            }

            public override void OnEnter(EnemyAI interactor)
            {
                walker = interactor.GetComponent<Walker>();
                fighter = interactor.GetComponent<Fighter>();
                
                //TODO: interrupt walking on exit must me necessary
                walker.SetWalkTarget(target.GetComponent<CellElement>());
                fighter.SetTarget(target);
            }

            public override void Update(EnemyAI interactor)
            {
                if (target == null) interactor.ChangeState(new Wandering());

                if (!fighter.TryToAttack())
                {
                    if(!walker.TryToGetCloser())
                    {
                        interactor.turnEntity.SpendTime(1f);
                    }
                }
            }
        }
    }
}