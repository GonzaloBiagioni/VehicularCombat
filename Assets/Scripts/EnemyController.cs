using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public float moveSpeed = 3f;
    public float detectionRange = 10f;
    public int damageAmount = 1; 
    public float attackCooldown = 1f;

    private Transform player;
    private bool isPlayerInRange = false;
    private bool canAttack = true;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        CheckPlayerInRange();

        if (isPlayerInRange)
        {
            MoveTowardsPlayer();

            if (canAttack)
            {
                AttackPlayer();
            }
        }
    }

    void CheckPlayerInRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            isPlayerInRange = true;
        }
        else
        {
            isPlayerInRange = false;
        }
    }

    void MoveTowardsPlayer()
    {
        transform.LookAt(player.position);
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    void AttackPlayer()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
        }

        canAttack = false;
        Invoke("ResetAttack", attackCooldown);
    }

    void ResetAttack()
    {
        canAttack = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}