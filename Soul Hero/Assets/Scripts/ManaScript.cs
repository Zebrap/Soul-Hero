﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaScript : MonoBehaviour
{
    public int manaMax = 100;
    public int mana;
    public int manaRegen = 1;

    #pragma warning disable 0649
    [SerializeField]
    private Image manaFill;
    #pragma warning disable 0649
    [SerializeField]
    private Text manaText;

    private float time =0.0f;
    public float period = 1f;

    private void Awake()
    {
        mana = manaMax;
        if (manaText != null)
        {
            manaText.text = mana + " / " + manaMax;
        }
        if (manaFill != null)
        {
            manaFill.fillAmount = mana / 100f;
        }
    }

    public bool CostMana(int manaCost)
    {
        if (mana >= manaCost)
        {
            mana -= manaCost;
            manaFill.fillAmount = mana / 100f;
            manaText.text = mana + " / " + manaMax;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Update()
    {
        if(time < period)
        {
            time += Time.deltaTime;
        }
        else
        {
            ManaRegeneration(manaRegen);
            time = 0;
        }
    }

    public void ManaRegeneration(int regeneration)
    {
        mana = Mathf.Clamp(mana + regeneration, 0, manaMax);
        manaFill.fillAmount = mana / 100f;
        manaText.text = mana + " / " + manaMax;
    }
}

