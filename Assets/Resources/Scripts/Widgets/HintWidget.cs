namespace SpaceTurret.Game {
  using UnityEngine;
  using UnityEngine.UI;
  using UnityEngine.EventSystems;
  using System.Collections;

  public class HintWidget: MonoBehaviour, IPointerDownHandler {

    public GameObject Buttons;
    public GoalWidget GoalWidget;
    public Text HintText;
    public ToggleGroup Toggles;

    private Toggle _CurrentToggle;

    public void ShowHint(string text) {
      if(!this.gameObject.activeSelf) {
        Buttons.SetActive(false);
        this.gameObject.SetActive(true);
      }
      HintText.text = text;
    }

    public void HideHint() {
      if(!Buttons.activeSelf) {
        this.gameObject.SetActive(false);
        Buttons.SetActive(true);
      }
    }

    public void ProcessStarToggle(Toggle toggle) {
      if(Toggles.AnyTogglesOn()) {
        ShowHint(GoalWidget.Goals[int.Parse(toggle.name[toggle.name.Length - 1].ToString()) - 1].Description);
      }
      else {
        HideHint();
      }
    }

    public void OnPointerDown(PointerEventData eventData) {
      Toggles.SetAllTogglesOff();
      HideHint();
    }
  }
}
