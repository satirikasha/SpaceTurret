namespace SpaceTurret.Game {
  using UnityEngine;
  using System.Collections;
  using Engine.Utils;

  [RequireComponent(typeof(CanvasGroup))]
  public class TutorialItem: MonoBehaviour {

    public float PopUpTime;

    private CanvasGroup CanvasGroup;
    private float FromAlpha;
    private float ToAlpha;

    private float CurrentLerpTime = 0;

    // Use this for initialization
    void Awake() {
      CanvasGroup = this.GetComponent<CanvasGroup>();
      FromAlpha = 0;
    }

    // Update is called once per frame
    void Update() {
      if(CurrentLerpTime < PopUpTime) {
        float t = CurrentLerpTime / PopUpTime;
        t = Mathf.Sin(t * Mathf.PI * 0.5f);
        CanvasGroup.alpha = Mathf.Lerp(FromAlpha, ToAlpha, t);
        CurrentLerpTime += Time.deltaTime;
      }
    }

    public void Show() {
      FromAlpha = 0;
      ToAlpha = 1;
      CurrentLerpTime = 0;
      this.gameObject.SetActive(true);
    }

    public void Hide() {
      FromAlpha = 1;
      ToAlpha = 0;
      CurrentLerpTime = 0;
      StartCoroutine(Utils.DelayedAction(_ => this.gameObject.SetActive(false), time : PopUpTime));
    }
  }
}
