using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static bool GameIsOver;

    public GameObject gameOverUI;
    public GameObject completeLevelUI;

    void Start()
    {
        GameIsOver = false;
    }
    void Update()
    {
        if (GameIsOver)
            return;
        
        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
        
        
    }
    
    void EndGame()
    {
        GameIsOver = true;
        
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    { 
        completeLevelUI.SetActive(true);
    }

   
}
