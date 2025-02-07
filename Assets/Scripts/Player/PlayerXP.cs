using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    [Header("XP Settings")]
    public int xp = 0;
    public int nextUpgradeXP = 15;  

    public void AddXP(int amount)
    {
        xp += amount;
        Debug.Log("Player XP: " + xp);

        if (xp >= nextUpgradeXP)
        {
            if (UpgradeManager.Instance != null)
            {
                UpgradeManager.Instance.ShowUpgradeOptions();
            }
            nextUpgradeXP *= 2;
        }
    }
}


