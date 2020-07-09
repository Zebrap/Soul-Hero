using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Ability : MonoBehaviour, IAbility
{
    private ParticleSystem particleEffect;
    protected bool dealDamage;
    public int spellDamage = 30;
    protected Vector3 offSetPosition;
    protected List<Collider> collidersList;

    public float timeStartCollider = 1.7f;
    public float timeEndCollider = 0.5f;
    public float timeDeactive = 1f;

    protected float timer;
    protected Vector3 pos;
    public float fowardValue = 1f;
    public AbilityEnum abilityEnum;

    public int manaCost = 20;

    void Awake()
    {
        dealDamage = false;
        offSetPosition = transform.position;
        collidersList = new List<Collider>();
        particleEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            MoveAblity();
        }
    }

    public void UseAbility(GameObject parent)
    {
        if (!gameObject.activeSelf)
        {
            timer = 0;
            StartPosition(parent);
            gameObject.SetActive(true);
            particleEffect.Play();
            StartCoroutine(CollectColider());
        }
    }

    IEnumerator CollectColider()
    {
        yield return new WaitForSeconds(timeStartCollider);
        dealDamage = true;
        yield return new WaitForSeconds(timeEndCollider);
        DealDamage();
        EffectOnTarget();
        yield return new WaitForSeconds(timeDeactive);
        gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider target)
    {
        if (dealDamage && target.tag == Tags.ENEMY_TAG)
        {
            if (!collidersList.Contains(target))
            {
                collidersList.Add(target);
            }
        }
    }

    private void DealDamage()
    {
        dealDamage = false;
        foreach (Collider target in collidersList)
        {
            if (target.gameObject.activeSelf)
            {
                ActionOnTarget(target);
            }
        }
        collidersList.Clear();
    }

    protected virtual void ActionOnTarget(Collider target) { }

    public float TimeCast()
    {
        return timeStartCollider + timeEndCollider;
    }

    public float ActiveTime()
    {
        return timeStartCollider + timeEndCollider + timeDeactive;
    }

    protected abstract void MoveAblity();
    protected abstract void StartPosition(GameObject parent);
    protected abstract void EffectOnTarget();


}
