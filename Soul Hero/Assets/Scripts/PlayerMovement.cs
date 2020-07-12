using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum PlayerState
{
    IDLE,
    WALK,
    ATTAK
}

public class PlayerMovement : MoveControl
{

    public ParticleSystem particleClick;
    public Color particleEnemyColor;
    private Color particleMoveColor;
    private ParticleSystem.ColorOverLifetimeModule particleColor;
    private float offSetParticle = 0.1f;
    public GUI_Script GUI_Script;

    private PlayerState playerState;

    public float reachedDestinationTime = 0.8f;
    public LayerMask IgnoreMe;
    [HideInInspector]
    public Inventory inventory;

    private HealthScript healthScript;

    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        characterAnimations = GetComponent<CharacterAnimations>();
        healthScript = GetComponent<HealthScript>();
        particleClick.Stop();
        particleMoveColor = particleClick.main.startColor.color;
        particleColor = particleClick.colorOverLifetime;
        playerState = PlayerState.IDLE;
        attack_Timer = wait_Beffore_Attack_Time;
        navAgent.avoidancePriority = 0;
        canMove = true;

        inventory = new Inventory(UseItem, 20);
        inventory.AddItem(new Item(Item.ItemType.BaseSword, 1));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!GUI_Script.ClickGUI())
            {
                ClickMove();
            }
        }
        else if (playerState == PlayerState.WALK && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            if (!navAgent.hasPath || navAgent.velocity.sqrMagnitude == 0f)
            {
                navAgent.isStopped = true;
                playerState = PlayerState.IDLE;
            }
        }


        if (navAgent.velocity.magnitude > 0)
        {
            characterAnimations.Walk(true);
        }
        else
        {
            characterAnimations.Walk(false);
        }

        if(playerState == PlayerState.ATTAK && !target.GetComponent<HealthScript>().isDead && canMove)
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= attack_Distance)
            {
                AttackSingleTarget();
                return;
            }
            navAgent.destination = target.transform.position;
        }
    }

    private void ClickMove()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, ~IgnoreMe))
        {
            if (canMove)
            {
                navAgent.isStopped = false;
            }
            Quaternion rot = Quaternion.Euler(90, 0, 0);
            if (hit.transform.tag == Tags.ENEMY_TAG)
            {
                particleColor.color = particleEnemyColor;
                target = hit.transform.gameObject;
                particleClick.transform.SetParent(target.transform);
                particleClick.transform.localPosition = new Vector3(0,offSetParticle,0);
                //   particleClick.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y + offSetParticle, enemy.transform.position.z); // enemy position
                navAgent.destination = hit.point;
                playerState = PlayerState.ATTAK;
            }
            else
            {
                particleColor.color = particleMoveColor;
                particleClick.transform.SetParent(null);
                particleClick.transform.position = new Vector3(hit.point.x, hit.point.y + offSetParticle, hit.point.z);
                navAgent.destination = hit.point;
                playerState = PlayerState.WALK;
            }
            particleClick.transform.rotation = rot;
            particleClick.Play();
        }
    }

    protected override void SetState()
    {
        playerState = PlayerState.IDLE;
    }

    public void AddAgentSpeed(float speed)
    {
        navAgent.speed += speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        ItemWorld itemWorld = other.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            if (inventory.AddItem(itemWorld.GetItem()))
            {
                itemWorld.DestroySelf();
            }
        }
    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.HealthPotion:
                healthScript.HealthRegeneration(30);
                inventory.RemoveItem(new Item(Item.ItemType.HealthPotion, 1));
                break;
        }
    }
}
