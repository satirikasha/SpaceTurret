using UnityEngine;
using System.Collections;

public class ParticleUscaledTimeUpdater: MonoBehaviour {

  [HideInInspector]
  public ParticleSystem ParticleSystem;

  private float lastTime;

  private void Awake() {
    ParticleSystem = GetComponent<ParticleSystem>();
    lastTime = Time.realtimeSinceStartup;
    if(!ParticleSystem.playOnAwake)
      ParticleSystem.time = ParticleSystem.duration;
  }

  // Update is called once per frame
  void Update() {
    float deltaTime = Time.realtimeSinceStartup - lastTime;

    ParticleSystem.Simulate(deltaTime, true, false); //last must be false!!

    lastTime = Time.realtimeSinceStartup;
  }

  public void Play() {
    ParticleSystem.Simulate(0, true, true);
  }
}