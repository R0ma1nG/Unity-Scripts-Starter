using Framework;
using Framework.Data;
using Localization;
using UnityEngine;

namespace Utils {
    public class TestController : MonoBehaviour {
        public void ResetPreferences() {
            Preferences.Reset();
            //Assert.True(Preferences.GetLanguage().Equals(Constants.DefaultLanguage));
            //Assert.True(Preferences.GetSoundStatus() == Preferences.SoundStatus.On);
        }

        public void LoadGameData() {
            var data = DataAccess.Load();
            Debug.Log(JsonUtility.ToJson(data));
            //Assert.NotNull(data);
        }

        public void SaveGameData() {
            DataAccess.Save();
            //Assert.DoesNotThrow(DataAccess.Save);
        }

        public void ChangeLanguage() {
            LanguageManager.ChangeLanguage();
        }
    }
}