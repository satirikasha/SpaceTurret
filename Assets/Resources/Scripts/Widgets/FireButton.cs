namespace SpaceTurret.Game {
  using UnityEngine;
  using UnityEngine.EventSystems;
  using UnityEngine.UI;
  using System.Collections;

  public class FireButton: MonoBehaviour {
    
    public Turret Turret;
    public Image Image;
    public Text Text;
    [Space(10)]
    public TutorialWidget Tutorial;

    private float _CoolDownTimeLeft = Settings.CoolDown;
    private Sprite _ButtonImage;
    private Sprite _ButtonImagePressed;

    void Awake() {
      var buttons = Resources.LoadAll<Sprite>(ResourcePaths.FireButton);
      _ButtonImage = buttons[1];
      _ButtonImagePressed = buttons[0];
    }

    void Start() {
      this.GetComponent<Button>().onClick.AddListener(() => {
        if(Mathf.Approximately(_CoolDownTimeLeft, 0) && Turret.enabled) {
          _CoolDownTimeLeft = Settings.CoolDown;
          StartReloading();
          Turret.Fire();

          if(Tutorial)
            Tutorial.FireButtonHasBeenPressed();
        }
      });
    }

    void FixedUpdate() {
      if(_CoolDownTimeLeft > 0) {
        _CoolDownTimeLeft -= Time.fixedDeltaTime;
        _CoolDownTimeLeft = _CoolDownTimeLeft < 0 ? 0 : _CoolDownTimeLeft;
        Image.fillAmount = (Settings.CoolDown - _CoolDownTimeLeft) / Settings.CoolDown;
        if(Mathf.Approximately(_CoolDownTimeLeft, 0)) {
          EndReloading();
        }
      }
    }

    private void StartReloading() {
      Text.color = Color.gray;
      Image.sprite = _ButtonImagePressed;
    }

    private void EndReloading() {
      Text.color = Color.white;
      Image.sprite = _ButtonImage;
    }
  }
}
