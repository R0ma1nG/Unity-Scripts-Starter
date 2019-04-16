using UnityEngine;

namespace Framework.Data {
    public static class GameDataManager {
        /// <summary>
        /// Create an object data with the latest model update
        /// </summary>
        /// <returns></returns>
        public static GameData Create() {
            var obj = new GameData();
            return UpdateModel(obj);
        }

        /// <summary>
        /// Perform model update operations
        /// </summary>
        /// <param name="obj">the old data</param>
        /// <returns>The data updated</returns>
        public static GameData UpdateModel(GameData obj) {
            var savedVersion = obj.SavedVersion;
            Debug.Log($"Update PlayerData, saved version: {savedVersion}, current: {GameData.CurrentVersion}");
            if (savedVersion >= GameData.CurrentVersion) return obj;

            /**
             * To add an update, use the block below and change to the current version.
            if (savedVersion < 1 && GameData.CurrentVersion >= 1) {
                // Make Updates for version 1
                Debug.Log("Update to v1");
                savedVersion = 1;
                DataAccess.Save();
            }
            **/

            obj.SavedVersion = savedVersion;
            return obj;
        }
    }
}