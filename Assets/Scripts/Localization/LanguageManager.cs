using System;
using System.Collections.Generic;
using System.IO;
using Exceptions;
using Framework;
using UnityEngine;

namespace Localization {
    /// <summary>
    /// Class used to store the language dictionary
    /// </summary>
    public static class LanguageManager {
        private static Dictionary<string, string> localizedText;
        private const string MissingTextString = "Text not found";

        /// <summary>
        /// Load the language stored in the Preferences and update language dictionary
        /// </summary>
        private static void Reload() {
            Init();
        }

        /// <summary>
        /// Load the current language to the dictionary
        /// </summary>
        public static void Init() {
            var language = Preferences.GetLanguage();
            Debug.Log("Using language: " + language);
            try {
                LoadLocalizedText(language);
            }
            catch (LocalizationFileNotFound e) {
                Debug.LogError(e);
            }
        }

        /// <summary>
        /// Load a language to the dictionary
        /// </summary>
        /// <param name="language">the language to load</param>
        /// <exception cref="LocalizationFileNotFound"></exception>
        private static void LoadLocalizedText(Language language) {
            localizedText?.Clear();
            localizedText = new Dictionary<string, string>();

            var fileName = FormatLanguageFilePath(language);
            var filePath = Path.Combine(Application.streamingAssetsPath, fileName);
            if (!File.Exists(filePath)) throw new LocalizationFileNotFound(fileName);

            var dataAsJson = File.ReadAllText(filePath);
            var loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            foreach (var item in loadedData.items) {
                localizedText.Add(item.key, item.value);
            }

            Debug.Log("Language loaded");
        }

        /// <summary>
        /// Get the string value translated to the current language corresponding to the key
        /// </summary>
        /// <param name="key">key associated to the desired value</param>
        /// <returns>The translated value associated to the param key</returns>
        public static string GetLocalizedValue(string key) {
            var result = MissingTextString;
            if (localizedText.ContainsKey(key)) {
                result = localizedText[key];
            }

            return result;
        }

        /// <summary>
        /// Change the language to the next in the list, or the first one if the end is reached.
        /// </summary>
        public static void ChangeLanguage() {
            var languagesCount = Enum.GetValues(typeof(Language)).Length;
            var currentLanguage = Preferences.GetLanguage();
            var language = ((int) currentLanguage + 1) % languagesCount;
            Preferences.SetLanguage((Language) language);
            Reload();
        }

        /// <summary>
        /// Create the file name for the language in input
        /// </summary>
        /// <param name="language"></param>
        /// <returns>the language filename</returns>
        private static string FormatLanguageFilePath(Language language) {
            var languageName = Enum.GetName(typeof(Language), language);
            var fileName = Constants.LanguageFileBaseName + "_" + languageName + Constants.LanguageFileExtension;
            return Constants.LanguageFolder + "/" + fileName;
        }
    }
}