using UnityEngine;
using UnityEngine.UI;

namespace Localization {
    [RequireComponent(typeof(Text))]
    public class LocalizedTextAtRuntime : MonoBehaviour {
        public string key;
        private Text text;

        void Start() {
            text = GetComponent<Text>();
        }

        void Update() {
            text.text = LanguageManager.GetLocalizedValue(key);
        }
    }
}