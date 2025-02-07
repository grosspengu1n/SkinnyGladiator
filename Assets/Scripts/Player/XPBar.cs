using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    [Header("UI References")]
    public Slider xpSlider;      

    [Header("Player XP Reference")]
    public PlayerXP playerXP;    

    [Header("XP Threshold Settings")]
    public int initialThreshold = 15; 

    void Update()
    {
        int nextThreshold = playerXP.nextUpgradeXP;
        int prevThreshold = (nextThreshold == initialThreshold) ? 0 : nextThreshold / 2;


        float interval = nextThreshold - prevThreshold;

        float progress = (playerXP.xp - prevThreshold) / interval;

        // Clamp progress between 0 and 1 and update the slider.
        xpSlider.value = Mathf.Clamp01(progress);
    }
}

