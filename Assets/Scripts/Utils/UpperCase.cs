using UnityEngine;
using UnityEngine.UI;

namespace Utils {
    [RequireComponent(typeof(Text))]
    public class UpperCase : MonoBehaviour {

        private Text text;

        void Start() {
            text = GetComponent<Text>();
        }
        
        void Update() {
            var upperText = text.text.ToUpper();
            if (text.text != upperText) text.text = upperText;
        }
    }
}