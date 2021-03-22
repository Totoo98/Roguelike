using Roguelike.Control;
using System.Collections;
using UnityEngine;

namespace Roguelike.Movement
{
    public class CameraFollow : MonoBehaviour
    {
        private const float epsilon = 0.001f;

        [SerializeField] Walker walkerTarget = null;

        [SerializeField] float minSize = 3f;
        [SerializeField] float maxSize = 1000f;
        [SerializeField] float mouseWheelSensisivity = 1f;
        [SerializeField] float pinchZoomSensisivity = 0.05f;
        [SerializeField] float characteristicTime = 10f;

        Vector3 lastTargetPosition = Vector3.zero;

        MouseControl mouseControl = null;
        Camera cam;

        private void Awake()
        {
            cam = GetComponent<Camera>();
        }

        public void Init(Walker walker)
        {
            walkerTarget = walker;
            mouseControl = walker.GetComponent<MouseControl>();
            transform.position = new Vector3(walkerTarget.transform.position.x, walkerTarget.transform.position.y, transform.position.z);
            lastTargetPosition = walker.transform.position;
        }

        public void CameraMoveToPlayer() => StartCoroutine(MoveToPlayer());

        IEnumerator MoveToPlayer()
        {
            Vector2 deltaPos = walkerTarget.transform.position - cam.transform.position;

            while (deltaPos.sqrMagnitude > epsilon)
            {
                cam.transform.position += new Vector3(deltaPos.x, deltaPos.y, 0) / characteristicTime;

                deltaPos = walkerTarget.transform.position - cam.transform.position;

                if(Input.GetMouseButtonDown(0))
                {
                    break;
                }

                yield return null;
            }
        }

        void LateUpdate()
        {
            if (walkerTarget != null)
            {
                var deltaPosTarget = walkerTarget.transform.position - lastTargetPosition;
                cam.transform.position += deltaPosTarget;
            }

            if(mouseControl.IsDragging)
            {
                cam.transform.position -= cam.ScreenToViewportPoint(mouseControl.DeltaDrag) * cam.orthographicSize;
            }

            lastTargetPosition = walkerTarget.transform.position;


            float delta = -(Input.GetAxisRaw("Mouse ScrollWheel") * cam.orthographicSize * mouseWheelSensisivity);
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize + delta, minSize, maxSize);


            if (Input.touchCount == 2)
            {
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);

                Vector2 touch0pos = touch0.position;
                Vector2 touch1pos = touch1.position;

                Vector2 touch0prevPos = touch0pos - touch0.deltaPosition;
                Vector2 touch1prevPos = touch1pos - touch1.deltaPosition;

                float mag = (touch0pos - touch1pos).magnitude;
                float prevMag = (touch0prevPos - touch1prevPos).magnitude;

                float deltaMag = mag - prevMag;

                cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - deltaMag * pinchZoomSensisivity, minSize, maxSize);
            }
        }
    }
}