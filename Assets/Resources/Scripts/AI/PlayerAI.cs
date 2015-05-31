namespace SpaceTurret.AI {
  using System.Linq;
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using SpaceTurret.Game;
  using System;
  using Engine.Utils;

  public class PlayerAI: MonoBehaviour {

    public int PopulationCount; // Перенести в Settings
    public float MinDistaceSuccess; // Перенести тоже
    [Space(10)]
    public float MinEstimatedForce;
    public float MaxEstimatedForce;
    [Space(10)]
    public List<Transform> Targets;
    [Space(10)]
    public bool EnableLookup;
    public Specimen[] Lookup;


    private Specimen _PlannedSpecimen;
    private Specimen _EtalonSpecimen;
    private List<Specimen> _ExistingPopulation;
    private Queue<Specimen> _DeadPopulation;

    private Turret _Turret;

    private float _MinDistanceSuccessDeg2;

    private float CDTimeLeft = Settings.CoolDown;

    void Start() {
      PresetSelf();
    }

    void Update() {
      EngagePopulation();
      SetPopulation();
      TrackPopulation();
    }

    private void PresetSelf() {
      _Turret = this.GetComponent<Turret>();
      _ExistingPopulation = new List<Specimen>();
      _DeadPopulation = new Queue<Specimen>();
      _MinDistanceSuccessDeg2 = MinDistaceSuccess.deg2();
    }

    private void SetPopulation() {
      if((CDTimeLeft - Time.fixedDeltaTime - 2.5f * Settings.DampAngleTime) < 0 && _PlannedSpecimen == null) {
          _PlannedSpecimen = GetNextPlanned();
          SetSpecimen(_PlannedSpecimen);
        }
    }

    private void EngagePopulation() {
      if(CDTimeLeft <= 0) {
        CDTimeLeft = Settings.CoolDown;

        if(_PlannedSpecimen != null) {
          _PlannedSpecimen.Rocket = _Turret.FireRocket();
          _ExistingPopulation.Add(_PlannedSpecimen);
          _PlannedSpecimen = null;
        }
      }
      else {
        CDTimeLeft -= Time.deltaTime;
      }
    }

    private void TrackPopulation() {
      foreach(Specimen s in _ExistingPopulation) {
        float minDistance = float.MaxValue;
        foreach(Transform t in Targets) {
          var distance = (t.position - s.Rocket.transform.position).sqrMagnitude;
          if(distance < minDistance)
            minDistance = distance;
        }

        if(minDistance < s.Result) {
          s.Result = minDistance;
          s.Mark = 1 / s.Result;
          if(!s.Success && s.Result < _MinDistanceSuccessDeg2)
            s.Success = true;
        }
      }
    }

    private Specimen GetNextPlanned() {
      if(_DeadPopulation.Count + _ExistingPopulation.Count > 0) {
        float markSumm = _DeadPopulation.Sum(_ => _.Mark) + _ExistingPopulation.Sum(_ => _.Success ? _.Mark : 0);

        Specimen parent1 = null;
        Specimen parent2 = null;
        Specimen result = new Specimen() { Angle = 0f, Force = 0f, Parent = this };

        var probability1 = UnityEngine.Random.Range(0f, 1f);
        var probability2 = UnityEngine.Random.Range(0f, 1f);
        var probSum = 0f;
        foreach(Specimen s in _DeadPopulation) {
          s.SurvivalCoeff = s.Mark / markSumm;
          probSum += s.SurvivalCoeff;
          if(probSum >= probability1 && parent1 == null)
            parent1 = s;
          if(probSum >= probability2 && parent2 == null)
            parent2 = s;
        }
        foreach(Specimen s in _ExistingPopulation) {
          s.SurvivalCoeff = s.Mark / markSumm;
          probSum += s.SurvivalCoeff;
          if(probSum >= probability1 && parent1 == null)
            parent1 = s;
          if(probSum >= probability2 && parent2 == null)
            parent2 = s;
        }

        if(parent1 == null || parent2 == null)
          return GetRandomSpecimen();

        var mutationCoeff = (parent1.Result + parent2.Result) / (2 * _MinDistanceSuccessDeg2);

        GetChild(parent1, parent2, ref result);

        Mutate(ref result, mutationCoeff);

        return result;
      }
      else return GetRandomSpecimen();
    }

    private void GetChild(Specimen parent1, Specimen parent2, ref Specimen child) {
      child.Force = parent1.Force / 2 + parent2.Force / 2;
      child.Angle = parent1.Angle / 2 + parent2.Angle / 2;
      if(!ReferenceEquals(parent1, parent2)) {
        child.AngleMutationSign = GetAngleSign(parent1, parent2);
        child.ForceMutationSign = GetForceSign(parent1, parent2);
      }
    }

    private void Mutate(ref Specimen obj, float coeff) {

      var angleMutation = UnityEngine.Random.Range(coeff / 2, coeff + Settings.AngleMutationConst) * Settings.AmplitudeAngle;
      var forceMutation = UnityEngine.Random.Range(coeff / 2 , coeff + Settings.ForceMutationConst) * Settings.AmplitudeForce;

      if(obj.AngleMutationSign.IsSign()) {
        angleMutation = angleMutation * obj.AngleMutationSign;
      }
      else {
        var sign = Utils.GetRandomSign();
        angleMutation = angleMutation * sign;
        obj.AngleMutationSign = sign;
      }

      if(obj.ForceMutationSign.IsSign()) {
        forceMutation = forceMutation * obj.ForceMutationSign;
      }
      else {
        var sign = Utils.GetRandomSign();
        forceMutation = forceMutation * sign;
        obj.ForceMutationSign = sign;
      }

      obj.Angle += angleMutation;
      obj.Force += forceMutation;
      
      if(Mathf.Abs(obj.Angle) > Settings.MaxAngle)
        obj.Angle = Mathf.Sign(obj.Angle) * Settings.MaxAngle;
      if(obj.Force > MaxEstimatedForce)
        obj.Force = MaxEstimatedForce;
      if(obj.Force < MinEstimatedForce)
        obj.Force = MinEstimatedForce;
      if(obj.Force > Settings.MaxForce)
        obj.Force = Settings.MaxForce;
      if(obj.Force < Settings.MinForce)
        obj.Force = Settings.MinForce;
    }

    private Specimen GetRandomSpecimen() {
      if(EnableLookup) {
        if(_EtalonSpecimen == null)
         _EtalonSpecimen = Lookup[UnityEngine.Random.Range(0, Lookup.Length)];
        var result = new Specimen() { Angle = _EtalonSpecimen.Angle, Force = _EtalonSpecimen.Force, Parent = this };
        Mutate(ref result, 0.05f);
        return result;
      }
      else {
        return new Specimen() {
          Angle = UnityEngine.Random.Range(-Settings.MaxAngle, Settings.MaxAngle),
          Force = UnityEngine.Random.Range(MinEstimatedForce, MaxEstimatedForce),
          Parent = this
        };
      }
    }

    private void SetSpecimen(Specimen obj) {
      _Turret.SetForce(obj.Force);
      _Turret.SetAngle(obj.Angle);
    }

    private float GetAngleSign(Specimen obj1, Specimen obj2) {
      if(obj1.Mark > obj2.Mark) {
        return Mathf.Sign(obj1.Angle - obj2.Angle);
      }
      if(obj2.Mark > obj1.Mark) {
        return Mathf.Sign(obj2.Angle - obj1.Angle);
      }
      return 0f;
    }

    private float GetForceSign(Specimen obj1, Specimen obj2) {
      if(obj1.Mark > obj2.Mark) {
        return Mathf.Sign(obj1.Force - obj2.Force);
      }
      if(obj2.Mark > obj1.Mark) {
        return Mathf.Sign(obj2.Force - obj1.Force);
      }
      return 0f;
    }

    public void RegisterDeath(Specimen obj) {
      _ExistingPopulation.Remove(obj);

      if(_DeadPopulation.Count >= PopulationCount)
        _DeadPopulation.Dequeue();

      if(obj.Success)
        _DeadPopulation.Enqueue(obj);
    }

    [Serializable]
    public class Specimen {
      public float Angle; //Угол
      public float AngleMutationSign; // В какую сторону мутировал угол относительно предков
      public float Force; //Сила
      public float ForceMutationSign; // В какую сторону мутировала сила относительно предков
      [HideInInspector]
      public float Result = float.MaxValue; //Близость к цели
      [HideInInspector]
      public float Mark = 0; // = 1/Result
      [HideInInspector]
      public float SurvivalCoeff = 0; //Коеффициент выживаемости, строится в контектсе других особей в выборке
      [HideInInspector]
      public bool Success = false; //Полет был успешным (не попал в себя и долетел достаточно далеко)
      
      [HideInInspector]
      public PlayerAI Parent;

      public Rocket Rocket {
        get {
          return _Rocket;
        }
        set {
          if(value != null) {
            _Rocket = value;
            _Rocket.OnHit = OnDeath;
          }
        }
      }
      private Rocket _Rocket = null;

      private void OnDeath(GameObject source) {
        if(source != null) {
          var sourceType = (CollisionSource)Enum.Parse(typeof(CollisionSource), source.tag);
          if(sourceType == CollisionSource.Turret) {
            if(Parent.Targets.Contains(source.transform))
              Result = 0.000001f;
            else
              Success = false;
          }
          if(sourceType == CollisionSource.Shield) {
            if(Parent.Targets.Contains(source.GetComponent<Shield>().GetParent().transform))
              Result /= 5f;
            else
              Success = false;
          }
        }
        Mark = 1 / Result;
        _Rocket.OnHit = null;
        _Rocket = null;
        Parent.RegisterDeath(this);
      }
    }
  }
}

