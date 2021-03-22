using UnityEngine;

namespace Roguelike.Control
{
    public class MouseControl : MonoBehaviour
    {
        //[SerializeField] float clickTime = .1f;
        [SerializeField] float clickMagnitudeLimit = 20f;

        private Vector2 lastFramePosition = Vector2.zero;
        private Vector2 deltaDragSinceOrigin = Vector2.zero;
        private int lastFrameTouchCount = 0;

        public Vector2 DeltaDragSinceOrigin => IsDragging ? deltaDragSinceOrigin : Vector2.zero;
        public Vector2 DeltaDrag { get; private set; } = Vector2.zero;
        public bool IsDragging { get; private set; } = false;

        public bool HasClicked()
        {
            if (Input.GetMouseButtonUp(0))
            {
                return (!IsDragging);
            }

            return false;
        }

        private void Update()
        {
            var currentPos = Input.mousePosition;
            DeltaDrag = Vector2.zero;

            if (Input.GetMouseButtonDown(0))
            {
                lastFramePosition = currentPos;
                deltaDragSinceOrigin = Vector2.zero;
                IsDragging = false;
            }

            if(Input.GetMouseButton(0))
            {
                if (Input.touchCount == lastFrameTouchCount)
                {
                    deltaDragSinceOrigin += (Vector2)currentPos - lastFramePosition;
                    DeltaDrag = (Vector2)currentPos - lastFramePosition;
                }
                if(!IsDragging)
                {
                    if(deltaDragSinceOrigin.sqrMagnitude >= clickMagnitudeLimit * clickMagnitudeLimit)
                    {
                        deltaDragSinceOrigin = Vector2.zero;
                        IsDragging = true;
                    }
                }
            }

            lastFramePosition = currentPos;
            lastFrameTouchCount = Input.touchCount;
        }
    }
}
