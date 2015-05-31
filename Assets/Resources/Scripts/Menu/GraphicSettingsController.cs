namespace SpaceTurret.Menu {
  using SpaceTurret.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

  public class GraphicSettingsController: MonoBehaviour {

    public Slider TextureQuality;
    public Text TextureQualityValue;
    [Space(10)]
    public Slider MultiSampling;
    public Text MultiSamplingValue;
    [Space(10)]
    public Slider VSync;
    public Text VSyncValue;
    [Space(10)]
    public Slider FrameRate;
    public Text FrameRateValue;

    void Start() {
      TextureQuality.onValueChanged.AddListener(OnTextureQualityChanged);
      MultiSampling.onValueChanged.AddListener(OnMultiSamplingChanged);
      VSync.onValueChanged.AddListener(OnVSyncChanged);
      FrameRate.onValueChanged.AddListener(OnFramRateChanged);

      ImplementSettings(PlayerData.Current.GraphicSettings);
    }

    public void ResetSettings() {
      PlayerData.Current.GraphicSettings = GraphicSettings.Default;
      ImplementSettings(PlayerData.Current.GraphicSettings);
      SaveSettings();
    }

    public void SaveSettings() {
      PlayerData.Save();
    }

    private void ImplementSettings(GraphicSettings settings) {
      TextureQuality.value = settings.TextureQuality;
      MultiSampling.value = settings.MultiSampling;
      VSync.value = settings.VSync;
      FrameRate.value = settings.FrameRate;
    }

    private void OnTextureQualityChanged(float value) {
      var intvalue = (int)value;
      PlayerData.Current.GraphicSettings.TextureQuality = intvalue;
      var setting = GraphicSettings.Data["TextureQuality"][intvalue];
      QualitySettings.masterTextureLimit = setting.Num;
      TextureQualityValue.text = setting.Name;
    }

    private void OnMultiSamplingChanged(float value) {
      var intvalue = (int)value;
      PlayerData.Current.GraphicSettings.MultiSampling = intvalue;
      var setting = GraphicSettings.Data["MultiSampling"][intvalue];
      QualitySettings.antiAliasing = setting.Num;
      MultiSamplingValue.text = setting.Name;
    }

    private void OnVSyncChanged(float value) {
      var intvalue = (int)value;
      PlayerData.Current.GraphicSettings.VSync = intvalue;
      var setting = GraphicSettings.Data["VSync"][intvalue];
      QualitySettings.vSyncCount = setting.Num;
      VSyncValue.text = setting.Name;
    }

    private void OnFramRateChanged(float value) {
      var intvalue = (int)value;
      PlayerData.Current.GraphicSettings.FrameRate = intvalue;
      var setting = GraphicSettings.Data["FrameRate"][intvalue];
      Application.targetFrameRate = setting.Num;
      FrameRateValue.text = setting.Name;
    }
  }
}
