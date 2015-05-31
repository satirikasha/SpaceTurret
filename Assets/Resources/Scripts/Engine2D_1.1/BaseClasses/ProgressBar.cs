namespace Engine.Interface {
  using UnityEngine;

  public abstract class ProgressBar: MonoBehaviour {
    protected float Progress { get; set; }

    public abstract void SetProgress(float progress);
    public abstract float GetProgress();
  }
}
