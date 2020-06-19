using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target; 
    private int wavePointIndex = 0;

    private Enemy enemy;
    private LivesUI livesUI;
    void Start()
    {
        livesUI = GameObject.Find("HP").GetComponent<LivesUI>();

        enemy = GetComponent<Enemy>();
        target = Waypoints.points[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);
        
        enemy.speed = enemy.startSpeed;
    }

    public void GetNextWayPoint()
    {
        if (wavePointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        
        wavePointIndex++;
        target = Waypoints.points[wavePointIndex];
    }

    void EndPath()
    {
        
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        livesUI.HpStatus();
        Destroy(gameObject);
    }
}
