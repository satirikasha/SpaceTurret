namespace SpaceTurret.Menu {
  using UnityEngine;
  using System.Collections;

  public class RocketRotation: MonoBehaviour {

    void FixedUpdate() {
      this.transform.Rotate(Vector3.forward, 2f);
    }
  }
}
