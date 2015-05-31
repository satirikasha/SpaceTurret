namespace SpaceTurret.Game {
  using UnityEngine;
  using System.Collections;
  using Engine.Utils;

  public class StarBurstEffect: MonoBehaviour {

    public ParticleUscaledTimeUpdater StarBurst;

    private void Play() {
      StarBurst.Play();
    }

    public void Show(Vector2 position) {
      this.transform.position = position;
      Play();
    }
  }
}
