namespace SpaceTurret.Game {
  using UnityEngine;
  using System.Collections.Generic;
  using Engine.Utils;

  public class ExplosionAffectable: MonoBehaviour {

    void Awake() {
      GameManager.Current.ExplosionAffectable.Add(this.GetComponent<Rigidbody>());
    }
  }
}