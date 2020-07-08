using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemWorldSpawner : MonoBehaviour
{
    public Item item;
    private HealthScript healthScript;

    private void Start()
    {
        healthScript = GetComponent<HealthScript>();
        healthScript.DieEvent += DieEvent;
    }

    public void SpawnItem()
    {
        ItemWorld.SpawnItemWorld(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), item);
    //    Destroy(gameObject);
    }


    private void DieEvent(object sender, EventArgs e)
    {
        SpawnItem();
    }
}
