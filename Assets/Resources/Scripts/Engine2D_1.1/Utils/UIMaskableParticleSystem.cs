namespace Engine.Utils {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using UnityEngine;
  using UnityEngine.UI;

  public class UIMaskableParticleSystem: MonoBehaviour {

    public Mask Mask;
    public bool ParticleSystemIsEmmiting = true;

    private ParticleSystem _ParticleSystem;

    void Start() {
      _ParticleSystem = this.GetComponent<ParticleSystem>();
    }

    void Update() {
      if(Mask.IsRaycastLocationValid(Camera.main.WorldToScreenPoint(this.transform.position), Camera.main) && ParticleSystemIsEmmiting) {
        _ParticleSystem.enableEmission = true;
      }
      else {
        _ParticleSystem.enableEmission = false;
      }
    }

    public void EnableEmmition(bool value) {
      ParticleSystemIsEmmiting = value;
    }
  }
}
