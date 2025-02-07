using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    [Header("Upgrade Assets")]
    public Upgrade[] availableUpgrades; 

    [Header("UI References")]
    public GameObject upgradePanel;    
    public Button[] upgradeButtons;    


    private List<Upgrade> currentOptions = new List<Upgrade>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (upgradePanel != null)
            upgradePanel.SetActive(false);
    }

    /// <summary>
    /// Displays the upgrade options UI by randomly selecting three unique upgrades.
    /// Pauses the game time.
    /// </summary>
    public void ShowUpgradeOptions()
    {
        Time.timeScale = 0f;
        if (upgradePanel != null)
            upgradePanel.SetActive(true);

        currentOptions.Clear();
        int optionsCount = Mathf.Min(3, availableUpgrades.Length);
        while (currentOptions.Count < optionsCount)
        {
            Upgrade candidate = availableUpgrades[Random.Range(0, availableUpgrades.Length)];
            if (!currentOptions.Contains(candidate))
                currentOptions.Add(candidate);
        }

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            if (i < currentOptions.Count)
            {
                Button btn = upgradeButtons[i];
                Text btnText = btn.GetComponentInChildren<Text>();
                if (btnText != null)
                    btnText.text = currentOptions[i].upgradeName + "\n" + currentOptions[i].description;

                btn.onClick.RemoveAllListeners();
                int index = i;
                btn.onClick.AddListener(() => { ApplyUpgrade(currentOptions[index]); });
                btn.gameObject.SetActive(true);
            }
            else
            {
                upgradeButtons[i].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Applies the chosen upgrade by calling the player's ApplyUpgrade method,
    /// resumes game time, and hides the upgrade panel.
    /// </summary>
    public void ApplyUpgrade(Upgrade chosen)
    {
        Time.timeScale = 1f;
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.ApplyUpgrade(chosen);
        }
        if (upgradePanel != null)
            upgradePanel.SetActive(false);
    }
}
