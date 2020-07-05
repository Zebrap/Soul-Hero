using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    private AbilityControler abilities;
    public GameObject skill0;
    public GameObject skill1;
    public GameObject skill2;
    public GameObject skill3;


    private void Start()
    {
        abilities = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).GetComponent<AbilityControler>();
        skill0.transform.Find("Skill_Image").GetComponent<Image>().sprite = abilities.abilities[0].GetComponent<Image>().sprite;
    }

    public void AddSkillPoint()
    {
        abilities.unlockAbilities[1] = true;
    }
}
