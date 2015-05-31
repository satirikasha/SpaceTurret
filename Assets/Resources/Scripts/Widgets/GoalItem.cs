namespace SpaceTurret.Game {
  using UnityEngine;
  using UnityEngine.UI;
  using System.Collections;
  using Engine.Utils;

  public class GoalItem: MonoBehaviour {

    public bool Achieved { get; set; }

    public Animator Animator;
    public Image Image;

    public void ShowStarBurst() {
      if(Achieved)
        GameManager.Current.StarBurstFactory.Show(this.transform.position);
    }
  }
}
