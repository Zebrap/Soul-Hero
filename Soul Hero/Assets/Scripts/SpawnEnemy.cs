using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    // private bool alreadySpawned = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.PLAYER_TAG)
        {
            foreach (Transform child in transform)
            {
                if (!child.gameObject.activeSelf)
                {
                    child.GetComponent<HealthEnemy>().reviveEnemy();
                    child.gameObject.SetActive(true);
                    child.GetComponent<EnemyControler>().ResetPosition();
                }
            }
        }
    }


}
