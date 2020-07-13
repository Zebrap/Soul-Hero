using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    public event EventHandler<OnSkillUnlockedEventArgs> onSkillUnlocked;
    public class OnSkillUnlockedEventArgs : EventArgs
    {
        public SkillType skillType;
    }


    public enum SkillType
    {
        None,
        Q,
        W,
        E,
        R,
        Health,
        Health2,
        Speed
    }

    private List<SkillType> unlockedSKillTypeList;

    public SkillManager()
    {
        unlockedSKillTypeList = new List<SkillType>();
    }

    private void UnlockSKill(SkillType sKillType)
    {
        if (!IsSKillUnlocked(sKillType))
        {
            unlockedSKillTypeList.Add(sKillType);
            onSkillUnlocked?.Invoke(this, new OnSkillUnlockedEventArgs { skillType = sKillType });
        }
    }

    public bool IsSKillUnlocked(SkillType skillType)
    {
        return unlockedSKillTypeList.Contains(skillType);
    }

    public List<SkillType> GetSkillRequirment(SkillType skillType)
    {
        List<SkillType> list = new List<SkillType>();
        switch (skillType)
        {
            case SkillType.E: list.Add(SkillType.Q);
                break;
            case SkillType.R: list.Add(SkillType.Q);
                break;
            case SkillType.Health: list.Add(SkillType.W);
                break;
            case SkillType.Health2:
                list.Add(SkillType.R);
                list.Add(SkillType.Health);
                break;
            case SkillType.Speed:
                list.Add(SkillType.R);
                break;
            default:
                list.Add(SkillType.None);
                break;
        }
        return list;
    }

    public bool TryUnlock(SkillType skillType)
    {
        if (!IsSKillUnlocked(skillType))
        {
            List<SkillType> listskillRequirement = GetSkillRequirment(skillType);
            foreach (SkillType skillRequirement in listskillRequirement)
            {
                if (skillRequirement != SkillType.None)
                {
                    if (!IsSKillUnlocked(skillRequirement))
                    {
                        return false;
                    }
                }
                else
                {
                    UnlockSKill(skillType);
                    return true;
                }
            }
            UnlockSKill(skillType);
            return true;
        }
        else
        {
            return false;
        }
       
    }
}
