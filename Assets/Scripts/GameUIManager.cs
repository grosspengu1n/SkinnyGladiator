using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [Header("UI References")]
    public Text timerText;     
    public Text healthText;    

    [Header("Game Timer Settings")]
    public float gameDuration = 900f; // 15 minutes in seconds
    private float remainingTime;

    private PlayerHealth playerHealth;

    void Start()
    {
        remainingTime = gameDuration;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime < 0)
                remainingTime = 0;
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (playerHealth != null)
        {
            healthText.text = "Health: " + playerHealth.currentHealth + " / " + playerHealth.maxHealth;
        }
    }
}

