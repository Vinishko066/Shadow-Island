using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("Tags & Scene")]
    public string survivorTag = "Survivor";
    public string enemyTag = "Enemy";
    public string gameOverSceneName = "Game_Over";  // Must match your scene name in Build Settings

    private bool isGameOver = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"[GameOverManager] OnTriggerEnter2D called. Self tag: {gameObject.tag}, Other tag: {other.tag}");

        if (isGameOver)
        {
            Debug.Log("[GameOverManager] Game over already triggered. Ignoring collision.");
            return;
        }

        // Trigger if Survivor and Enemy collide
        if ((other.CompareTag(enemyTag) && gameObject.CompareTag(survivorTag)) ||
            (other.CompareTag(survivorTag) && gameObject.CompareTag(enemyTag)))
        {
            Debug.Log("[GameOverManager] Survivor and Enemy collided! Triggering Game Over.");
            TriggerGameOver();
        }
        else
        {
            Debug.Log("[GameOverManager] Collision detected, but tags do not match for Game Over.");
        }
    }

    void TriggerGameOver()
    {
        isGameOver = true;

        Debug.Log($"[GameOverManager] Loading Game Over scene: {gameOverSceneName}");
        SceneManager.LoadScene(gameOverSceneName);
    }
}
