using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverBtnScripts : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReplayGame()
    {
        Time.timeScale = 1f; // reset time if paused
        SceneManager.LoadScene("Shadow_Island_S");
    }

    // Load Main Menu scene
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main menu"); 
    }
}
