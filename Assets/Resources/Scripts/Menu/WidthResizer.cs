namespace SpaceTurret.Menu {
  using UnityEngine;
  using UnityEngine.UI;
  using System.Collections;

  [ExecuteInEditMode]
  public class WidthResizer: MonoBehaviour {

    public float WidhtToHeightRatio;

    void Update() {
      var rectTransform = this.transform as RectTransform;
      rectTransform.sizeDelta = new Vector2(rectTransform.rect.height * WidhtToHeightRatio, 0);
    }
  }
}
