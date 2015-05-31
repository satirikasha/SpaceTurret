namespace SpaceTurret.Game {
  using UnityEngine;
  using System.Collections;
  using UnityEngine.UI;

  public class TurretWidget: MonoBehaviour {

    public Image Force;
    public Text Angle;
    public Transform Arrow;

    private float _ArrowAngle;
    private float _ArrowCurrentAngularVelocity;

    void FixedUpdate() {
      Arrow.rotation = Quaternion.Euler(0, 0, Mathf.SmoothDampAngle(Arrow.rotation.eulerAngles.z, _ArrowAngle, ref _ArrowCurrentAngularVelocity, Settings.ArrowDampAngleTime));
    }

    public void SetAngle(float angle) {
      _ArrowAngle = angle;
      angle = Mathf.Abs(angle);
      Angle.text = angle < 10 ? "0" + angle.ToString("F1") : angle.ToString("F1");
    }

    public void SetForce(float force) {
      Force.fillAmount = ((Settings.MaxForce - Settings.MinForce) - (Settings.MaxForce - force)) / (Settings.MaxForce - Settings.MinForce);
    }
  }
}
