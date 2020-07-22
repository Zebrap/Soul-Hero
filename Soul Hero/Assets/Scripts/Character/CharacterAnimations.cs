using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    private Animator[] animator;
    private int numberControllers;
    private AnimatorStateInfo animationState;
    private AnimatorClipInfo[] myAnimatorClip;
    private float animationTime;
    private float baseTime = 1f;
    public bool showStats = false;

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

    public void SetAttackSpeed(float time)
    {
        RuntimeAnimatorController ac = animator[0].runtimeAnimatorController;
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == "Berserker_attack_03")
            {
                myAnimatorClip = animator[0].GetCurrentAnimatorClipInfo(0);
                animationTime = myAnimatorClip[0].clip.length;
                animationSpeed = animationTime / time;
                for (int j = 0; j < numberControllers; j++)
                {
                    animator[j].SetFloat("AttackSpeed", animationSpeed);
                }
                return;
            }
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
    private float animationSpeed;
    public void Attack3(float time)
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
                ResetAnimationSpeed(animator[i]);
            }
        }
    }

    private void ChangeAniamtionSpeed(float time, Animator anim )
    {
        myAnimatorClip = anim.GetCurrentAnimatorClipInfo(0);
        animationTime = myAnimatorClip[0].clip.length;
        animationSpeed = animationTime / time;
        anim.SetFloat("AttackSpeed", animationSpeed);
    }

    private void ResetAnimationSpeed(Animator anim)
    {
        anim.speed = baseTime;
    }
}
