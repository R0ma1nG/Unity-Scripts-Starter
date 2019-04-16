using Framework;
using UnityEngine;

namespace Utils {
    public class SceneUtil : MonoBehaviour {

        public SceneEnum scene;
        
        public void GoTo() {
            SceneManager.GoTo(scene);
        }
    }
}