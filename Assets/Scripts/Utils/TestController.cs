using Framework;
using Framework.Data;
using Framework.Events;
using Localization;
using UnityEngine;
using UnityEngine.Events;

namespace Utils {
    public class TestController : MonoBehaviour {
        private UnityAction<GameEvent> action;

        void Awake() {
            action = OnReceiveTestEvent;
        }
        
        private void OnEnable() {
            EventManager.StartListening<TestEvent>(action);
        }

        public void ResetPreferences() {
            Preferences.Reset();
        }

        public void LoadGameData() {
            var data = DataAccess.Load();
            Debug.Log(JsonUtility.ToJson(data));
        }

        public void SaveGameData() {
            DataAccess.Save();
        }

        public void ChangeLanguage() {
            LanguageManager.ChangeLanguage();
        }

        public void SnackbarNoButton() {
            SnackbarManager.Instance.Create("game_title", 2f);
        }

        public void Snackbar() {
            SnackbarManager.Instance.Create("game_title");
        }

        public void SendTestEvent() {
            EventManager.TriggerEvent(new TestEvent());
        }

        private void OnReceiveTestEvent(GameEvent e) {
            if (e is TestEvent evnt) {
                Debug.Log(evnt);
                SnackbarManager.Instance.Create("event_test", 1.5f);
            }
        }
    }
}