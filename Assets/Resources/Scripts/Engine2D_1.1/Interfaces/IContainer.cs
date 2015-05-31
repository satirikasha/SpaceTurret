namespace Engine.Interface {
  using UnityEngine;

  public interface IContainer {
    Vector2 Center { get; }
    bool Contains(Vector2 point);
  }
}
