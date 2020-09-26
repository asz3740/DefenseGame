using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{

    public Wave[] waves;
    
    public Transform spawnPoint;

    public float timeBetweenWaves = 7f;
    private float countdown = 3f;
    public GameManager gameManager;

    private int waveNumber = 0;

    public Text waveNumberText;
    public Text countDownText;
    public static int EnemiesAlive;

    void Update ()
    {
      
        if (waveNumber == waves.Length && EnemiesAlive <=0)
        {    
            gameManager.WinLevel();
            enabled = false;
        }
        if (countdown <=  0 && waveNumber != waves.Length)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        if ( PlayerStats.Rounds == 0)
        {
            countDownText.text = countdown.ToString("F0");
        }
        else
        {
            countDownText.enabled = false;
        }





    }

    IEnumerator SpawnWave()
    {
        EnemiesAlive = 0;
        PlayerStats.Rounds++;
        waveNumberText.text = PlayerStats.Rounds.ToString() + "ROUND";
        Wave wave = waves[waveNumber];

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveNumber++;

    }

    void SpawnEnemy (GameObject enemy)
    {
        EnemiesAlive++;
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);     
    }
}
