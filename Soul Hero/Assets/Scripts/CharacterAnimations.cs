using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    private Animator[] animator;

    void Awake()
    {
        animator = new Animator[6];
        for (int i = 0; i <= 5; i++)
        {
            animator[i] = transform.GetChild(i).GetComponent<Animator>();
        }
    }

    public void Walk(bool walk)
    {
        for (int i = 0; i <= 5; i++)
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

    void FreezeAnimation()
    {
       // animator.speed = 0f;
    }

    public void UnFreezeAnimation()
    {
     //  animator.speed = 1f;
    }
}
