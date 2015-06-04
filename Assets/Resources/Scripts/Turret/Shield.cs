namespace SpaceTurret.Game {
  using UnityEngine;
  using System.Collections;
  using System;

  public class Shield: MonoBehaviour {

    public event Action OnHit;
    public event Func<GameObject> OnParentRequest;

    public Color Color;
    public Color EffectColor;
    public GameObject effect;
    public Vector3 FixInctanceAngle;

    public GameObject ObjectCommingThrough { get; set; }

    private Renderer _Renderer;
    private Collider _Collider;

    private ShieldMode _Mode;

    private float _DestroyingTimeLeft;
    private float _RestoringTimeLeft;

    void Start() {
      _Renderer = this.GetComponent<Renderer>();
      _Renderer.sortingLayerName = "Turret";
      _Renderer.sortingOrder = 10;
      _Renderer.material.color = Color;
      var effectRenderer = this.transform.GetChild(0).GetComponent<Renderer>();
      effectRenderer.material.color = EffectColor;
      effectRenderer.sortingLayerName = "Turret";
      effectRenderer.sortingOrder = 10;
      _Collider = this.GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider collider) {
      if(!ReferenceEquals(collider.gameObject, ObjectCommingThrough)) {
        var hitPoint = collider.transform.position;
        var direction = hitPoint - this.transform.position;
        direction.Normalize();
        direction.z = 0;
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Sign(-direction.x) * Vector3.Angle(Vector2.up, direction)));
        effect.SetActive(true);
        effect.transform.LookAt(hitPoint);
        effect.transform.Rotate(FixInctanceAngle);
        foreach(Rigidbody rb in GameManager.Current.ExplosionAffectable) {
          rb.AddForce(Settings.BigExplosionForce * new Vector3(0, Settings.BigExplosionRadius / (rb.transform.position.y - this.transform.position.y), 0), ForceMode.Impulse);
        }
        GameManager.Current.ForceWaveFactory.Show(this.transform.position);
        _Collider.enabled = false;
        OnHit();
        SetMode(ShieldMode.Destroying);
      }
    }

    void OnTriggerExit(Collider collider) {
      ObjectCommingThrough = null;
    }

    void FixedUpdate() {
      if(_Mode == ShieldMode.Destroying) {
        _DestroyingTimeLeft -= Time.fixedDeltaTime;
        var lerp = Mathf.Lerp(1, 0, _DestroyingTimeLeft);
        _Renderer.material.SetFloat("_Cutoff", lerp);
        _Renderer.material.SetFloat("_Transparency", 1 - lerp);
        if(_DestroyingTimeLeft <= 0)
          SetMode(ShieldMode.Restoring);
      }
      if(_Mode == ShieldMode.Restoring) {
        _RestoringTimeLeft -= Time.fixedDeltaTime;
        var lerp = Mathf.Lerp(0, 1, _RestoringTimeLeft / Settings.ShieldReloadTime);
        _Renderer.material.SetFloat("_Cutoff", lerp);
        _Renderer.material.SetFloat("_Transparency", 1 - lerp);
        if(_RestoringTimeLeft <= 0)
          SetMode(ShieldMode.Normal);
      }
    }

    public void DestroyIfUncharged() {
      if(_Mode == ShieldMode.Destroying || _Mode == ShieldMode.Restoring) {
        SetMode(ShieldMode.Destroyed);
      }
    }

    public GameObject GetParent() {
      return OnParentRequest();
    }

    private void SetMode(ShieldMode mode) {
      switch(mode) {
        case ShieldMode.Destroying: { _Mode = mode; _DestroyingTimeLeft = Settings.ShieldHitTime; } break;
        case ShieldMode.Restoring:  { _Mode = mode; _RestoringTimeLeft = Settings.ShieldReloadTime; } break;
        case ShieldMode.Destroyed:  { _Mode = mode; this.gameObject.SetActive(false); } break;
        case ShieldMode.Normal:     { _Mode = mode; _Collider.enabled = true; } break;
      }
    }
  }
}
