using Roguelike.CellMapping;
using Roguelike.Turn;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Movement
{
    public class SpriteMovement : MonoBehaviour
    {
        Dictionary<MoveAnimation, FunctionAnimation> animations = new Dictionary<MoveAnimation, FunctionAnimation>();

        public bool IsMoving { get; private set; }

        TurnEntity entity;
        CellElement CellElement;

        public enum MoveAnimation { None, Walk, Attack, }

        private struct FunctionAnimation
        {
            public float Time;
            public Func<float, float> Function;
        };

        public void AnimateTo(MoveAnimation moveAnimation, Vector2 end)
        {
            var animation = animations[moveAnimation];
            StartCoroutine(SmoothAnimation(end, animation.Time, animation.Function));
        }

        public void MoveTo(Vector2 end) => AnimateTo(MoveAnimation.Walk, end);

        private void Awake()
        {
            entity = GetComponent<TurnEntity>();
            CellElement = GetComponent<CellElement>();

            if (CellElement != null) CellElement.OnMovement += MoveTo;

            RegisterAnimations();
        }

        private void RegisterAnimations()
        {
            animations.Add(MoveAnimation.Walk, new FunctionAnimation { Time = 0.1f, Function = x => x });
            animations.Add(MoveAnimation.Attack, new FunctionAnimation { Time = 0.2f, Function = x => (-((2 * x - 1) * (2 * x - 1)) + 1) / 2 });
        }

        private IEnumerator SmoothAnimation(Vector2 end, float moveTime, Func<float, float> f)
        {
            if (entity != null) entity.ReadyForNextTurn = false;
            IsMoving = true;

            float t = 0;
            Vector2 start = transform.position;
            Vector2 delta = end - start;

            while (t < moveTime)
            {
                t = Mathf.Clamp(t + Time.deltaTime, 0, moveTime);
                float tNormalized = t / moveTime;
                transform.position = start + delta * f(tNormalized);
                yield return null;
            }

            if (entity != null) entity.ReadyForNextTurn = true;
            IsMoving = false;
        }
    }
}
