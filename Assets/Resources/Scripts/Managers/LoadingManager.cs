namespace SpaceTurret.Loading {
  using UnityEngine;
  using System.Collections;
  using UnityEngine.UI;
  using Data;
  using Engine.Interface;

  public class LoadingManager: MonoBehaviour {

    public ProgressBar ProgressBar;

    private AsyncOperation _LevelLoader;
    private static string _LevelToLoad;

    IEnumerator Start() {
      Time.timeScale = 1;
      _LevelLoader = Application.LoadLevelAsync(_LevelToLoad);
      yield return _LevelLoader;
    }

    void Update() {
      if(ProgressBar != null) {
        ProgressBar.SetProgress(_LevelLoader.progress);
      }
    }

    public static void LoadHeavyLevel(string levelName) {
      _LevelToLoad = levelName;
      Time.timeScale = 0;
      Application.LoadLevelAsync("Loading");
    }

    public static void LoadLightLevel(string levelName) {
      Application.LoadLevelAsync(levelName);
    }
  }
}
