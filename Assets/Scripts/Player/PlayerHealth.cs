using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;

    void Start()
    {
        currentHealth = maxHealth;
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died!");

        gameObject.SetActive(false);

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        currentHealth += amount;
    }
}



