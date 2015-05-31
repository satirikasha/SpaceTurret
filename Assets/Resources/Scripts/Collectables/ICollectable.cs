namespace SpaceTurret.Game {
  using UnityEngine;


  public abstract class Collectable: MonoBehaviour {

    public bool IsCollected { get; protected set; }
  }
}
