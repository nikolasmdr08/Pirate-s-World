using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_HP : MonoBehaviour
{

    private void Start()
    {
        Destroy(gameObject, 30f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            GameObject HPGO;
            if (collision.gameObject.tag == "Player")
            {
                HPGO = GameObject.Find("ShipPrefab");
            }
            else
            {
                HPGO = GameObject.Find("EnemyController");
            }
            LifeManager HPReference = HPGO.GetComponent<LifeManager>();
            HPReference.ReceiveHealing(5);
            Destroy(this.gameObject);
        }

    }
}
