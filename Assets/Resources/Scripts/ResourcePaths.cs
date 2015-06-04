using System;
using System.Text;
using UnityEngine;

namespace SpaceTurret.CommonResources {
  public static class ResourcePaths {
    private const string _SpritesFolder = "Sprites/";
    private const string _SpritesUIFolder = _SpritesFolder + "UI/";

    public const string RatingStars = _SpritesUIFolder + "RatingStars";

    private static Sprite[] _ProgressStars;
    public static Sprite GetProgressStar(bool filled) {
      if(_ProgressStars == null)
        _ProgressStars = Resources.LoadAll<Sprite>(RatingStars);
      return filled ? _ProgressStars[0] : _ProgressStars[1];
    }
  }
}

namespace SpaceTurret.Loading {
  public static class ResourcePaths {

  }
}

namespace SpaceTurret.Menu {
  public static class ResourcePaths {
    private const string _SpritesFolder = "Sprites/";
    private const string _PrefabsFolder = "Prefabs/";

    private const string _BackgroundsFolder = _SpritesFolder + "Backgrounds";
    private const string _UIFolder = _SpritesFolder + "UI/";

    public static Sprite GetRandomBackground() {
      var mas = Resources.LoadAll<Sprite>(_BackgroundsFolder);
      return mas[UnityEngine.Random.Range(0, mas.Length)];
    }
  }
}

namespace SpaceTurret.Game {
  public static class ResourcePaths {
    private const string _PrefabsFolder = "Prefabs/";
    private const string _PrefabsEssentialsFolder = _PrefabsFolder + "Essentials/";
    private const string _SpritesFolder = "Sprites/";
    private const string _SpritesUIFolder = _SpritesFolder + "UI/";

    public const string Explosion = _PrefabsEssentialsFolder + "ExplosionEffect";
    public const string ForceWave = _PrefabsEssentialsFolder + "ForceWaveEffect";
    public const string StarBurst = _PrefabsEssentialsFolder + "StarBurstEffect";

    public const string Rocket    = _PrefabsEssentialsFolder + "Rocket";

    public const string FireButton = _SpritesUIFolder + "th-Red-button";
  }
}

