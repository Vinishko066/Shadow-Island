using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameOverController : MonoBehaviour
{
    [Header("Button References")]
    public Button playButton;
    public Button mainMenuButton;
    
    [Header("Scene Settings")]
    public string gameSceneName = "SampleScene";
    public string mainMenuSceneName = "Main menu";
    
    [Header("Animation Settings")]
    public float buttonClickDelay = 0.2f;
    
    void Start()
    {
        // Ensure buttons are assigned and add listeners
        if (playButton != null)
        {
            playButton.onClick.RemoveAllListeners();
            playButton.onClick.AddListener(() => StartCoroutine(OnPlayButtonClick()));
            
            // Enhance hover animation settings
            EnhanceHoverAnimation(playButton);
        }
        
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.RemoveAllListeners();
            mainMenuButton.onClick.AddListener(() => StartCoroutine(OnMainMenuButtonClick()));
            
            // Enhance hover animation settings
            EnhanceHoverAnimation(mainMenuButton);
        }
        
        // Add pulse animation to buttons on start
        StartCoroutine(InitialButtonAnimation());
    }
    
    void EnhanceHoverAnimation(Button button)
    {
        // The ButtonHoverAnimation component is already configured
        // Just ensure it exists
        ButtonHoverAnimation hoverAnim = button.GetComponent<ButtonHoverAnimation>();
        if (hoverAnim == null)
        {
            // Add the component if it doesn't exist
            hoverAnim = button.gameObject.AddComponent<ButtonHoverAnimation>();
        }
        
        // Set button transition for visual feedback
        button.transition = Button.Transition.ColorTint;
        ColorBlock colors = button.colors;
        colors.normalColor = new Color(0.83f, 0.33f, 0.35f, 1f);
        colors.highlightedColor = new Color(1f, 0.45f, 0.45f, 1f);
        colors.pressedColor = new Color(0.7f, 0.2f, 0.2f, 1f);
        colors.selectedColor = new Color(0.9f, 0.4f, 0.4f, 1f);
        colors.fadeDuration = 0.1f;
        button.colors = colors;
        
        // Set the target graphic
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            button.targetGraphic = buttonImage;
        }
    }
    
    IEnumerator OnPlayButtonClick()
    {
        // Play click animation
        PlayClickAnimation(playButton);
        
        // Wait for animation
        yield return new WaitForSeconds(buttonClickDelay);
        
        // Reset time scale in case it was paused
        Time.timeScale = 1f;
        
        // Load the game scene
        SceneManager.LoadScene(gameSceneName);
    }
    
    IEnumerator OnMainMenuButtonClick()
    {
        // Play click animation
        PlayClickAnimation(mainMenuButton);
        
        // Wait for animation
        yield return new WaitForSeconds(buttonClickDelay);
        
        // Reset time scale in case it was paused
        Time.timeScale = 1f;
        
        // Load the main menu scene
        SceneManager.LoadScene(mainMenuSceneName);
    }
    
    void PlayClickAnimation(Button button)
    {
        // Quick scale punch effect
        StartCoroutine(ScalePunch(button.transform));
    }
    
    IEnumerator ScalePunch(Transform target)
    {
        Vector3 originalScale = target.localScale;
        float punchScale = 0.9f;
        float duration = 0.1f;
        
        // Scale down
        target.localScale = originalScale * punchScale;
        yield return new WaitForSeconds(duration);
        
        // Scale back up
        target.localScale = originalScale * 1.05f;
        yield return new WaitForSeconds(duration);
        
        // Return to original
        target.localScale = originalScale;
    }
    
    IEnumerator InitialButtonAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        
        // Subtle pulse effect on buttons when scene loads
        if (playButton != null)
        {
            StartCoroutine(PulseButton(playButton.transform));
        }
        
        yield return new WaitForSeconds(0.2f);
        
        if (mainMenuButton != null)
        {
            StartCoroutine(PulseButton(mainMenuButton.transform));
        }
    }
    
    IEnumerator PulseButton(Transform button)
    {
        Vector3 originalScale = button.localScale;
        float pulseAmount = 1.05f;
        float duration = 0.3f;
        
        // Scale up
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            button.localScale = Vector3.Lerp(originalScale, originalScale * pulseAmount, t);
            yield return null;
        }
        
        // Scale down
        elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            button.localScale = Vector3.Lerp(originalScale * pulseAmount, originalScale, t);
            yield return null;
        }
        
        button.localScale = originalScale;
    }
    
    void Update()
    {
        // Keyboard shortcuts
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Space))
        {
            // Restart game with R or Space
            if (playButton != null && playButton.interactable)
            {
                playButton.onClick.Invoke();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.M))
        {
            // Go to main menu with ESC or M
            if (mainMenuButton != null && mainMenuButton.interactable)
            {
                mainMenuButton.onClick.Invoke();
            }
        }
    }
}