namespace SpaceTurret.Game {
  using UnityEngine;
  using UnityEngine.UI;
  using System.Collections;
  using System.Linq;
  using SpaceTurret.Data;
  using Engine.Utils;
  using CommonResources;
  
 
  public class GoalWidget: MonoBehaviour {

    public Goal[] Goals { get; private set; }
    public GoalItem[] GoalItems { get; private set; }
    public bool[] LevelGoals { get; private set; }

    void Awake() {
      Goals = Goal.Data[Application.loadedLevelName];
      GoalItems = new GoalItem[3];
      LevelGoals = PlayerData.Current.LevelGoals[Application.loadedLevelName];
      for(int i = 0; i < 3; i++) {
        GoalItems[i] = this.transform.GetChild(i).GetComponent<GoalItem>();
        if(LevelGoals[i]) {
          GoalItems[i].Achieved = true;
          GoalItems[i].Image.sprite = CommonResources.ResourcePaths.GetProgressStar(true);
        }
      }
    }

    void OnEnable() {
      StartCoroutine(CheckGoals());
    }

    public IEnumerator CheckGoals() {
      if(GameManager.Current.GameEnded) {
        if(!Goals[0].Сondition()) {
          for(int i = 0; i < 3; i++) {
            var item = GoalItems[i];
            if(!LevelGoals[i] && item.Achieved) {
              item.Achieved = false;
              item.Animator.SetTrigger(AnimatorTriggerHash.GetTrigger("Lost"));
              yield return StartCoroutine(Utils.DelayedActionUnscaled(_ => { }, time : Settings.StarBurstCheckDelay));
            }
          }
          yield break;
        }
        else {
          var goalsState = GetGoalsState();
          PlayerData.Current.LevelGoals[Application.loadedLevelName] = goalsState;
          if(GameManager.Current.NextLevelName != null && !PlayerData.Current.LevelGoals.ContainsKey(GameManager.Current.NextLevelName))
            PlayerData.Current.LevelGoals.Add(GameManager.Current.NextLevelName, new bool[3]);
          PlayerData.Save();
          for(int i = 0; i < 3; i++) {
            var item = GoalItems[i];
            if(!item.Achieved && goalsState[i]) {
              item.Achieved = true;
              item.Animator.SetTrigger(AnimatorTriggerHash.GetTrigger("Achieved"));
              yield return StartCoroutine(Utils.DelayedActionUnscaled(_ => { }, time : Settings.StarBurstCheckDelay));
            }
          }
        }
      }
      for(int i = 0; i < 3; i++) {
        var item = GoalItems[i];
        if(!item.Achieved && Goals[i].Сondition()) {
          item.Achieved = true;
          item.Animator.SetTrigger(AnimatorTriggerHash.GetTrigger("Achieved"));
          yield return StartCoroutine(Utils.DelayedActionUnscaled(_ => { }, time : Settings.StarBurstCheckDelay));
        }
      }
    }

    public bool[] GetGoalsState() {
      var result = new bool[3];
      for(int i = 0; i < 3; i++)
        result[i] = GoalItems[i].Achieved || Goals[i].Сondition();
      return result;
    }
  }
}
