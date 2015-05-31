namespace SpaceTurret.Menu {
  using UnityEngine;
  using System.Collections;
  using UnityEngine.UI;
  using Data;

  public class Level: MonoBehaviour {

    public Transform Stars;

    void Awake() {
      Load();
      MenuManager.Current.OnClearProgress += Load;
    }

    private void Load() {
      bool[] progress;
      if(PlayerData.Current.LevelGoals.TryGetValue(this.gameObject.name, out progress)) {
        var starRenderers = Stars.GetComponentsInChildren<Image>();
        for(int i = 0; i < 3; i++) {
          if(progress[i])
            starRenderers[i].sprite = CommonResources.ResourcePaths.GetProgressStar(true);
          else
            starRenderers[i].sprite = CommonResources.ResourcePaths.GetProgressStar(false);
        }
      }
      else
        SetLocked();
    }

    private void SetLocked() {
      Stars.gameObject.SetActive(false);
      this.GetComponent<Toggle>().interactable = false;
    }
  }
}
