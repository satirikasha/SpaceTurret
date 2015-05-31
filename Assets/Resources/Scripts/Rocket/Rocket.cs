namespace SpaceTurret.Game {
  using UnityEngine;
  using System.Collections;
  using Engine.Utils;
  using System;
  using SpaceTurret.AI;

  public class Rocket: MonoBehaviour {

    public Action<GameObject> OnHit;

    public bool Hidden {
      get { return _Hidden; }
      set {
        if(_Hidden != value) {
          if(value == true)
            Hide();
          else
            Show();
        }
      }
    }
    private bool _Hidden = true;

    public float InitialSpeed;
    public Vector2 InitialDirection;

    public Rigidbody Rigidbody;
    public SpriteRenderer SpriteRenderer;
    public ParticleSystem Afterburner;
    public Collider Collider;
    public Light Light;

    public Turret Parent { get; set; }

    void Update() {
      if(!Hidden) {
        if(Mathf.Abs(this.transform.position.x) < Settings.GameLeftRight && Mathf.Abs(this.transform.position.y) < Settings.GameTopBottom) {
          var direction = Rigidbody.velocity.normalized;
          Rigidbody.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
        }
        else {
          if(OnHit != null)
            OnHit(null);
          Hide();
        }
      }
    }

    void OnTriggerEnter(Collider collider) {
      if(!Hidden) {
        if(collider.tag == "Shield") {
          var shield = collider.GetComponent<Shield>();
          if(ReferenceEquals(shield.ObjectCommingThrough, this.gameObject))
            return;
        }
        if(collider.tag == "Collectable") {
          return;
        }
        GameManager.Current.ExplosionEffect.Show(this.transform.position);
        foreach(Rigidbody rb in GameManager.Current.ExplosionAffectable) {
          rb.AddExplosionForce(Settings.ExplosionForce, this.transform.position, Settings.ExplosionRadius, 0f, ForceMode.Impulse);
        }
        if(OnHit != null)
          OnHit(collider.gameObject);
        Hide();
      }
    }

    public void Show() {
      if(Hidden) {
        Rigidbody.velocity = InitialSpeed * InitialDirection;

        this.enabled = true;
        SpriteRenderer.enabled = true;
        Afterburner.enableEmission = true;
        Rigidbody.isKinematic = false;
        Collider.enabled = true;
        Light.enabled = true;

        _Hidden = false;
      }
    }

    public void Hide() {
      if(!Hidden) {
        this.enabled = false;
        SpriteRenderer.enabled = false;
        Afterburner.enableEmission = false;
        Rigidbody.isKinematic = true;
        Collider.enabled = false;
        Light.enabled = false;

        _Hidden = true;
      }
    }
  }
}
