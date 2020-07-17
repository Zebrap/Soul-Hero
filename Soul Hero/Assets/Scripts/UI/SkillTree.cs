using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    private AbilityControler abilities;
    public GameObject[] skill;
    private SkillManager skillManager;
    public Color unLockColor;
    public Transform uiDescription;

    private List<SkillButton> skillButtonList;

    private void Start()
    {
        skillManager = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).GetComponent<AbilityControler>().skillManager;
        abilities = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).GetComponent<AbilityControler>();

        skillButtonList = new List<SkillButton>();
        skillButtonList.Add(new SkillButton(transform.Find("Q"), skillManager, SkillManager.SkillType.Q, unLockColor));
        skillButtonList.Add(new SkillButton(transform.Find("W"), skillManager, SkillManager.SkillType.W, unLockColor));
        skillButtonList.Add(new SkillButton(transform.Find("E"), skillManager, SkillManager.SkillType.E, unLockColor));
        skillButtonList.Add(new SkillButton(transform.Find("R"), skillManager, SkillManager.SkillType.R, unLockColor));
        skillButtonList.Add(new SkillButton(transform.Find("Pasive0"), skillManager, SkillManager.SkillType.Health, unLockColor));
        skillButtonList.Add(new SkillButton(transform.Find("Pasive1"), skillManager, SkillManager.SkillType.Health2, unLockColor));
        skillButtonList.Add(new SkillButton(transform.Find("Pasive2"), skillManager, SkillManager.SkillType.Speed, unLockColor));
        int id = 0;
        foreach (Ability ability in abilities.abilities)
        {
            skill[id].transform.Find("Skill_Image").GetComponent<Image>().sprite = ability.GetComponent<Image>().sprite;
            skill[id].transform.Find("Skill_Image").GetComponent<Image>().color = ability.GetComponent<Image>().color;
            skill[id].transform.GetComponent<AbilityDescription>().description = ability.GetDescription();
            skill[id].transform.GetComponent<AbilityDescription>().SetUI(uiDescription);
            id++;
        }
    }

    private class SkillButton
    {
        private Transform transform;
        private Image unLock;
        private Color unLockColor;
        private SkillManager skillManager;
        private SkillManager.SkillType skillType;
        private Experience skillPoints;

        public SkillButton(Transform transform, SkillManager skillManager, SkillManager.SkillType skillType, Color unLockColor)
        {
            this.transform = transform;
            this.skillManager = skillManager;
            this.skillType = skillType;
            this.unLock = transform.Find("TextBackGround").GetComponent<Image>();
            this.unLockColor = unLockColor;
            this.skillPoints = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).GetComponent<Experience>();

            transform.GetComponent<Button>().onClick.AddListener(() => AddSkill(skillType));
        }

        private void AddSkill(SkillManager.SkillType type)
        {
            if(skillPoints.skillPoitns > 0)
            {
                if (!skillManager.TryUnlock(type))
                {
                    print("can't add skill");
                }
                else
                {
                    skillPoints.LoseSkillPoint();
                    UpdateVisual();
                }
            }
            else
            {
                print("you need more points");
            }
        }

        public void UpdateVisual()
        {
            unLock.color = unLockColor;
        }
    }
    void OnDisable()
    {
        uiDescription.gameObject.SetActive(false);
    }

}
