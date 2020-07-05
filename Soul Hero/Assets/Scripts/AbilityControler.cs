using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AbilityControler : MonoBehaviour
{
    public Ability[] abilities;
    public bool[] unlockAbilities;
    protected CharacterAnimations characterAnimations;
    private PlayerMovement player;
    private ManaScript manaScript;

#pragma warning disable 0649
    [SerializeField]
    private GameObject[] skillUI;

    private Image[] SkillBackGround;
    private Image[] SkillFill;

    private float[] timerSkill;
    private float[] timeCooldown;

    private int cdNumber;

    void Awake()
    {
        timerSkill = new float[abilities.Length];
        timeCooldown = new float[abilities.Length];
        SkillBackGround = new Image[skillUI.Length];
        SkillFill = new Image[skillUI.Length];
        manaScript = GetComponent<ManaScript>();
        unlockAbilities = new bool[4] { true, false, false, false };

        cdNumber = 0;
        foreach (GameObject skills in skillUI)
        {
            SkillBackGround[cdNumber] = skills.transform.GetChild(0).GetComponent<Image>();  // Background
            SkillFill[cdNumber] = skills.transform.GetChild(1).GetComponent<Image>();        // FillAmount
            cdNumber++;
        }

        cdNumber = 0;
        characterAnimations = GetComponent<CharacterAnimations>();
        player = GetComponent<PlayerMovement>();
        foreach (Ability ability in abilities)
        {
            timerSkill[cdNumber] = 0;
            timeCooldown[cdNumber] = ability.ActiveTime();
            SkillBackGround[cdNumber].GetComponent<Image>().sprite = ability.GetComponent<Image>().sprite;
            cdNumber++;
        }

    }

    void Update()
    {
        if(player.canMove)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ActiveAbility(0);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                ActiveAbility(1);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                ActiveAbility(2);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                ActiveAbility(3);
            }
        }

        cdNumber = 0;
        foreach(Ability ability in abilities)
        {
            if (ability.gameObject.activeSelf)
            {
                timerSkill[cdNumber] += Time.deltaTime;
                SkillFill[cdNumber].fillAmount = (timeCooldown[cdNumber] - timerSkill[cdNumber]) / timeCooldown[cdNumber];
            }
            cdNumber++;
        }
    }

    private void ActiveAbility(int id)
    {
        if (abilities.Length > id)
        {
            if (!abilities[id].gameObject.activeSelf && unlockAbilities[id])  // no active and uncloked
            {
                if (manaScript.CostMana(abilities[id].manaCost))
                {
                    timerSkill[id] = 0;
                    player.NavMeshAgent_is_Stop(true);
                    abilities[id].UseAbility(transform.position, transform.forward);
                    characterAnimations.UseAbility(abilities[id].abilityEnum);
                    float timeCast = abilities[id].TimeCast();
                    StartCoroutine(CastTime(timeCast));
                }
            }
        }
        else
        {
            print("not learn yet "+id);
        }
    }

    IEnumerator CastTime(float timeCast)
    {
        yield return new WaitForSeconds(timeCast);
        player.NavMeshAgent_is_Stop(false);
    }

}
