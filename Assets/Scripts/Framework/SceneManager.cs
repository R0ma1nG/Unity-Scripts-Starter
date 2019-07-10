using System.Collections.Generic;

namespace Framework
{
	/// <summary>
	/// The order of the scenes must be the same as in the build settings.
	/// </summary>
	public enum SceneEnum {
		MainMenuScene = 0,
		GlobalTestScene = 1
	}

    public static class SceneManager
	{
		private static readonly List<SceneEnum> SceneStack = new List<SceneEnum>();
		private static readonly SceneData Data = new SceneData();

		public class SceneData {
			public readonly Dictionary<string, string> dictionary = new Dictionary<string, string>();
			public const string LevelKey = "level-key";
		}
		
		/// <summary>
		/// If the scene is not the last on the stack, push the scene on the stack and load it.
		/// If the scene is the last on the stack, reload it.
		/// </summary>
		/// <param name="scene">the scene to load</param>
		public static void GoTo(SceneEnum scene) {
			if (SceneStack.Count == 0 || SceneStack[SceneStack.Count - 1] != scene) {
				SceneStack.Add(scene);
			}
			LoadScene(scene);
        }

		public static void GoTo(SceneEnum scene, KeyValuePair<string, string> tuple) {
			AddData(tuple.Key, tuple.Value);
			GoTo(scene);
		}

		/// <summary>
		/// Replace the last scene of the stack by the new one.
		/// If the stack is empty, load the scene directly.
		/// </summary>
		/// <param name="scene">the scene to load</param>
		public static void ReplaceBy(SceneEnum scene) {
			if (SceneStack.Count > 0) {
				SceneStack.RemoveAt(SceneStack.Count - 1);
			}
			SceneStack.Add(scene);
			LoadScene(scene);
		}

		public static void ReplaceBy(SceneEnum scene, KeyValuePair<string, string> tuple) {
			AddData(tuple.Key, tuple.Value);
			ReplaceBy(scene);
		}

		/// <summary>
		/// Remove the last scene of the stack and load the new last scene.
		/// </summary>
		public static void GoBack() {
			SceneEnum sceneToLoad;
			switch (SceneStack.Count) {
				case 0:
				case 1:
					sceneToLoad = SceneEnum.MainMenuScene;
					SceneStack.Clear();
					SceneStack.Add(sceneToLoad);
					break;
				default:
					SceneStack.RemoveAt(SceneStack.Count - 1);
					sceneToLoad = SceneStack[SceneStack.Count - 1];
					break;
			}
			LoadScene(sceneToLoad);
		}

		private static void LoadScene(SceneEnum scene) {
			UnityEngine.SceneManagement.SceneManager.LoadScene((int)scene);
		}

		public static string GetData(string key) {
			if (Data.dictionary.ContainsKey(key)) {
				return Data.dictionary[key];
			}

			return "";
		}

		public static void AddData(string key, string value) {
			if (Data.dictionary.ContainsKey(key)) {
				Data.dictionary.Remove(key);
			}
			Data.dictionary.Add(key, value);
		}
    }
}
