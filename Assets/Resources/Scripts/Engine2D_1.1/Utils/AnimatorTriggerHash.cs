namespace Engine.Utils {
  using System.Collections.Generic;
  using UnityEngine;

  public static class AnimatorTriggerHash {
    public static Dictionary<string, int> Hash = new Dictionary<string, int>();

    public static int GetTrigger(string name) {
      int result;
      if(!Hash.TryGetValue(name, out result)) {
        result = Animator.StringToHash(name);
        Hash.Add(name, result);
      }
      return result;
    }
  }
}