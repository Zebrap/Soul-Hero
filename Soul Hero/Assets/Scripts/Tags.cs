using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsTags
{
    public const string WALK = "Walk";
    public const string IDLE = "Idle";
    public const string ATTACK1 = "Attack1";
    public const string ATTACK2 = "Attack2";
    public const string ATTACK3 = "Attack3";
    public const string DIE = "Die";
}

public class AbilityAnimationsTags
{
    public const string BEAMSKILL = "BeamSkill";
}

public class Tags
{
    public const string PLAYER_TAG = "Player";
    public const string ENEMY_TAG = "Enemy";
    public const string MAINCAMERA_TAG = "MainCamera";
}

public class AxisTags
{
    public const string AXIS_SCROLLWHEEL = "Mouse ScrollWheel";
}

public enum AbilityEnum
{
    BeamSkill,
    NoSkill
}
