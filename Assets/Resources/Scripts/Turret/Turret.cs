#pragma warning disable
namespace SpaceTurret.Game {
  using UnityEngine;
  using UnityEngine.EventSystems;
  using System.Collections;
  using Engine.Utils;

  public class Turret: MonoBehaviour {

    public bool IsOnTop;
    public bool IsAI;

    public Transform RocketSpawner;
    public ShieldController ShieldController;
    public TurretWidget Widget;

    public float TargetAngle { get; private set; }
    public float TargetForce { get; private set; }

    private float _CurrentAngularVelocity = 0f;

    private Animator _Animator;
    private SphereCollider _Collider;

    private Vector2 _CurrentSwipeStartPos;

    void Start() {
      _Animator = this.GetComponent<Animator>();
      _Collider = this.GetComponent<SphereCollider>();
      TargetAngle = 0;
      TargetForce = Settings.DefaultForce;
      if(!IsAI) {
        Widget.SetForce(TargetForce);
        Widget.SetAngle(TargetAngle);
      }
      if(IsOnTop) {
        GameManager.Current.Player2_Turrets.Add(this);
      }
      else {
        GameManager.Current.Player1_Turrets.Add(this);
      }
    }

    void FixedUpdate() {
      this.transform.rotation = !IsOnTop ?
        Quaternion.Euler(0, 0, Mathf.SmoothDampAngle(this.transform.rotation.eulerAngles.z, TargetAngle, ref _CurrentAngularVelocity, Settings.DampAngleTime)) :
        Quaternion.Euler(0, 0, Mathf.SmoothDampAngle(this.transform.rotation.eulerAngles.z, TargetAngle + 180, ref _CurrentAngularVelocity, Settings.DampAngleTime));
    }

    void OnTriggerEnter(Collider collider) {
      if(ShieldController != null)
        ShieldController.OnHit();
      StartCoroutine(BlowUp());
    }

    void OnDisable() {
      var ai = this.GetComponent<SpaceTurret.AI.PlayerAI>();
      if(ai != null) {
        ai.enabled = false;
      }
    }

    private IEnumerator BlowUp() {
      var delay = Random.Range(Settings.BlowUpDelay - Settings.BlowUpDelayDelta, Settings.BlowUpDelay + Settings.BlowUpDelayDelta);
      _Animator.SetTrigger(AnimatorTriggerHash.GetTrigger("Break"));
      GameManager.Current.OnTurretDown(this);
      for(int i = 0; i < Settings.BlowUpIterations; i++) {
        yield return new WaitForSeconds(delay);
        GameManager.Current.ExplosionFactory.Show(this.transform.position.ToVector2() + Settings.BlowUpRadius * Random.insideUnitCircle);
        delay = Random.Range(Settings.BlowUpDelay - Settings.BlowUpDelayDelta, Settings.BlowUpDelay + Settings.BlowUpDelayDelta);
      }
    }

    public Rocket FireRocket() {
      _Animator.SetTrigger(AnimatorTriggerHash.GetTrigger("Fire"));
      var rocket = GameManager.Current.RocketFactory.ShowRocket(RocketSpawner.position, this.transform.rotation.eulerAngles.z.ToDirection(), TargetForce, this);
      if(ShieldController != null)
        ShieldController.SetObjectCommingThrough(rocket.gameObject);
      return rocket;
    }

    public void Fire() {
      if(this.enabled) {
        _Animator.SetTrigger(AnimatorTriggerHash.GetTrigger("Fire"));
        var rocket = GameManager.Current.RocketFactory.ShowRocket(RocketSpawner.position, this.transform.rotation.eulerAngles.z.ToDirection(), TargetForce, this);
        if(ShieldController != null)
          ShieldController.SetObjectCommingThrough(rocket.gameObject);
      }
    }

    public void SetAngle(float angle) {
      TargetAngle = angle;
      if(!IsAI) {
        Widget.SetAngle(TargetAngle);
      }
    }

    public void SetForce(float force) {
      TargetForce = force;
      if(!IsAI) {
        Widget.SetForce(TargetForce);
      }
    }
  }
}
