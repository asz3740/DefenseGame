using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    public float speed = 5f;
    
    public float health = 100;
    
    // 적 처치 시 획득 골드
    public int worth = 50;

    public Text healthText;
    
    void Start()
    {
        speed = startSpeed;
    }
    public void TakeDamage(float amount)
    {
        health -= amount;

        healthText.text = health.ToString("F0");
        if (health <= 0)
        {
            Die();
        }
    }

    public void Slow (float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    void Die()
    {
        PlayerStats.Money += worth;
        WaveSpawner.EnemiesAlive--;
        print("파괴" + WaveSpawner.EnemiesAlive);
        Destroy(gameObject);
      
      
    }
   
}
