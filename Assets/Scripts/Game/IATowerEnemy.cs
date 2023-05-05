using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IATowerEnemy : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float fireRate;
    public float attackRange;

    private float fireRateTimer;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (playerTransform == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= attackRange)
        {
            if (fireRateTimer > 0)
            {
                fireRateTimer -= Time.deltaTime;
            }
            else
            {
                Shoot();
                fireRateTimer = fireRate;
            }
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();

        if (projectileRigidbody != null)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            projectileRigidbody.velocity = direction * projectileSpeed;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
