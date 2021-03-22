using Roguelike.CellMapping;
using Roguelike.Turn;
using System.Collections;
using UnityEngine;

namespace Roguelike.Movement
{
    public class AttackMovement : MonoBehaviour
    {
        float moveTime = .2f;

        TurnEntity entity;

        private void Awake()
        {
            entity = GetComponent<TurnEntity>();
        }

        private float f(float x) => (-((2 * x - 1) * (2 * x - 1)) + 1) / 2;

        public void MoveTo(Vector2 position) => StartCoroutine(SmoothAttackMove(position));

        private IEnumerator SmoothAttackMove(Vector2 position)
        {
            if (entity != null) entity.ReadyForNextTurn = false;

            float t = 0;
            Vector2 start = transform.position;
            Vector2 delta = position - start;

            while (t < moveTime)
            {
                t = Mathf.Clamp(t + Time.deltaTime, 0, moveTime);
                float tNormalized = t / moveTime;
                transform.position = start + delta * f(tNormalized);
                yield return null;
            }

            if (entity != null) entity.ReadyForNextTurn = true;
        }
    }
}