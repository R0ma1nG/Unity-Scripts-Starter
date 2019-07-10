using UnityEngine;

namespace Framework {
    /// <inheritdoc />
    /// <summary>
    /// Allow snackbar lifecycle management.
    /// Attached GameObject will not be destroyed on load.
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    public class SnackbarManager : MonoBehaviour {

        public static SnackbarManager Instance; 
        public GameObject snackbarPrefab;

        void Awake() {
            if(!Instance) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Instantiate a snackbar and display it.
        /// </summary>
        /// <param name="key">The snackbar translation key</param>
        /// <param name="duration">The duration of the snackbar. If a duration is set, then the validation button disappear.</param>
        public void Create(string key, float duration = -1f) {
            Debug.Log("Creating a snackbar");
            var obj = Instantiate(snackbarPrefab, transform);
            obj.GetComponent<Snackbar>().Show(key, duration);
        }
    }
}