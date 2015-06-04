namespace SpaceTurret.Game {
  using System.Collections.Generic;  
  using UnityEngine;
  using Engine.Utils;


  public class ForceWaveFactory: MonoBehaviour {

    public static ForceWaveFactory Current {
      get {
        if(_Current == null)
          _Current = GameObject.FindObjectOfType<ForceWaveFactory>();
        return _Current;
      }
    }
    private static ForceWaveFactory _Current;

    private Queue<ForceWaveEffect> ForceWaveQueue;

    public void AddEffects(int capacity) {
      if(ForceWaveQueue == null)
        ForceWaveQueue = new Queue<ForceWaveEffect>();
      while(capacity > 0) {
        var instance = Instantiate(Resources.Load<ForceWaveEffect>(ResourcePaths.ForceWave));
        instance.transform.parent = this.transform;
        ForceWaveQueue.Enqueue(instance);
        capacity--;
      }
    }

    public void Show(Vector2 position) {
      var current = ForceWaveQueue.Dequeue();
      current.Show(position);
      ForceWaveQueue.Enqueue(current);
    }
  }
}