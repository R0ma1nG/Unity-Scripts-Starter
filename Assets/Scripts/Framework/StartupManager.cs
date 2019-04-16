using Framework.Data;
using Localization;
using UnityEngine;

namespace Framework {
    public class StartupManager : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void OnBeforeSceneLoadRuntimeMethod()
        {
            Debug.Log("===== StartupManager =====");
            // Initialize language dictionary
            LanguageManager.Init();
            // Load game data from persistent save
            DataAccess.Load();
            Debug.Log("===== End =====");
        }
    }
}