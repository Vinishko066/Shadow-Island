using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [Header("UI References")]
    public Button restartButton;
    public Button mainMenuButton;
    public Button quitButton;
    
    [Header("Scene Names")]
    public string gameSceneName = "SampleScene";
    public string mainMenuSceneName = "Main menu";
    
    void Start()
    {
        // Set up button listeners
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);
            
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(GoToMainMenu);
            
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
            
        // Add hover animations to buttons
        AddHoverAnimation(restartButton);
        AddHoverAnimation(mainMenuButton);
        AddHoverAnimation(quitButton);
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f; // Reset time scale in case it was paused
        SceneManager.LoadScene(gameSceneName);
    }
    
    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Reset time scale in case it was paused
        SceneManager.LoadScene(mainMenuSceneName);
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    
    private void AddHoverAnimation(Button button)
    {
        if (button == null) return;
        
        // Check if ButtonHoverAnimation already exists
        ButtonHoverAnimation hoverAnim = button.GetComponent<ButtonHoverAnimation>();
        if (hoverAnim == null)
        {
            hoverAnim = button.gameObject.AddComponent<ButtonHoverAnimation>();
        }
        
        // Set pixel-style horror-themed hover colors
        ColorBlock colors = button.colors;
        colors.normalColor = new Color(0.15f, 0.05f, 0.05f, 0.95f);
        colors.highlightedColor = new Color(0.25f, 0.08f, 0.08f, 1f);
        colors.pressedColor = new Color(0.4f, 0.12f, 0.12f, 1f);
        colors.selectedColor = new Color(0.2f, 0.06f, 0.06f, 0.98f);
        button.colors = colors;
    }
    
    void Update()
    {
        // Allow ESC key to go to main menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToMainMenu();
        }
        
        // Allow R key to restart
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
}