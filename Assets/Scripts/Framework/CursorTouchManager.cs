using UnityEngine;

namespace Framework {
    [RequireComponent(typeof(SpriteRenderer))]
    public class CursorTouchManager : MonoBehaviour {
        public float speed = 0.1f;
        public bool enableDebugWithMouse = false;

        private Vector3 delta = Vector3.zero;
        private Vector3 lastPos = Vector3.zero;

        void Awake() {
            if (Application.isEditor) {
                GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        void Update() {
            // TOUCH HANDLING
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
                var touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                transform.Translate(
                    -touchDeltaPosition.x * speed * Time.deltaTime,
                    -touchDeltaPosition.y * speed * Time.deltaTime,
                    0
                );
            }

            // MOUSE HANDLING
            if (enableDebugWithMouse && Application.isEditor) {
                if (Input.GetMouseButtonDown(0)) {
                    lastPos = Input.mousePosition;
                }
                else if (Input.GetMouseButton(0)) {
                    delta = Input.mousePosition - lastPos;
                    transform.Translate(
                        -delta.x * speed * Time.deltaTime,
                        -delta.y * speed * Time.deltaTime,
                        0
                    );
                    lastPos = Input.mousePosition;
                }
            }
        }
    }
}