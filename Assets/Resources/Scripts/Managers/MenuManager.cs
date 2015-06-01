namespace SpaceTurret.Menu {
  using UnityEngine;
  using UnityEngine.UI;
  using System.Collections;
  using Engine.Utils;
  using System.Collections.Generic;
  using System.Linq;
  using System;
  using UnityStandardAssets.Utility;

  public class MenuManager: MonoBehaviour {
     public static MenuManager Current {
      get {
        if(_Current == null) {
          _Current = GameObject.FindObjectOfType<MenuManager>();
        }
        return _Current;
      }
    }
    private static MenuManager _Current;

    public SpriteRenderer Background;
    public Transform MenuCanvas;
    public Transform CurrentMenuPanel;
    public Transform PreviousMenuPanel;
    public ToggleGroup LevelToggles;

    public event Action OnClearProgress;

    void Start() {
      Background.sprite = ResourcePaths.GetRandomBackground();
      CurrentMenuPanel.GetComponent<Animator>().SetTrigger(AnimatorTriggerHash.GetTrigger("Show"));
      FPSCounter.Instantiate();
    }

    public void ShowMenuPanel(int index){
      PreviousMenuPanel = CurrentMenuPanel;
      CurrentMenuPanel = MenuCanvas.GetChild(index);
      PreviousMenuPanel.GetComponent<Animator>().SetTrigger(AnimatorTriggerHash.GetTrigger("Hide"));
      CurrentMenuPanel.GetComponent<Animator>().SetTrigger(AnimatorTriggerHash.GetTrigger("Show"));
    }

    public void Back() {
      if(PreviousMenuPanel != null) {
        var panel = CurrentMenuPanel;
        CurrentMenuPanel = PreviousMenuPanel;
        PreviousMenuPanel = panel;
        PreviousMenuPanel.GetComponent<Animator>().SetTrigger(AnimatorTriggerHash.GetTrigger("Hide"));
        CurrentMenuPanel.GetComponent<Animator>().SetTrigger(AnimatorTriggerHash.GetTrigger("Show"));
      }
    }

    public void StartLevel() {
      var toggle = LevelToggles.ActiveToggles().FirstOrDefault();
      if(toggle != null) {
        Loading.LoadingManager.LoadHeavyLevel(toggle.gameObject.name);
      }
    }

    public void StartMultiPlayerLevel() {
      Loading.LoadingManager.LoadHeavyLevel("Multiplayer");
    }

    public void ClearProgress() {
      Data.PlayerData.Delete();
      if(OnClearProgress != null)
        OnClearProgress();
    }
  }
}
