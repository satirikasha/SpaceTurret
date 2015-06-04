namespace SpaceTurret.Game {
  using UnityEngine;
  using System.Collections;

  public class ExplosionEffect: MonoBehaviour {

    public ParticleSystem Trail;
    public ParticleSystem Sparks;
    public ParticleSystem Fireball;

    private void Play() {
      Fireball.Play();
      Sparks.Play();
      Trail.Play();
    }

    public void Show(Vector2 position) {
      this.transform.position = position;
      Play();
    }
  }
}
