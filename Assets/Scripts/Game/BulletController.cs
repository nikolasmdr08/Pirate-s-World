using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public string tagTarget;
    public float bulletLifeTime = 0.5f;

    private void Start()
    {
        Destroy(gameObject, bulletLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        int damage = 1;
        if (collision.gameObject.tag == tagTarget)
        {
            if (tag == "Player")
            {
                GameObject enemy = GameObject.Find("EnemyController");
                IAEnemyController enemyIA = enemy.GetComponent<IAEnemyController>();
                damage = enemyIA.levelATK;
            }

            collision.gameObject.GetComponent<LifeManager>().ReceiveDamage(damage);
            Destroy(gameObject);
        }
    }
}
