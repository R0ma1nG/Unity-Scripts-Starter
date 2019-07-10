using System.Collections;
using Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Framework {
    [RequireComponent(typeof(Animator))]
    public class Snackbar : MonoBehaviour {
        public Text content;
        public RectTransform textRectTransform;
        public GameObject buttonGameObject;

        private Animator animator;
        private readonly int visibilityHash = Animator.StringToHash("visible");

        void Awake() {
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Show the snackbar apparition animation and update snackbar layout and content.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="durationInSeconds"></param>
        public void Show(string key, float durationInSeconds) {
            animator.SetBool(visibilityHash, true);
            var text = LanguageManager.GetLocalizedValue(key);
            content.text = text;
            if (durationInSeconds > 0) {
                buttonGameObject.SetActive(false);
                textRectTransform.anchorMax = new Vector2(1, 1);
                StartCoroutine(DestroyAfter(durationInSeconds));
            }
        }

        /// <summary>
        /// Destroy the gameObject after the dismiss animation
        /// </summary>
        public void Dismiss() {
            animator.SetBool(visibilityHash, false);
        }

        /// <summary>
        /// Destroy immediately the gameObject
        /// </summary>
        public void Destroy() {
            Destroy(gameObject);
        }

        /// <summary>
        /// Call Dismiss() after an amount of time in seconds
        /// </summary>
        /// <param name="durationInSeconds">Wait duration in seconds</param>
        /// <returns></returns>
        private IEnumerator DestroyAfter(float durationInSeconds) {
            yield return new WaitForSeconds(durationInSeconds);
            Dismiss();
        }
    }
}