using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{
    private int level = 0;
    private int[] expNeedForTheLevel = new int[6] { 5, 8, 12, 15, 20, 25 };
    private int neeedExp = 5;
    public int currentExp;
    public int skillPoitns = 0;

#pragma warning disable 0649
    [SerializeField]
    private Image expFill;
#pragma warning disable 0649
    [SerializeField]
    public Text expText;
#pragma warning disable 0649
    [SerializeField]
    public Text levelText;
#pragma warning disable 0649
    [SerializeField]
    public Text SkillPointText;

    public ParticleSystem levelUpParticle;

    void Awake()
    {
        levelUpParticle.Stop();
        expFill.fillAmount = currentExp / neeedExp;
        expText.text = currentExp + " / " + neeedExp + " XP";
        levelText.text = (level + 1).ToString();
        SkillPointText.text = skillPoitns.ToString() ;
    }

    public void GetExp(int exp)
    {
        currentExp += exp;
        if (currentExp >= neeedExp)
        {
            currentExp -= neeedExp;
            neeedExp += (int)(neeedExp * 0.2f);
            level++;
            levelText.text = (level + 1).ToString();
            AddSKillPoint();
            levelUpParticle.Play();
            GetExp(0);
        }
        else
        {
            expFill.fillAmount = ((float)currentExp / (float)neeedExp);
            expText.text = currentExp + " / " + neeedExp + " XP";
        }
    }

    private void AddSKillPoint()
    {
        skillPoitns++;
        SkillPointText.text = skillPoitns.ToString();
    }
    public void LoseSkillPoint()
    {
        skillPoitns--;
        SkillPointText.text = skillPoitns.ToString();
    }
}
