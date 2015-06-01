namespace SpaceTurret.Menu {
  using UnityEngine;
  using UnityEngine.UI;
  using System.Collections;

  [ExecuteInEditMode]
  public class WidthResizer: MonoBehaviour {

    public float WidhtToHeightRatio;

    private float _PreviousHeight;
    private RectTransform _RectTransform;

    void Awake() {
      _RectTransform = this.transform as RectTransform;
      _PreviousHeight = _RectTransform.rect.height;
    }

    void Update() {
      if(_PreviousHeight != _RectTransform.rect.height) {
        _RectTransform.sizeDelta = new Vector2(_RectTransform.rect.height * WidhtToHeightRatio, 0);
        _PreviousHeight = _RectTransform.rect.height;
      }
    }
  }
}
