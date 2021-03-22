using System;
using UnityEngine;

namespace Roguelike.Turn
{
    public class TurnEntity : MonoBehaviour
    {
        [SerializeField] TurnEntityList turnEntityCollection = null;

        public bool ReadyForNextTurn { get; set; } = true;

        [SerializeField] float timeOfNextTurn;
        public float TimeOfNextTurn { get => timeOfNextTurn; }
        public void SpendTime(float time) => timeOfNextTurn += time;

        public event Action<float> OnTimeSpent;
        public void ElapseTime(float timeElapsed) => OnTimeSpent?.Invoke(timeElapsed);

        IHandleTurn turnHandler;

        private void OnEnable()
        {
            turnEntityCollection.Add(this);
        }
        private void OnDisable()
        {
            turnEntityCollection.Remove(this);
        }

        protected virtual void Awake()
        {
            turnHandler = GetComponent<IHandleTurn>();
        }

        public void HandleTurn()
        {
            turnHandler.HandleTurn();
        }
    }
}
