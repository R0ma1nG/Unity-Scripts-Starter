using System.IO;
using Exceptions;
using UnityEngine;

namespace Utils {
    public static class StorageUtil {
        public static string ReadFileFromStreamingAssets(string relativePath) {
            string result;
            if (Application.platform == RuntimePlatform.Android) {
                var filePath = Path.Combine("jar:file://" + Application.dataPath + "!assets/", relativePath);
                var reader = new WWW(filePath);
                while (!reader.isDone) { }

                result = reader.text;
            }
            else {
                var filePath = Path.Combine(Application.streamingAssetsPath, relativePath);
                if (!File.Exists(filePath)) throw new LocalizationFileNotFound(relativePath);

                result = File.ReadAllText(filePath);
            }

            return result;
        }
    }
}