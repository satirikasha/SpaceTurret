namespace SpaceTurret.Game {
  using System.Collections.Generic;  
  using UnityEngine;
  using Engine.Utils;


  public class StarBurstFactory: MonoBehaviour {

    public static StarBurstFactory Current {
      get {
        if(_Current == null)
          _Current = GameObject.FindObjectOfType<StarBurstFactory>();
        return _Current;
      }
    }
    private static StarBurstFactory _Current;

    private Queue<StarBurstEffect> StarBurstQueue;

    public void AddEffects(int capacity) {
      if(StarBurstQueue == null)
        StarBurstQueue = new Queue<StarBurstEffect>();
      while(capacity > 0) {
        var instance = Instantiate(Resources.Load<StarBurstEffect>(ResourcePaths.StarBurst));
        instance.transform.parent = this.transform;
        StarBurstQueue.Enqueue(instance);
        capacity--;
      }
    }

    public void Show(Vector2 position) {
      var current = StarBurstQueue.Dequeue();
      current.Show(position);
      StarBurstQueue.Enqueue(current);
    }
  }
}