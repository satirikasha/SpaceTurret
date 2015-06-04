namespace SpaceTurret.Game {
  using System.Collections.Generic;  
  using UnityEngine;
  using Engine.Utils;


  public class ExplosionFactory: MonoBehaviour {

    public static ExplosionFactory Current {
      get {
        if(_Current == null)
          _Current = GameObject.FindObjectOfType<ExplosionFactory>();
        return _Current;
      }
    }
    private static ExplosionFactory _Current;

    private Queue<ExplosionEffect> ExplosionQueue;

    public void AddEffects(int capacity) {
      if(ExplosionQueue == null)
        ExplosionQueue = new Queue<ExplosionEffect>();
      while(capacity > 0) {
        var instance = Instantiate(Resources.Load<ExplosionEffect>(ResourcePaths.Explosion));
        instance.transform.parent = this.transform;
        ExplosionQueue.Enqueue(instance);
        capacity--;
      }
    }

    public void Show(Vector2 position) {
      var current = ExplosionQueue.Dequeue();
      current.Show(position);
      ExplosionQueue.Enqueue(current);
    }
  }
}