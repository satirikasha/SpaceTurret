namespace SpaceTurret.Game {
  using UnityEngine;
  using System.Collections;
  using Engine.Utils;
  using SpaceTurret.AI;
  using SpaceTurret.Data;

  public class TutorialWidget: MonoBehaviour {

    public TurretInput TurretInput;
    public TutorialItem RotationPanel;
    public GameObject HorizontalSwipe;
    public Turret Turret;
    public TutorialItem ForcePanel;
    public GameObject VerticalSwipe;
    public TutorialItem ButtonPanel;
    public GameObject ButtonPress;
    public FireButton FireButton;
    public Shield EnemyShield;
    public PlayerAI EnemyAi;

    private bool _FireButtonPressed = false;

    public void Init() {
      StartCoroutine(Scenario());
      EnemyShield.OnHit += () => EnemyAi.enabled = true;
    }

    public IEnumerator Scenario() {
      yield return new WaitForSeconds(0.25f);
      RotationPanel.Show();
      TurretInput.BlockHorizontalInput = false;
      yield return new WaitForSeconds(1.5f);
      HorizontalSwipe.SetActive(true);
      while(Mathf.Abs(Turret.TargetAngle) < 10)
        yield return false;
      RotationPanel.Hide();
      yield return new WaitForSeconds(1f);
      TurretInput.BlockVerticalInput = false;
      ForcePanel.Show();
      yield return new WaitForSeconds(1.5f);
      VerticalSwipe.SetActive(true);
      while(Mathf.Abs(Settings.DefaultForce - Turret.TargetForce) < 0.5)
        yield return false;
      ForcePanel.Hide();
      yield return new WaitForSeconds(1f);
      ButtonPanel.Show();
      yield return new WaitForSeconds(0.5f);
      FireButton.gameObject.SetActive(true);
      yield return new WaitForSeconds(1f);
      ButtonPress.SetActive(true);
      while(!_FireButtonPressed)
        yield return false;
      ButtonPanel.Hide();
      PlayerData.Current.HasCopletedTutorial = true;
    }

    public void FireButtonHasBeenPressed() {
      _FireButtonPressed = true;
    }
  }
}