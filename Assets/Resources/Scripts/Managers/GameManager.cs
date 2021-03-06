﻿#pragma warning disable
namespace SpaceTurret.Game {
  using Engine.Utils;
  using System;
  using System.Collections.Generic;
  using UnityEngine;
  using UnityEngine.UI;


  class GameManager : MonoBehaviour {
    public static GameManager Current {
      get {
        if(_Current == null) {
          _Current = GameObject.FindObjectOfType<GameManager>();
        }
        return _Current;
      }
    }
    private static GameManager _Current;

    public RocketFactory RocketFactory { get; private set; }
    public StarBurstFactory StarBurstFactory { get; private set; }
    public ExplosionFactory ExplosionFactory { get; private set; }
    public ForceWaveFactory ForceWaveFactory { get; private set; }

    public string NextLevelName { get; private set; }

    public List<Turret> Player1_Turrets { get; set; }
    public List<Turret> Player2_Turrets { get; set; }
    
    public List<Rigidbody> GravityAffectable;
    public List<Rigidbody> ExplosionAffectable;

    public Collectable[] Collectables;
    [Space(10)]
    public Image PausePanel;
    public GameObject PauseMenu;
    public GameObject StartMenu;
    [Space(10)]
    public bool IsMultiPlayerGame;

    #region Flags
    public bool HasWonTheGame { get; private set; }
    public bool GameEnded { get; private set; }
    #endregion

    private bool _Paused;

    void Awake() {
      ShowStartMenu();
      PrewarmTriggers();
      PresetSelf();
      PresetFactories();
    }

    private void PrewarmTriggers() {
      AnimatorTriggerHash.GetTrigger("Fire");
      AnimatorTriggerHash.GetTrigger("Break");
    }

    private void PresetSelf() {
      Player1_Turrets = new List<Turret>();
      Player2_Turrets = new List<Turret>();
      GravityAffectable = new List<Rigidbody>();
      int nextLevelNum;
      if(int.TryParse(Application.loadedLevelName.Replace("Level", ""), out nextLevelNum)) {
        var nextLevelName = "Level" + (nextLevelNum + 1);
        if(Application.CanStreamedLevelBeLoaded(nextLevelName))
          NextLevelName = nextLevelName;
      }
    }

    private void PresetFactories() {
      RocketFactory = new GameObject("RocketFactory", typeof(RocketFactory)).GetComponent<RocketFactory>();
      RocketFactory.AddRockets(10);
      StarBurstFactory = new GameObject("StarBurstFactory", typeof(StarBurstFactory)).GetComponent<StarBurstFactory>();
      StarBurstFactory.AddEffects(3);
      ExplosionFactory = new GameObject("ExplosionFactory", typeof(ExplosionFactory)).GetComponent<ExplosionFactory>();
      ExplosionFactory.AddEffects(10);
      ForceWaveFactory = new GameObject("ForceWaveFactory", typeof(ForceWaveFactory)).GetComponent<ForceWaveFactory>();
      ForceWaveFactory.AddEffects(3);
    }

    public void OnTurretDown(Turret turret) {
      turret.enabled = false;
      if(Player1_Turrets.Remove(turret) && Player1_Turrets.Count == 0) {
        DisableTurrets();
        StartCoroutine(Utils.DelayedAction(_ => ShowGameEndMenu(false), time : Settings.GameEndDelay));
        HasWonTheGame = false;
      }
      if(Player2_Turrets.Remove(turret) && Player2_Turrets.Count == 0) {
        DisableTurrets();
        StartCoroutine(Utils.DelayedAction(_ => ShowGameEndMenu(true), time : Settings.GameEndDelay));
        HasWonTheGame = true;
      }
    }

    public void DisableTurrets() {
      foreach(Turret t in Player1_Turrets) {
        t.enabled = false;
      }
      foreach(Turret t in Player2_Turrets) {
        t.enabled = false;
      }
    }

    public void Pause() {
      _Paused = true;
      Time.timeScale = 0;
      PausePanel.enabled = true;
    }

    public void Resume() {
      _Paused = false;
      Time.timeScale = 1;
      PausePanel.enabled = false;
    }

    public void ShowPauseMenu() {
      if(!_Paused) {
        Pause();
        PauseMenu.gameObject.SetActive(true);
      }
      else {
        StartCoroutine(Utils.DelayedAction(_ => {
          Resume();
          PauseMenu.gameObject.SetActive(false);
        }));
      }
    }

    public void ShowStartMenu() {
      if(!_Paused) {
        Pause();
        StartMenu.SetActive(true);
      }
      else {
        Resume();
        StartMenu.SetActive(false);
      }
    }

    public void ShowGameEndMenu(bool hasWon) {
      GameEnded = true;
      var text = PauseMenu.transform.GetChild(0).GetComponent<Text>();
      if(!IsMultiPlayerGame) {
        text.text = hasWon ? "You won!" : "You lost!";
        text.color = hasWon ? Color.green : Color.red;
      }
      else {
        text.text = hasWon ? "Player 1 won!" : "Player 2 won!";
        text.color = Color.green;
      }
      ShowPauseMenu();
    }

    public void RestartLevel() {
      Loading.LoadingManager.LoadHeavyLevel(Application.loadedLevelName);
    }

    public void GoToMainMenu() {
      Loading.LoadingManager.LoadHeavyLevel("MainMenu");
    }

    public void GoToNextLevel() {
      Loading.LoadingManager.LoadHeavyLevel(NextLevelName);
    }
  }
}
