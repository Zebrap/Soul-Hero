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
    public const string CAST = "Cast";
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
    public const string MODEL_TAG = "Model";
    public const string CANVAS_TAG = "MainCanvas";
}

public class UiTags
{
    public const string BACKGROUND = "BackGround";
    public const string TEXT = "Text";
    public const string ITEM_SLOT_CONTAINER = "ItemSlotContainer";
    public const string ITEM_SLOT = "ItemSlot";

}

public class AxisTags
{
    public const string AXIS_SCROLLWHEEL = "Mouse ScrollWheel";
}

public enum AbilityEnum
{
    BeamSkill,
    HealSkill,
    NoSkill
}
