namespace SpaceTurret.Data {
  using System;
  using System.Collections.Generic;
  using SpaceTurret.Game;



  public partial class Goal {

    public static Dictionary<string, Goal[]> Data = new Dictionary<string, Goal[]>(){
      
      {"Level1", new Goal[]{
          new Goal("Destroy the turret",() => GameManager.Current.HasWonTheGame),
          new Goal("Complete tutorial",() => PlayerData.Current.HasCopletedTutorial),
          new Goal("Pick a star",() => GameManager.Current.Collectables[0].IsCollected)
        }
      },
      {"Level2", new Goal[]{
          new Goal("Destroy the turret",() => GameManager.Current.HasWonTheGame),
          new Goal("Pick a star",() => GameManager.Current.Collectables[0].IsCollected),
          new Goal("Pick a star",() => GameManager.Current.Collectables[1].IsCollected)
        }
      },
      {"Level3", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level4", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level5", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level6", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level7", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level8", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level9", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level10", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level11", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level12", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level13", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level14", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level15", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level16", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level17", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level18", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level19", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level20", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level21", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level22", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level23", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level24", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level25", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level26", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level27", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level28", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level29", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      },
      {"Level30", new Goal[]{
          new Goal("",() => true),
          new Goal("",() => true),
          new Goal("",() => true)
        }
      }
    };
  }
}
