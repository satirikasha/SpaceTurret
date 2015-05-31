namespace SpaceTurret {
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

  public static partial class Settings {
    //Common
    public const int RareUpdateCount = 1;
    
    //Rocket
    public const float ExplosionForce  = 0.25f;
    public const float BigExplosionForce  = 0.5f;
    public const float ExplosionRadius = 2f;
    public const float BigExplosionRadius = 5f;

    //Turret
    public const float CoolDown         = 5f;
    public const float DefaultForce     = 1f;
    public const float MaxForce         = 3f;
    public const float MinForce         = 0.1f;
    public const float AmplitudeForce   = 2.9f;
    public const float MaxAngle         = 50f;
    public const float AmplitudeAngle   = 100f;
    public const float DampAngleTime    = 1f;
    public const float BlowUpDelay      = 0.25f;
    public const float BlowUpDelayDelta = 0.2f;
    public const float BlowUpRadius     = 0.7f;
    public const float BlowUpIterations = 5f;
    public const float ShieldHitTime    = 1f;
    public const float ShieldReloadTime = 10f;
    public const float ForceSensitivity = 2f;
    public const float AngleSensitivity = 25f;

    //TurretWidget
    public const float ArrowDampAngleTime = 0.1f;

    //Input
    public const float MinDeltaPosition = 20f;

    //AI
    public const float AngleMutationConst = 0.0015f;
    public const float ForceMutationConst = 0.01f;

    //UI
    public const float StarBurstCheckDelay = 1f;

    //Game
    public const float GameTopBottom = 5.5f;
    public const float GameLeftRight = 5f;
    public const float GameEndDelay  = 5f;
  }
}
