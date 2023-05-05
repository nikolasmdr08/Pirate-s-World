using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_ATK : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 30f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject enemy = GameObject.Find("EnemyController");
            IAEnemyController enemyIA = enemy.GetComponent<IAEnemyController>();
            enemyIA.AddATK();
        }
        if (collision.gameObject.tag == "Player")
        {
            GameObject player = GameObject.Find("ShipPrefab");
            ShipMove playerReference = player.GetComponent<ShipMove>();
            playerReference.AddATK();
        }
        Destroy(this.gameObject);
    }
}
