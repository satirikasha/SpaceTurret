namespace SpaceTurret.Data {
  using System;
using System.Collections.Generic;



  public partial class GraphicSettings {

    public static Dictionary<string,NamedNum[]> Data = new Dictionary<string, NamedNum[]>() {
      {
        "TextureQuality", new NamedNum[]{
          new NamedNum("low",3),
          new NamedNum("middle",2),
          new NamedNum("high",1),
          new NamedNum("best",0),
        }
      },
      {
        "MultiSampling", new NamedNum[]{
          new NamedNum("off",0),
          new NamedNum("2x",2),
          new NamedNum("4x",4),
          new NamedNum("8x",8),
        }
      },
      {
        "VSync", new NamedNum[]{
          new NamedNum("off",0),
          new NamedNum("one",1),
          new NamedNum("two",2)
        }
      },
      {
        "FrameRate", new NamedNum[]{
          new NamedNum("25",25),
          new NamedNum("30",30),
          new NamedNum("45",45),
          new NamedNum("60",60),
          new NamedNum("max",-1),
        }
      }
    };
  }
}