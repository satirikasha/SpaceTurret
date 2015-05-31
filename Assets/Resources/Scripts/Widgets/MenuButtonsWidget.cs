namespace SpaceTurret.Game {
  using UnityEngine;
  using UnityEngine.UI;
  using System.Collections;
  using System.Linq;

  public class MenuButtonsWidget: MonoBehaviour {

    private GameObject ContinueButton;
    private Button NextLevelButton;

    void Awake() {
      ContinueButton = this.transform.GetChild(0).gameObject;
      NextLevelButton = this.transform.GetChild(1).GetComponent<Button>();
    }

    void OnEnable() {
      StartCoroutine(ProcessButtons());
    }

    private IEnumerator ProcessButtons() {
      yield return new WaitForEndOfFrame();
      if(GameManager.Current.GameEnded) {
        ContinueButton.SetActive(false);
        NextLevelButton.gameObject.SetActive(true);
        if(GameManager.Current.NextLevelName == null || !Data.PlayerData.Current.LevelGoals.ContainsKey(GameManager.Current.NextLevelName))
          NextLevelButton.interactable = false;
      }
    }
  }
}