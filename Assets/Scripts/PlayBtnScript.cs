using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayBtnScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Shadow_Island_S"); 
    }
    
     public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit(); // Works only in build
    }
}
