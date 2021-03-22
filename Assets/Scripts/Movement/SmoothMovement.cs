using Roguelike.CellMapping;
using Roguelike.Turn;
using System.Collections;
using UnityEngine;

namespace Roguelike.Movement
{
    public class SmoothMovement : MonoBehaviour
    {
        float moveTime = .1f;

        TurnEntity entity;
        CellElement CellElement;

        private void Awake()
        {
            entity = GetComponent<TurnEntity>();
            CellElement = GetComponent<CellElement>();

            if (CellElement != null) CellElement.OnMovement += MoveTo;
        }

        public void MoveTo(Vector2 position) => StartCoroutine(SmoothMove(position));

        private IEnumerator SmoothMove(Vector3 position)
        {
            if (entity != null) entity.ReadyForNextTurn = false;

            float t = 0;
            Vector2 start = transform.position;

            while (t < moveTime)
            {
                t = Mathf.Clamp(t + Time.deltaTime, 0, moveTime);
                float tNormalized = t / moveTime;
                transform.position = Vector2.Lerp(start, position, tNormalized);
                yield return null;
            }

            if (entity != null) entity.ReadyForNextTurn = true;
        }

        private void OnDestroy()
        {
            if (CellElement != null) CellElement.OnMovement -= MoveTo;
        }
    }
}