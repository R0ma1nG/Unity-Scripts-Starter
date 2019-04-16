using UnityEngine;
using UnityEngine.UI;

namespace Localization {
    [RequireComponent(typeof(Text))]
    public class LocalizedText : MonoBehaviour {
        public string key;

        void Start() {
            var text = GetComponent<Text>();
            text.text = LanguageManager.GetLocalizedValue(key);
        }
    }
}