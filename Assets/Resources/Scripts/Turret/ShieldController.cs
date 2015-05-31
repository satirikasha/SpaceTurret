namespace SpaceTurret.Game {
  using UnityEngine;
  using System.Collections;

  public class ShieldController: MonoBehaviour {

    public GameObject Defending;
    public Color ShieldColor;
    public Color EffectColor;
    [Space(10)]
    public Shield[] Shields;

    void Awake() {
      foreach(Shield shield in Shields) {
        shield.Color = ShieldColor;
        shield.EffectColor = EffectColor;
        shield.OnHit += OnHit;
        shield.OnParentRequest += () => Defending;
      }
    }

    public void SetObjectCommingThrough(GameObject obj) {
      foreach(Shield shield in Shields) {
        shield.ObjectCommingThrough = obj;
      }
    }

    public void OnHit() {
      foreach(Shield shield in Shields) {
        shield.DestroyIfUncharged();
      }
    }
  }
}
