using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSpell : MonoBehaviour
{
    public GameObject activeThis;
    private float timeToActie = 1.3f;
    private float actieTime = 4.5f;
    private float timer = 0f;

    void Update()
    {
        if (!activeThis.activeSelf)
        {
            if (timer > timeToActie)
            {
                activeThis.SetActive(true);
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        else
        {
            if (timer > actieTime)
            {
                Destroy(this.gameObject);
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
}
