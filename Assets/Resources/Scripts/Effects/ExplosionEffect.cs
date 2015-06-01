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
      //Debug.DrawLine(position + new Vector2(1, 1), position - new Vector2(1, 1), Color.red, 10f);
      //Debug.DrawLine(position + new Vector2(1, -1), position - new Vector2(1, -1), Color.red, 10f);
      this.transform.position = position;
      Play();
    }
  }
}
