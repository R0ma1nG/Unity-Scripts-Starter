using UnityEngine;
using Localization;

namespace Framework {
    public static class Preferences {
        private const string LanguageKey = "language";
        private const string SoundStatusKey = "language";

        public enum SoundStatus {
            On = 0,
            Off = 1
        }

        public static void SetSoundStatus(SoundStatus status) {
            PlayerPrefs.SetInt(SoundStatusKey, (int) status);
        }

        public static SoundStatus GetSoundStatus() {
            return (SoundStatus) PlayerPrefs.GetInt(SoundStatusKey, 1);
        }

        public static void SetLanguage(Language language) {
            PlayerPrefs.SetInt(LanguageKey, (int) language);
        }

        public static Language GetLanguage() {
            return (Language) PlayerPrefs.GetInt(LanguageKey, (int) Constants.DefaultLanguage);
        }

        public static void Reset() {
            PlayerPrefs.DeleteAll();
            SetSoundStatus(SoundStatus.On);
            SetLanguage(Constants.DefaultLanguage);
        }
    }
}