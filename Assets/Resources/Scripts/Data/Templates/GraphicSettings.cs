namespace SpaceTurret.Data {
  using System;


  [Serializable]
  public partial class GraphicSettings {

    public int TextureQuality;
    public int VSync;
    public int MultiSampling;
    public int FrameRate;
    [NonSerialized]
    public static GraphicSettings Default = new GraphicSettings(3, 0, 1, 3);

    public GraphicSettings(int textureQuality, int vSync, int multiSampling, int frameRate) {
      TextureQuality = textureQuality;
      VSync = vSync;
      MultiSampling = multiSampling;
      FrameRate = frameRate;
    }

    [Serializable]
    public class NamedNum {
      public string Name;
      public int Num;

      public NamedNum(string name, int num) {
        Name = name;
        Num = num;
      }
    }
  }
}