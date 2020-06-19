using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public string levelToLoad = "Main";

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

     
     public void Menu()
     {
         SceneManager.LoadScene(levelToLoad);
     }
}
