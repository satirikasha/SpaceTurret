using UnityEngine;
using System.Collections;
using SpaceTurret.Game;
using Engine.Utils;

public class CollectableStar : Collectable {

  void OnTriggerEnter(Collider collider) {
    if(collider.tag == "Rocket" && !collider.GetComponent<Rocket>().Parent.IsOnTop) {
      IsCollected = true;
      this.GetComponent<Animator>().SetTrigger(AnimatorTriggerHash.GetTrigger("Collected"));
    }
  }
}
