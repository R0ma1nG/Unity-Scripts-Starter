using Framework;
using Framework.Data;
using Localization;
using UnityEngine;

namespace Utils {
    public class TestController : MonoBehaviour {
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
    }
}