using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{
    private int level = 0;
    private int[] expNeedForTheLevel = new int[4] { 5, 8, 12, 15 };
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

    void Awake()
    {
        expFill.fillAmount = currentExp / expNeedForTheLevel[level];
        expText.text = currentExp + " / " + expNeedForTheLevel[level] + " XP";
        levelText.text = (level + 1).ToString();
    }

    public void GetExp(int exp)
    {
        currentExp += exp;
        if (currentExp >= expNeedForTheLevel[level])
        {
            currentExp -= expNeedForTheLevel[level];
            level++;
            skillPoitns++;
            levelText.text = (level + 1).ToString();
        }
        expFill.fillAmount = ((float)currentExp / (float)expNeedForTheLevel[level]);
        expText.text = currentExp + " / " + expNeedForTheLevel[level] + " XP";
    }
}
