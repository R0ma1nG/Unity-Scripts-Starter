using UnityEngine;

namespace Utils {
    public class OnlyDebug : MonoBehaviour {
        public void Start() {
            gameObject.SetActive(Debug.isDebugBuild);
        }
    }
}