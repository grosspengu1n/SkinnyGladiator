using UnityEngine;
using System.Collections;

public class RangedEnemyController : MonoBehaviour
{
    [Header("Smart Movement Settings")]
    public float followDistance = 5f;          
    public float speed = 3f;                   
    public float repositionInterval = 3f;      
    public float repositionRadius = 2f;        

    [Header("Shooting Settings")]
    public float shootInterval = 2f;          
    public GameObject bulletPrefab;          
    public Transform firePoint;                

    private Transform player;
    private Vector2 targetPosition;           
    private float repositionTimer = 0f;

    void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
        }
        targetPosition = transform.position;
        StartCoroutine(ShootRoutine());
    }

    void Update()
    {
        if (player == null)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > followDistance)
        {
            targetPosition = player.position;
            repositionTimer = 0f;
        }
        else
        {
            repositionTimer += Time.deltaTime;
            if (repositionTimer >= repositionInterval)
            {
                Vector2 randomOffset = Random.insideUnitCircle.normalized * repositionRadius;
                targetPosition = (Vector2)player.position + randomOffset;
                repositionTimer = 0f;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootInterval);
            ShootAtPlayer();
        }
    }

    void ShootAtPlayer()
    {
        if (player != null && bulletPrefab != null && firePoint != null)
        {
            // Calculate the direction from firePoint to the player.
            Vector2 direction = (player.position - firePoint.position).normalized;
            // Compute angle in degrees for correct rotation.
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // Adjust angle if your bullet sprite's default orientation requires it.
            Quaternion bulletRotation = Quaternion.Euler(0, 0, angle - 90f);
            // Instantiate the bullet.
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction * 10f;
            }

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.shooter = gameObject;
            }
        }
    }

}
