using TMPro;
using UnityEngine;

namespace Localization {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedTextMeshPro : MonoBehaviour {
        public string key;
        private TextMeshProUGUI text;

        void Start() {
            text = GetComponent<TextMeshProUGUI>();
            text.text = LanguageManager.GetLocalizedValue(key);
        }
    }
}