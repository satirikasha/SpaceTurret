namespace SpaceTurret.Game {
  using UnityEngine;
  using System.Collections;

  public class ForceWaveEffect: MonoBehaviour {

    public ParticleSystem ForceWave;

    private void Play() {
      ForceWave.Play();
    }

    public void Show(Vector2 position) {
      this.transform.position = position;
      Play();
    }
  }
}
