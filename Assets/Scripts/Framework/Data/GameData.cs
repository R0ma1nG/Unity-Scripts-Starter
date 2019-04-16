using System;
using UnityEngine;

namespace Framework.Data {
    [Serializable]
    public class GameData {
        // CONSTANTS
        public const int CurrentVersion = 0;

        // SERIALIZED FIELDS
        [SerializeField] private int savedVersion;

        // EXPOSED FIELDS
        public int SavedVersion {
            get => savedVersion;
            set => savedVersion = value;
        }
    }
}