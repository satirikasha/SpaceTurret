using System;
using UnityEngine;

namespace UnityStandardAssets.Utility {
  [RequireComponent(typeof(GUIText))]
  public class FPSCounter: MonoBehaviour {
    private static FPSCounter _Current;

    const float fpsMeasurePeriod = 0.5f;
    private int m_FpsAccumulator = 0;
    private float m_FpsNextPeriod = 0;
    private int m_CurrentFps;
    const string display = "{0} FPS";
    private GUIText m_GuiText;

    public static void Instantiate() {
      if(_Current == null && Debug.isDebugBuild) {
        var obj = new GameObject("FPSCounter", typeof(GUIText), typeof(FPSCounter)).GetComponent<FPSCounter>();
        _Current = obj;
        obj.m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
        obj.m_GuiText = obj.GetComponent<GUIText>();
        obj.m_GuiText.anchor = TextAnchor.UpperRight;
        obj.transform.position = new Vector3(1f, 1f, 0f);
        DontDestroyOnLoad(obj.gameObject);
      }
    }

    private void Update() {
      // measure average frames per second
      m_FpsAccumulator++;
      if(Time.realtimeSinceStartup > m_FpsNextPeriod) {
        m_CurrentFps = (int)(m_FpsAccumulator / fpsMeasurePeriod);
        m_FpsAccumulator = 0;
        m_FpsNextPeriod += fpsMeasurePeriod;
        m_GuiText.text = string.Format(display, m_CurrentFps);
      }
    }
  }
}
