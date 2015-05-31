namespace SpaceTurret.Game {
  using UnityEngine;
  using System.Collections.Generic;
  using Engine.Utils;

  public class Gravity: MonoBehaviour {

    private Rigidbody _Rigidbody;

    void Awake() {
      _Rigidbody = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
      foreach(Rigidbody rb in GameManager.Current.GravityAffectable) {
        if(!rb.isKinematic) {
          Vector3 offset = transform.position - rb.transform.position;
          rb.AddForce(offset / Mathf.Pow(offset.sqrMagnitude, 1.5f) * _Rigidbody.mass);
        }
      }
    }
  }
}