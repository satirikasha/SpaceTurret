namespace SpaceTurret.Data {
  using System;



  public partial class Goal {
    public string Description;
    public Func<bool> Сondition;

    public Goal(string description, Func<bool> condition) {
      Description = description;
      Сondition = condition;
    }
  }
}
