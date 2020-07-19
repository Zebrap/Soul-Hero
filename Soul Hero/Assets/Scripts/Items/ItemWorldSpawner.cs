using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemWorldSpawner : MonoBehaviour
{
    public Item[] item;
    private HealthScript healthScript;
    private float yPoxSpawn = 0.5f;
    public float chanceToDrop = 0.5f;

    private void Start()
    {
        healthScript = GetComponent<HealthScript>();
        healthScript.DieEvent += DieEvent;
    }

    public void SpawnItem()
    {
        if (item.Length > 0)
        {
            float chance = UnityEngine.Random.Range(0.0f, 1.0f);
            if(chance < chanceToDrop)
            {
                int r = UnityEngine.Random.Range(0, item.Length - 1);
                item[r].SetPower();
                Item dropItem = new Item(item[r].itemType, item[r].amount);
                ItemWorld.SpawnItemWorld(new Vector3(transform.position.x, transform.position.y + yPoxSpawn, transform.position.z), dropItem);
            }
        }
    }


    private void DieEvent(object sender, EventArgs e)
    {
        SpawnItem();
    }
}
