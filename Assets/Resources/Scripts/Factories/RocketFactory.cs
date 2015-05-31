namespace SpaceTurret.Game {
  using System.Collections.Generic;  
  using UnityEngine;
  using Engine.Utils;


  public class RocketFactory: MonoBehaviour {

    public static RocketFactory Current {
      get {
        if(_Current == null)
          _Current = GameObject.FindObjectOfType<RocketFactory>();
        return _Current;
      }
    }
    private static RocketFactory _Current;

    private Queue<Rocket> RocketQueue;

    public void AddRockets(int capacity) {
      if(RocketQueue == null)
        RocketQueue = new Queue<Rocket>();
      while(capacity > 0) {
        var instance = Instantiate(Resources.Load(ResourcePaths.Rocket)) as GameObject;
        instance.transform.parent = this.transform;
        GameManager.Current.GravityAffectable.Add(instance.GetComponent<Rigidbody>());
        RocketQueue.Enqueue(instance.GetComponent<Rocket>());
        capacity--;
      }
    }

    public Rocket ShowRocket(Vector2 position, Vector2 direction, float initialSpeed, Turret parent) {
      var current = RocketQueue.Dequeue();
      current.transform.position = position.ToVector3();
      StartCoroutine(Utils.DelayedAction(_ => { current.InitialDirection = direction; current.InitialSpeed = initialSpeed; current.Parent = parent; current.Show(); }, null, 0f));
      RocketQueue.Enqueue(current);
      return current;
    }
  }
}