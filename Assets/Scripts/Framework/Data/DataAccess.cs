using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Framework.Data {
    public static class DataAccess {
        private const string FileName = "data.dat";
        private const string Folder = "/";

        private static GameData gameData;

        /// <summary>
        /// Load the game data from
        /// - persistent file if first loading
        /// - memory if already loaded
        /// </summary>
        /// <returns>The player data (save)</returns>
        public static GameData Load() {
            if (gameData == null) {
                var filePath = Application.persistentDataPath + Folder + FileName;
                if (File.Exists(filePath)) {
                    try {
                        var data = ReadFromBinaryFile(filePath);
                        gameData = GameDataManager.UpdateModel(data);
                        Debug.Log("Game data loaded from local storage");
                    }
                    catch (Exception e) {
                        Debug.LogError(e);
                    }
                }
                else {
                    gameData = GameDataManager.Create();
                    Debug.Log("Game data created and stored in local storage with success");
                    Save();
                }
            }

            return gameData;
        }

        private static GameData ReadFromBinaryFile(string filePath) {
            var binaryFormatter = new BinaryFormatter();
            var file = File.Open(
                filePath,
                FileMode.Open,
                FileAccess.Read
            );
            var data = (GameData) binaryFormatter.Deserialize(file);
            file.Close();
            return data;
        }

        /// <summary>
        /// Save the current game data to persistent file.
        /// </summary>
        public static void Save() {
            try {
                SaveToBinaryFile();
                Debug.Log("Game data saved to local storage");
            }
            catch (Exception e) {
                Debug.LogWarning("Failed to serialize object to a file (Reason: " + e.Message + ")");
            }
        }

        private static void SaveToBinaryFile() {
            var binaryFormatter = new BinaryFormatter();
            var file = File.Open(
                Application.persistentDataPath + Folder + FileName,
                FileMode.Create,
                FileAccess.Write
            );
            binaryFormatter.Serialize(file, gameData);
            file.Close();
        }
    }
}