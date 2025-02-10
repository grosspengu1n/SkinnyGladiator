using UnityEngine;
using System.Collections;

public class MeleeEnemyController : MonoBehaviour
{
    [Header("Movement & Attack")]
    public float moveSpeed = 3f;         
    public float attackRange = 1f;       
    public float attackCooldown = 1.5f;  
    public int damage = 1;              

    private Transform player;
    private bool canAttack = true;

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance > attackRange)
            {

                Vector2 direction = (player.position - transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                if (canAttack)
                {
                    Attack();
                }
            }
        }
    }

    void Attack()
    {
        Debug.Log("Melee enemy attacking player!");
        player.GetComponent<PlayerHealth>().TakeDamage(damage);

        StartCoroutine(AttackCooldownRoutine());
    }

    IEnumerator AttackCooldownRoutine()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}

