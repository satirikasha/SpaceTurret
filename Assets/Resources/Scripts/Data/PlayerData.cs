namespace SpaceTurret.Data {
  using UnityEngine;
  using System;
  using System.Runtime.Serialization.Formatters.Binary;
  using System.IO;
  using System.Threading;
  using System.Collections.Generic;
  using System.Collections;

  [Serializable]
  public class PlayerData {

    public static PlayerData Current {
      get {
        if(_Current == null)
          Load();
        return _Current;
      }
    }
    [NonSerialized]
    private static PlayerData _Current;

    #region Fields
    public bool                       HasCopletedTutorial;
    public int                        Account;
    public Dictionary<string, bool[]> LevelGoals;
    public GraphicSettings            GraphicSettings;
    #endregion

    /// <summary>
    /// Знфчения по умолчанию
    /// </summary>
    private PlayerData() {
      HasCopletedTutorial = false;
      Account = 100;
      LevelGoals = new Dictionary<string, bool[]>() { { "Level1", new bool[3] { false, false, false } } };
      GraphicSettings = GraphicSettings.Default;
    }

    /// <summary>
    /// Автоматически не сохранияется, необходимо вызывать
    /// </summary>
    public static void Save() {
      var bf = new BinaryFormatter();
      using(var fs = File.Create(Application.persistentDataPath + "/PlayerData.dat")) {
        bf.Serialize(fs, _Current);
      }
    }

    private static void Load() {
      if(File.Exists(Application.persistentDataPath + "/PlayerData.dat")) {
        var bf = new BinaryFormatter();
        using(var fs = File.Open(Application.persistentDataPath + "/PlayerData.dat", FileMode.Open)) {
          _Current = (PlayerData)bf.Deserialize(fs);
        }
      }
      else {
        _Current = new PlayerData();
      }
    }

    public static void Delete() {
      File.Delete(Application.persistentDataPath + "/PlayerData.dat");
      _Current = new PlayerData();
    }
  }
}
