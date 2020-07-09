using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    private Animator[] animator;
    private int numberControllers;

    void Awake()
    {
        numberControllers = transform.Find(Tags.MODEL_TAG).childCount;
        var models = transform.Find(Tags.MODEL_TAG);
        animator = new Animator[numberControllers];
        for (int i = 0; i < numberControllers; i++)
        {
            animator[i] = models.transform.GetChild(i).GetComponent<Animator>();
        }
    }

    public void Walk(bool walk)
    {
        for (int i = 0; i < numberControllers; i++)
        {
            animator[i].SetBool(AnimationsTags.WALK, walk);
        }
    }

    public void Attack1()
    {
      //  animator.SetTrigger(AnimationsTags.ATTACK1);
    }

    public void Attack2()
    {
      //  animator.SetTrigger(AnimationsTags.ATTACK2);
    }

    public void Attack3()
    {
        for (int i = 0; i < numberControllers; i++)
        {
            animator[i].SetTrigger(AnimationsTags.ATTACK3);
        }
    }

    public void Die()
    {
        for (int i = 0; i < numberControllers; i++)
        {
            animator[i].SetTrigger(AnimationsTags.DIE);
        }
    }

    public void UseAbility(AbilityEnum tag)
    {
        if (tag != AbilityEnum.NoSkill)
        {
            for (int i = 0; i < numberControllers; i++)
            {
                animator[i].SetTrigger(tag.ToString());
            }
        }
    }
}
