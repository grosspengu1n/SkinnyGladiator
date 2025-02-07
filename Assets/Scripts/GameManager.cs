using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float gameDuration = 900f; // 15 minutes in seconds
    private float elapsedTime = 0f;
    public int killCount = 0;

    // Upgrade thresholds 
    public int[] upgradeThresholds;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= gameDuration)
        {
            EndGame();
        }
    }

    public void RegisterKill()
    {
        killCount++;
        CheckForUpgrade();
    }

    void CheckForUpgrade()
    {
        foreach (int threshold in upgradeThresholds)
        {
            if (killCount == threshold)
            {
                // Pause game and show upgrade selection
                UpgradeManager.Instance.ShowUpgradeOptions();
                break;
            }
        }
    }

    void EndGame()
    {
        Debug.Log("Game Over!");
    }
}

