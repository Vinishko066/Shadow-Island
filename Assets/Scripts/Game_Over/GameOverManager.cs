using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [Header("UI & Characters")]
    public GameObject gameOverPanel;   // Your Game Over panel (child of Canvas)
    public GameObject survivor;        // Player GameObject
    public GameObject enemy;          // Enemy GameObject

    private bool isGameOver = false;

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only trigger once
        if (isGameOver) return;

        // Check if survivor collided with killer
        if ((other.CompareTag("Enemy") && gameObject.CompareTag("Survivor")) ||
            (other.CompareTag("Survivor") && gameObject.CompareTag("Enemy")))
        {
            TriggerGameOver();
        }
    }

    void TriggerGameOver()
    {
        isGameOver = true;

        // Show Game Over UI
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // Hide characters
        if (survivor != null) survivor.SetActive(false);
        if (enemy != null) enemy.SetActive(false);

        // Freeze game
        Time.timeScale = 0f;
    }
}
