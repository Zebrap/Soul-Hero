using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public PlayerMovement player;

    public void FreezPlayerMove()
    {
        player.NavMeshAgent_is_Stop(true);
        player.canMove = false;
    }

    public void UnFreezPlayerMove()
    {
        player.NavMeshAgent_is_Stop(false);
        player.canMove = true;
    }
}
