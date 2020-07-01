using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Abilitys : MonoBehaviour
{
    public ParticleSystem particleEffect;
    private bool dealDamage;
    public int spellDamage = 30;
    private Vector3 offSetPosition;
    private List<Collider> collidersList;

    public float timeStartCollider = 1.7f;
    public float timeEndCollider = 0.5f;
    public float timeDeactive = 1f;

    private float timer;
    private Vector3 pos;

    public float fowardValue = 1f;

    void Awake()
    {
        gameObject.SetActive(false);
        dealDamage = false;
        offSetPosition = transform.position;
        collidersList = new List<Collider>();
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            timer += Time.deltaTime / timeStartCollider;
            transform.position = Vector3.Lerp(pos, offSetPosition + pos, timer);
        }
    }

    public void UseAbility(Vector3 position, Vector3 forward)
    {
        if (!gameObject.activeSelf)
        {
            timer = 0;
            pos = position + forward* fowardValue;
            transform.position = pos;
            //  transform.position = offSetPosition + position;
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
                target.GetComponent<HealthScript>().ApplyDamage(spellDamage);
            }
        }
    }

}
