namespace SpaceTurret.Game {
  using UnityEngine;
  using UnityEngine.EventSystems;
  using System.Collections;
  using Engine.Utils;

  public class TurretInput: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public Turret Turret;

    public Vector2 StartPosition { get; private set; }

    public bool BlockHorizontalInput = false;
    public bool BlockVerticalInput = false;

    public void OnBeginDrag(PointerEventData eventData) {
      StartPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData) {
      var overallDeltaPos = eventData.position - StartPosition;
      if(overallDeltaPos.sqrMagnitude > (Screen.width / Settings.MinDeltaPosition).deg2()) {
        if(Mathf.Abs(overallDeltaPos.x) >= Mathf.Abs(overallDeltaPos.y)) {
          if(!BlockHorizontalInput) {
            float predictedAngle;
            if(!Turret.IsOnTop) {
              predictedAngle = Turret.TargetAngle - Settings.AngleSensitivity * eventData.delta.x / Screen.width;
              Turret.SetAngle(Mathf.Abs(predictedAngle) < Settings.MaxAngle ? predictedAngle : Settings.MaxAngle * Mathf.Sign(predictedAngle));
            }
            else {
              predictedAngle = Turret.TargetAngle + Settings.AngleSensitivity * eventData.delta.x / Screen.width;
              Turret.SetAngle(Mathf.Abs(predictedAngle) < Settings.MaxAngle ? predictedAngle : Settings.MaxAngle * Mathf.Sign(predictedAngle));
            }
          }
        }
        else {
          if(!BlockVerticalInput){
            float predictedForce;
            if(!Turret.IsOnTop)
              predictedForce = Turret.TargetForce + Settings.ForceSensitivity * eventData.delta.y / Screen.height;
            else
              predictedForce = Turret.TargetForce - Settings.ForceSensitivity * eventData.delta.y / Screen.height;
            if(predictedForce > Settings.MinForce) {
              if(predictedForce < Settings.MaxForce) {
                Turret.SetForce(predictedForce);
              }
              else {
                Turret.SetForce(Settings.MaxForce);
              }
            }
            else {
              Turret.SetForce(Settings.MinForce);
            }
          }
        }
      }
    }

    public void OnEndDrag(PointerEventData eventData) {
      StartPosition = Vector2.zero;
    }
  }
}
