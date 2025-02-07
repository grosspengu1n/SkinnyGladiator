using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 10f;         
    public float lifetime = 3f;       
    public int damage = 1;           

    [Header("Bullet Owner")]
    public GameObject shooter;        

    void Start()
    {

        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
            return;
        }


        if (collision.CompareTag("Enemy"))
        {

            if (shooter != null && collision.gameObject == shooter)
            {
                return;
            }

            Destroy(collision.gameObject);


            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                PlayerXP playerXP = playerObj.GetComponent<PlayerXP>();
                if (playerXP != null)
                {
                    playerXP.AddXP(1); 
                }
            }
            Destroy(gameObject);
            return;
        }

        Destroy(gameObject);
    }
}


