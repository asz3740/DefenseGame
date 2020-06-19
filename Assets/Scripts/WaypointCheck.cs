using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointCheck : MonoBehaviour
{
    private EnemyMovement enemyMovement;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyMovement = other.GetComponent<EnemyMovement>();
            enemyMovement.GetNextWayPoint();
        }
    }
}
