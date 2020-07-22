using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public MoveControl player;

    public void Awake()
    {
        if (player == null)
        {
            player = transform.parent.gameObject.transform.parent.gameObject.GetComponent<MoveControl>(); // First parent Model, model parent
        }
    }

    public void FreezPlayerMove()
    {
    //    player.NavMeshAgent_is_Stop(true);
   //     player.canMove = false;
    }

    public void UnFreezPlayerMove()
    {
   //     player.NavMeshAgent_is_Stop(false);
   //     player.canMove = true;
    }

    public void EndAttackAnimation()
    {
        player.dealDamage();
    }
}
