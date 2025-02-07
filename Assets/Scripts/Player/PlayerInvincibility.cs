using UnityEngine;
using System.Collections;

public class PlayerInvincibility : MonoBehaviour
{
    public int playerLayer = 8;    
    public int enemyLayer = 9;     

    public void StartInvincibility(float duration)
    {
        StartCoroutine(InvincibilityRoutine(duration));
    }

    IEnumerator InvincibilityRoutine(float duration)
    {
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);
        yield return new WaitForSeconds(duration);
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
    }
}


